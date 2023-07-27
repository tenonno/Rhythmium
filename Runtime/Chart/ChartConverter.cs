using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Guid = System.String;

namespace Rhythmium
{
    /// <summary>
    /// 譜面変換クラス
    /// </summary>
    /// <remarks>
    /// 譜面エディタで作成した JSON を ChartEntity に変換する
    /// </remarks>
    public abstract class ChartConverter<TChartEntity, TNoteEntity, TNoteLineEntity, TChartDifficulty>
        where TChartEntity : ChartEntity<TNoteEntity, TNoteLineEntity, TChartDifficulty>
        where TNoteEntity : NoteEntity
        where TNoteLineEntity : NoteLineEntity<TNoteEntity>
        where TChartDifficulty : Enum
    {
        protected abstract TChartEntity CreateInstance(
            ChartJsonData chartJsonData,
            string audioSource,
            TChartDifficulty difficulty,
            float startTime,
            List<TNoteEntity> notes,
            List<TNoteLineEntity> noteLines,
            List<BpmChangeEntity> bpmChanges,
            List<SpeedChangeEntity> speedChanges,
            List<OtherObjectEntity> otherObjects,
            List<MeasureEntity> measures,
            List<LayerEntity> layers
        );

        /// <summary>
        /// 譜面データを変換する
        /// </summary>
        /// <param name="chartJsonData">譜面データ</param>
        public TChartEntity Convert(ChartJsonData chartJsonData)
        {
            var audioSource = chartJsonData.AudioSource.Split('.')[0];
            var startTime = chartJsonData.StartTime;
            var difficulty = (TChartDifficulty)Enum.ToObject(typeof(TChartDifficulty), chartJsonData.Difficulty);

            // bpm changes
            var bpmChanges = chartJsonData.Timeline.OtherObjects.Where(o => o.Type == (int)OtherObjectType.Bpm)
                .Select(bpmChangeJsonData => (
                    bpm: float.Parse(bpmChangeJsonData.Value),
                    position: bpmChangeJsonData.MeasureIndex + bpmChangeJsonData.MeasurePosition.To01()
                )).ToList();

            // speed changes
            var speedChanges = chartJsonData.Timeline.OtherObjects
                .Where(o => o.Type == (int)OtherObjectType.Speed)
                .Select(o => new SpeedChangeEntity(o))
                .ToList();

            // カスタムオブジェクト
            var otherObjectEntities = chartJsonData.Timeline.OtherObjects
                .Where(o => o.Type >= (int)OtherObjectType.Other)
                .Select(jsonData => new OtherObjectEntity(jsonData))
                .ToList();

            var bpmChangeEntities = new List<BpmChangeEntity>();

            var sortedBpmChanges = bpmChanges.OrderBy(bpmChange => bpmChange.position).ToList();

            // 最終 BPM を譜面の最後に配置する
            sortedBpmChanges.Add((sortedBpmChanges.Last().bpm, position: 1000f));

            // BPM の区間を計算する
            var beginTime = 0f;
            for (var i = 0; i < sortedBpmChanges.Count - 1; i++)
            {
                var begin = sortedBpmChanges[i];
                var end = sortedBpmChanges[i + 1];

                // 1 小節の時間
                var unitTime = (60f / begin.bpm) * 4;

                var beginMeasureIndex = Mathf.FloorToInt(begin.position);
                var endMeasureIndex = Mathf.FloorToInt(end.position);

                // BPM の開始と終了が同じ小節の場合
                if (beginMeasureIndex == endMeasureIndex)
                {
                    var tempo = 1f;

                    var measureIndex = beginMeasureIndex;

                    if (chartJsonData.Timeline.Measures != null)
                    {
                        var tempoJsonData =
                            chartJsonData.Timeline.Measures.FirstOrDefault(a => a.index == measureIndex);

                        tempo = tempoJsonData == null ? 1f : tempoJsonData.beat.To01();
                    }

                    var length = end.position - begin.position;

                    // 区間の秒数
                    var time = unitTime * tempo * length;

                    bpmChangeEntities.Add(new BpmChangeEntity
                    {
                        BeginPosition = begin.position,
                        EndPosition = end.position,
                        BeginTime = beginTime,
                        Duration = time
                    });

                    beginTime += time;
                }

                for (var measureIndex = beginMeasureIndex; measureIndex < endMeasureIndex; measureIndex++)
                {
                    var tempo = 1f;

                    if (chartJsonData.Timeline.Measures != null)
                    {
                        var tempoJsonData =
                            chartJsonData.Timeline.Measures.FirstOrDefault(a => a.index == measureIndex);

                        tempo = tempoJsonData == null ? 1f : tempoJsonData.beat.To01();
                    }

                    // 小節の途中で BPM が変わる場合がある
                    var beginPosition = measureIndex == beginMeasureIndex ? begin.position : measureIndex;
                    var endPosition = measureIndex == endMeasureIndex - 1 ? end.position : measureIndex + 1;
                    var length = endPosition - beginPosition;

                    // 区間の秒数
                    var time = unitTime * tempo * length;

                    bpmChangeEntities.Add(new BpmChangeEntity
                    {
                        BeginPosition = beginPosition,
                        EndPosition = endPosition,
                        BeginTime = beginTime,
                        Duration = time
                    });

                    beginTime += time;
                }
            }

            var noteGuidMap = new Dictionary<Guid, TNoteEntity>();

            // ノートを生成する
            var noteEntities = new List<TNoteEntity>();
            foreach (var noteJsonData in chartJsonData.Timeline.Notes)
            {
                // ノートの小節位置
                var noteMeasurePosition = noteJsonData.MeasureIndex + noteJsonData.MeasurePosition.To01();

                // 判定時間を取得する
                var judgeTime = bpmChangeEntities.Find(bpmRange => bpmRange.Between(noteMeasurePosition))
                    .GetJudgeTime(noteMeasurePosition);

                var note = ScriptableObject.CreateInstance<TNoteEntity>();
                note.Initialize(noteJsonData, judgeTime);
                note.name = $"note_{noteJsonData.Guid}";

                noteGuidMap[noteJsonData.Guid] = note;

                noteEntities.Add(note);
            }

            // ノートラインを生成する
            var noteLineEntities = new List<TNoteLineEntity>();
            foreach (var noteLineJsonData in chartJsonData.Timeline.NoteLines)
            {
                var headNote = noteGuidMap[noteLineJsonData.head];
                var tailNote = noteGuidMap[noteLineJsonData.tail];

                // 横に繋がっているロングは除外する
                if (Mathf.Approximately(headNote.JudgeTime, tailNote.JudgeTime)) continue;

                var noteLine = ScriptableObject.CreateInstance<TNoteLineEntity>();
                noteLine.Initialize(noteLineJsonData, headNote, tailNote);
                noteLineEntities.Add(noteLine);
            }

            var measureEntities = new List<MeasureEntity>();

            if (chartJsonData.Timeline.Measures != null)
            {
                // 小節を生成する
                foreach (var measureJsonData in chartJsonData.Timeline.Measures)
                {
                    try
                    {
                        // 判定時間を取得する
                        var judgeTime = bpmChangeEntities.Find(bpmRange => bpmRange.Between(measureJsonData.index))
                            .GetJudgeTime(measureJsonData.index);

                        var measure = new MeasureEntity(measureJsonData, judgeTime);
                        measureEntities.Add(measure);
                    }
                    catch (NullReferenceException e)
                    {
                        Debug.LogError(measureJsonData.index + " / " + e.Message);
                    }
                }
            }

            noteEntities = noteEntities.OrderBy(note => note.JudgeTime).ToList();

            var layers = chartJsonData.Layers.Select(layer => new LayerEntity(layer)).ToList();

            return CreateInstance(chartJsonData, audioSource, difficulty, startTime, noteEntities, noteLineEntities,
                bpmChangeEntities,
                speedChanges,
                otherObjectEntities, measureEntities, layers);
        }
    }
}
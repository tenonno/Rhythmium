#nullable enable

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
        /// <summary>
        /// 譜面データを変換する
        /// </summary>
        /// <param name="chartJsonData">譜面データ</param>
        public TChartEntity Convert(ChartJsonData chartJsonData)
        {
            var chart = ScriptableObject.CreateInstance<TChartEntity>();

            return Convert(chartJsonData, chart);
        }

        /// <summary>
        /// 譜面データを変換する
        /// </summary>
        /// <param name="chartJsonData">譜面データ</param>
        /// <param name="chart">対象譜面</param>
        protected virtual TChartEntity Convert(ChartJsonData chartJsonData, TChartEntity chart)
        {
            chart.AudioSource = chartJsonData.AudioSource.Split('.')[0];
            chart.StartTime = chartJsonData.StartTime;
            chart.Difficulty = (TChartDifficulty) Enum.ToObject(typeof(TChartDifficulty), chartJsonData.Difficulty);

            // bpm changes
            var bpmChanges = new List<(float bpm, float position)>();
            bpmChanges.AddRange(chartJsonData.Timeline.OtherObjects.Where(o => o.type == (int) OtherObjectType.Bpm)
                .Select(bpmChangeJsonData => (float.Parse(bpmChangeJsonData.value),
                    position: bpmChangeJsonData.measureIndex + bpmChangeJsonData.measurePosition.To01())));

            // speed changes
            chart.SpeedChanges = new List<SpeedChangeEntity>();
            foreach (var speedChange in chartJsonData.Timeline.OtherObjects.Where(o =>
                o.type == (int) OtherObjectType.Speed))
            {
                chart.SpeedChanges.Add(new SpeedChangeEntity(speedChange));
            }

            // カスタムオブジェクト
            chart.OtherObjects = new List<OtherObjectEntity>();
            foreach (var jsonData in chartJsonData.Timeline.OtherObjects.Where(
                o => o.type >= (int) OtherObjectType.Other))
            {
                chart.OtherObjects.Add(new OtherObjectEntity(jsonData));
            }

            chart.BpmChanges = new List<BpmChangeEntity>();

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

                for (var measureIndex = beginMeasureIndex; measureIndex < endMeasureIndex; measureIndex++)
                {
                    var tempo = 1f;

                    if (chartJsonData.Timeline.Measures != null)
                    {
                        var tempoJsonData =
                            chartJsonData.Timeline.Measures.FirstOrDefault(a => a.index == measureIndex);

                        tempo = tempoJsonData == null ? 1f : tempoJsonData.beat.To01();
                    }

                    // 区間の秒数
                    var time = unitTime * tempo;

                    chart.BpmChanges.Add(new BpmChangeEntity
                    {
                        BeginPosition = measureIndex,
                        EndPosition = measureIndex + 1,
                        BeginTime = beginTime,
                        Duration = time
                    });

                    beginTime += time;
                }
            }

            var noteGuidMap = new Dictionary<Guid, TNoteEntity>();

            // ノートを生成する
            chart.Notes = new List<TNoteEntity>();
            foreach (var noteJsonData in chartJsonData.Timeline.Notes)
            {
                // ノートの小節位置
                var noteMeasurePosition = noteJsonData.measureIndex + noteJsonData.measurePosition.To01();

                // 判定時間を取得する
                var judgeTime = chart.BpmChanges.Find(bpmRange => bpmRange.Between(noteMeasurePosition))
                    .GetJudgeTime(noteMeasurePosition);

                var note = ScriptableObject.CreateInstance<TNoteEntity>();
                note.Initialize(noteJsonData, judgeTime);
                note.name = $"note_{noteJsonData.guid}";

                noteGuidMap[noteJsonData.guid] = note;

                chart.Notes.Add(note);
            }

            // ノートラインを生成する
            chart.NoteLines = new List<TNoteLineEntity>();
            foreach (var noteLineJsonData in chartJsonData.Timeline.NoteLines)
            {
                var headNote = noteGuidMap[noteLineJsonData.head];
                var tailNote = noteGuidMap[noteLineJsonData.tail];

                // 横に繋がっているロングは除外する
                if (Mathf.Approximately(headNote.JudgeTime, tailNote.JudgeTime)) continue;

                var noteLine = ScriptableObject.CreateInstance<TNoteLineEntity>();
                noteLine.Initialize(noteLineJsonData, headNote, tailNote);
                chart.NoteLines.Add(noteLine);
            }

            chart.Measures = new List<MeasureEntity>();

            if (chartJsonData.Timeline.Measures != null)
            {
                // 小節を生成する
                foreach (var measureJsonData in chartJsonData.Timeline.Measures)
                {
                    try
                    {
                        // 判定時間を取得する
                        var judgeTime = chart.BpmChanges.Find(bpmRange => bpmRange.Between(measureJsonData.index))
                            .GetJudgeTime(measureJsonData.index);

                        var measure = new MeasureEntity(measureJsonData, judgeTime);
                        chart.Measures.Add(measure);
                    }
                    catch (NullReferenceException e)
                    {
                        Debug.LogError(measureJsonData.index + " / " + e.Message);
                    }
                }
            }

            chart.Notes = chart.Notes.OrderBy(note => note.JudgeTime).ToList();

            return chart;
        }
    }
}
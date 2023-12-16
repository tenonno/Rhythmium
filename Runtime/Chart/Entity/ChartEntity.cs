using System;
using System.Collections.Generic;

namespace Rhythmium
{
    /// <summary>
    /// 譜面のエンティティ
    /// </summary>
    public abstract class ChartEntity<TNoteEntity, TNoteType, TNoteLineEntity, TChartDifficulty>
        where TNoteEntity : NoteEntity<TNoteType>
        where TNoteType : Enum
        where TNoteLineEntity : NoteLineEntity<TNoteEntity, TNoteType>
        where TChartDifficulty : Enum
    {
        /// <summary>
        /// 音源
        /// </summary>
        public string AudioSource { get; }

        /// <summary>
        /// 難易度
        /// </summary>
        public TChartDifficulty Difficulty { get; }

        /// <summary>
        /// 開始時間
        /// </summary>
        public float StartTime { get; }

        /// <summary>
        /// ノート
        /// </summary>
        public List<TNoteEntity> Notes { get; }

        /// <summary>
        /// ノートライン
        /// </summary>
        public List<TNoteLineEntity> NoteLines { get; }

        /// <summary>
        /// BPM 変更
        /// </summary>
        public List<BpmChangeEntity> BpmChanges { get; }

        /// <summary>
        /// 速度変更
        /// </summary>
        public List<SpeedChangeEntity> SpeedChanges { get; }

        /// <summary>
        /// カスタムオブジェクト
        /// </summary>
        public List<OtherObjectEntity> OtherObjects { get; }

        /// <summary>
        /// 小節
        /// </summary>
        public List<MeasureEntity> Measures { get; }

        public List<LayerEntity> Layers { get; }

        protected ChartEntity(
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
        )
        {
            AudioSource = audioSource;
            Difficulty = difficulty;
            StartTime = startTime;
            Notes = notes;
            NoteLines = noteLines;
            BpmChanges = bpmChanges;
            SpeedChanges = speedChanges;
            OtherObjects = otherObjects;
            Measures = measures;
            Layers = layers;
        }
    }
}
#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Guid = System.String;

// ReSharper disable InconsistentNaming

namespace Rhythmium
{
    /// <summary>
    /// 譜面のエンティティ
    /// </summary>
    public class ChartEntity<TNoteEntity, TNoteLineEntity, TChartDifficulty> : ScriptableObject
        where TNoteEntity : NoteEntity
        where TNoteLineEntity : NoteLineEntity<TNoteEntity>
        where TChartDifficulty : Enum
    {
        /// <summary>音源</summary>
        public string AudioSource { get; set; }

        /// <summary>難易度</summary>
        public TChartDifficulty Difficulty { get; set; }

        /// <summary>開始時間</summary>
        public float StartTime { get; set; }

        /// <summary>ノート</summary>
        public List<TNoteEntity> Notes { get; set; }

        /// <summary>ノートライン</summary>
        public List<TNoteLineEntity> NoteLines { get; set; }

        /// <summary>BPM 変更</summary>
        public List<BpmChangeEntity> BpmChanges { get; set; }

        /// <summary>速度変更</summary>
        public List<SpeedChangeEntity> SpeedChanges { get; set; }

        /// <summary>カスタムオブジェクト</summary>
        public List<OtherObjectEntity> OtherObjects { get; set; }

        /// <summary>小節</summary>
        public List<MeasureEntity> Measures { get; set; }

        public T Mirror<T>() where T : ChartEntity<TNoteEntity, TNoteLineEntity, TChartDifficulty>
        {
            var mirroredNotes = Notes.ToDictionary(note => note, note => note.Mirror<TNoteEntity>());
            var mirroredNoteLines =
                NoteLines.Select(noteLine => noteLine.Mirror<TNoteLineEntity>(mirroredNotes)).ToList();

            var chartEntity = CreateInstance<T>();
            chartEntity.AudioSource = AudioSource;
            chartEntity.Difficulty = Difficulty;
            chartEntity.StartTime = StartTime;
            chartEntity.Notes = mirroredNotes.Values.ToList();
            chartEntity.NoteLines = mirroredNoteLines;
            chartEntity.BpmChanges = BpmChanges;
            chartEntity.SpeedChanges = SpeedChanges;
            chartEntity.OtherObjects = OtherObjects;
            chartEntity.Measures = Measures;
            return chartEntity;
        }
    }
}
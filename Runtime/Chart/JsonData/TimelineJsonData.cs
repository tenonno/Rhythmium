using System;
using UnityEngine;

namespace Rhythmium
{
    [Serializable]
    public sealed class TimelineJsonData
    {
        // ReSharper disable InconsistentNaming

        [SerializeField] private NoteJsonData[] notes = null!;
        [SerializeField] private NoteLineJsonData[] noteLines = null!;
        [SerializeField] private MeasureJsonData[] measures = null!;
        [SerializeField] private OtherObjectJsonData[] otherObjects = null!;

        // ReSharper restore InconsistentNaming

        public NoteJsonData[] Notes => notes;
        public NoteLineJsonData[] NoteLines => noteLines;
        public MeasureJsonData[] Measures => measures;
        public OtherObjectJsonData[] OtherObjects => otherObjects;
    }
}
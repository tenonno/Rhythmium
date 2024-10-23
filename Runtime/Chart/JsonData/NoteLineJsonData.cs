using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Guid = System.String;

namespace Rhythmium
{
    [Serializable]
    public sealed class NoteLineJsonData
    {
        // ReSharper disable InconsistentNaming

        [SerializeField] private Guid guid = null!;
        [SerializeField] private Guid head = null!;
        [SerializeField] private Guid tail = null!;
        [SerializeField] private NoteLineBezierJsonData bezier = null!;
        [SerializeField] private NoteLineVerticalFitJsonData verticalFit = null!;

        // ReSharper restore InconsistentNaming

        public Guid Guid => guid;
        public Guid Head => head;
        public Guid Tail => tail;
        public NoteLineBezierJsonData Bezier => bezier;
        public NoteLineVerticalFitJsonData VerticalFit => verticalFit;
    }

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public sealed class NoteLineBezierJsonData
    {
        [SerializeField] private bool enabled;
        [SerializeField] private float x;
        [SerializeField] private float y;

        public bool Enable => enabled;
        public Vector2 Value => new(x, y);
    }

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public sealed class NoteLineVerticalFitJsonData
    {
        [SerializeField] private bool enabled;

        public bool Enable => enabled;
    }
}
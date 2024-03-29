using System;
using UnityEngine;
using Guid = System.String;

namespace Rhythmium
{
    [Serializable]
    public sealed class NoteLineJsonData
    {
        // ReSharper disable InconsistentNaming

        [SerializeField] private Guid guid;
        [SerializeField] private Guid head;
        [SerializeField] private Guid tail;
        [SerializeField] private NoteLineBezierJsonData bezier;

        // ReSharper restore InconsistentNaming

        public Guid Guid => guid;
        public Guid Head => head;
        public Guid Tail => tail;

        public NoteLineBezierJsonData Bezier => bezier;
    }

    [Serializable]
    public sealed class NoteLineBezierJsonData
    {
        [SerializeField] bool enabled;
        [SerializeField] float x;
        [SerializeField] float y;

        public bool Enable => enabled;
        public Vector2 Value => new(x, y);
    }
}
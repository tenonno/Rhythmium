using System;
using UnityEngine;
using Guid = System.String;

// ReSharper disable InconsistentNaming

namespace Rhythmium
{
    [Serializable]
    public sealed class NoteLineJsonData
    {
        public Guid guid;
        public Guid head;
        public Guid tail;

        [SerializeField] private NoteLineBezierJsonData bezier;

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
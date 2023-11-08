using System;
using UnityEngine;

namespace Rhythmium.NoteEditor
{
    [Serializable]
    public struct NoteEditorStatus
    {
        // ReSharper disable InconsistentNaming
        [SerializeField] private string name;
        [SerializeField] private float time;
        [SerializeField] private long updatedAt;
        // ReSharper restore InconsistentNaming

        public string Name => name;
        public float Time => time;
        public long UpdatedAt => updatedAt;
    }
}
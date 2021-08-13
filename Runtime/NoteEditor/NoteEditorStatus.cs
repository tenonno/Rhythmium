using System;

namespace Rhythmium.NoteEditor
{
    [Serializable]
    public struct NoteEditorStatus
    {
        public string name;
        public float time;
        public long updatedAt;
    }
}

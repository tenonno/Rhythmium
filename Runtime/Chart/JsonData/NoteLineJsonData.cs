using System;
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
    }
}

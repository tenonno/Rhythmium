using System;
using Guid = System.String;

// ReSharper disable InconsistentNaming

namespace Rhythmium
{
    [Serializable]
    public sealed class OtherObjectJsonData
    {
        public Guid guid;
        public int measureIndex;
        public FractionJsonData measurePosition;
        public int type;
        public string value;
    }

    public enum OtherObjectType
    {
        Bpm,
        Speed,
        Stop,
        Other
    }
}

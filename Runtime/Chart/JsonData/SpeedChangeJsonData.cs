using System;
using Guid = System.String;

// ReSharper disable InconsistentNaming

namespace Rhythmium
{
    [Serializable]
    public struct SpeedChangeJsonData
    {
        public int measureIndex;
        public FractionJsonData measurePosition;
        public float speed;
    }
}

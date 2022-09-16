#nullable enable

using System;
using Guid = System.String;

namespace Rhythmium
{
    [Serializable]
    public struct SpeedChangeJsonData
    {
        // ReSharper disable InconsistentNaming

        public int measureIndex;
        public FractionJsonData measurePosition;
        public float speed;
        public string layer;

        // ReSharper restore InconsistentNaming
    }
}
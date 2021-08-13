using System;
using Guid = System.String;

// ReSharper disable InconsistentNaming

namespace Rhythmium
{
    [Serializable]
    public class MeasureJsonData
    {
        public int index;
        public FractionFloatJsonData beat;
    }
}

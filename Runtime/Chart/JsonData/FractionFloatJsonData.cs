using System;

// ReSharper disable InconsistentNaming

namespace Rhythmium
{
    [Serializable]
    public struct FractionFloatJsonData
    {
        public float numerator;
        public float denominator;

        public float To01()
        {
            return 1f / denominator * numerator;
        }
    }
}

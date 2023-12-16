using System;
using UnityEngine;

namespace Rhythmium
{
    [Serializable]
    public sealed class FractionJsonData
    {
        // ReSharper disable InconsistentNaming

        [SerializeField] private int numerator;
        [SerializeField] private int denominator;

        // ReSharper restore InconsistentNaming

        public FractionJsonData(int numerator, int denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }

        public int Numerator => numerator;
        public int Denominator => denominator;

        public float To01()
        {
            return 1f / Denominator * Numerator;
        }
    }
}
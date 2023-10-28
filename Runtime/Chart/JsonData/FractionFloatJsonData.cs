using System;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace Rhythmium
{
    [Serializable]
    public struct FractionFloatJsonData
    {
        [SerializeField] private float numerator;
        [SerializeField] private float denominator;

        public float Numerator => numerator;
        public float Denominator => denominator;

        public float To01()
        {
            return 1f / denominator * numerator;
        }
    }
}
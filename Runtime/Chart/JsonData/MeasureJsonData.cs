using System;
using UnityEngine;

namespace Rhythmium
{
    [Serializable]
    public sealed class MeasureJsonData
    {
        // ReSharper disable InconsistentNaming

        [SerializeField] private int index;
        [SerializeField] private FractionFloatJsonData beat;
        [SerializeField] private bool invisibleLine;

        // ReSharper restore InconsistentNaming

        public int Index => index;
        public FractionFloatJsonData Beat => beat;
        public bool InvisibleLine => invisibleLine;
    }
}
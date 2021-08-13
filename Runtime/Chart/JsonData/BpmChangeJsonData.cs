#nullable enable

using System;
using UnityEngine;

namespace Rhythmium
{
    [Serializable]
    public sealed class BpmChangeJsonData
    {
        // ReSharper disable InconsistentNaming

        [SerializeField] private int measureIndex;
        [SerializeField] private FractionJsonData measurePosition = null!;
        [SerializeField] private float bpm;
        [SerializeField] private string guid = null!;

        // ReSharper restore InconsistentNaming

        public int MeasureIndex => measureIndex;
        public FractionJsonData MeasurePosition => measurePosition;
        public float Bpm => bpm;
        public string Guid => guid;
    }
}
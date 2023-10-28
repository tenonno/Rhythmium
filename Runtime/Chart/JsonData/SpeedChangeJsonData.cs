using System;
using UnityEngine;
using Guid = System.String;

namespace Rhythmium
{
    [Serializable]
    public struct SpeedChangeJsonData
    {
        // ReSharper disable InconsistentNaming

        [SerializeField] public int measureIndex;
        [SerializeField] public FractionJsonData measurePosition;
        [SerializeField] public float speed;
        [SerializeField] public string layer;

        // ReSharper restore InconsistentNaming

        public int MeasureIndex => measureIndex;
        public FractionJsonData MeasurePosition => measurePosition;
        public float Speed => speed;
        public string Layer => layer;
    }
}
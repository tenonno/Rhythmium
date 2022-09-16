#nullable enable

using System;
using UnityEngine;
using Guid = System.String;

namespace Rhythmium
{
    [Serializable]
    public sealed class OtherObjectJsonData
    {
        // ReSharper disable InconsistentNaming

#pragma warning disable CS8618
        [SerializeField] private Guid guid;
        [SerializeField] private int measureIndex;
        [SerializeField] private FractionJsonData measurePosition;
        [SerializeField] private int type;
        [SerializeField] private string value;
        [SerializeField] private string layer;
#pragma warning restore CS8618

        // ReSharper restore InconsistentNaming

        public Guid Guid => guid;
        public int MeasureIndex => measureIndex;
        public FractionJsonData MeasurePosition => measurePosition;
        public int Type => type;
        public string Value => value;
        public string Layer => layer;
    }

    public enum OtherObjectType
    {
        Bpm,
        Speed,
        Stop,
        Other
    }
}
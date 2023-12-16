using System;
using UnityEngine;

namespace Rhythmium
{
    [Serializable]
    public sealed class OtherObjectJsonData
    {
        // ReSharper disable InconsistentNaming

#pragma warning disable CS8618
        [SerializeField] private string guid;
        [SerializeField] private int measureIndex;
        [SerializeField] private FractionJsonData measurePosition;
        [SerializeField, Obsolete] private int type;
        [SerializeField] private string typeName;
        [SerializeField] private string value;
        [SerializeField] private string layer;
#pragma warning restore CS8618

        // ReSharper restore InconsistentNaming

        public string Guid => guid;
        public int MeasureIndex => measureIndex;
        public FractionJsonData MeasurePosition => measurePosition;

        public string TypeName => typeName;

        public string Value => value;
        public string Layer => layer;

        public void MigrateTypeName(string[] typeNames)
        {
            typeName = typeNames[type];
        }
    }

    public enum OtherObjectType
    {
        Bpm,
        Speed,
        Stop,
        Other
    }
}
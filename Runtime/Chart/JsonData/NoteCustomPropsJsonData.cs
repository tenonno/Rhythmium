#nullable enable

using System;
using UnityEngine;

namespace Rhythmium
{
    [Serializable]
    public struct NoteCustomPropsJsonData
    {
        // ReSharper disable InconsistentNaming

        [SerializeField] private string type;
        [SerializeField] private string targetNoteLine;
        [SerializeField] private float scale;
        [SerializeField] private float distance;

        // ReSharper restore InconsistentNaming

        public string Type => type;
        public string TargetNoteLine => targetNoteLine;
        public float Scale => scale;
        public float Distance => distance;

        public NoteCustomPropsJsonData(string type, string targetNoteLine, float scale, float distance)
        {
            this.type = type;
            this.targetNoteLine = targetNoteLine;
            this.scale = scale;
            this.distance = distance;
        }
    }
}
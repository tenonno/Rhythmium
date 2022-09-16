#nullable enable

using System;
using UnityEngine;

namespace Rhythmium
{
    [Serializable]
    public sealed class LayerJsonData
    {
        // ReSharper disable InconsistentNaming

        [SerializeField] private string guid = null!;
        [SerializeField] private int group;

        // ReSharper restore InconsistentNaming

        public string Guid => guid;
        public int Group => group;
    }
}
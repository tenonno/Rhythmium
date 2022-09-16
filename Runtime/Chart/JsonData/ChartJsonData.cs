#nullable enable

using System;
using UnityEngine;

namespace Rhythmium
{
    [Serializable]
    public sealed class ChartJsonData
    {
        // ReSharper disable InconsistentNaming

        [SerializeField] private int musicGameSystemVersion;
        [SerializeField] private int difficulty;
        [SerializeField] private string level = null!;
        [SerializeField] private string name = null!;
        [SerializeField] private string audioSource = null!;
        [SerializeField] private float startTime;
        [SerializeField] private float developmentStartTime;
        [SerializeField] private TimelineJsonData timeline = null!;
        [SerializeField] private LayerJsonData[] layers = null!;

        // ReSharper restore InconsistentNaming

        public int MusicGameSystemVersion => musicGameSystemVersion;
        public int Difficulty => difficulty;
        public string Level => level;
        public string Name => name;
        public string AudioSource => audioSource;
        public float StartTime => startTime;
        public float DevelopmentStartTime => developmentStartTime;
        public TimelineJsonData Timeline => timeline;
        public LayerJsonData[] Layers => layers;
    }
}
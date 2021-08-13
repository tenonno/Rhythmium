using System;
using UnityEngine;

namespace Rhythmium
{
    [CreateAssetMenu(menuName = "Rhythmium/RhythmiumSetting")]
    public class RhythmiumSetting : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _namespace;
        [SerializeField] private string _scriptDirectory;
        [SerializeField] private string[] _difficulties;
    }


    [Serializable]
    public struct ChartDifficultySetting
    {
    }
}

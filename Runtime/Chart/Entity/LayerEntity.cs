#nullable enable

using System;
using UnityEngine;

namespace Rhythmium
{
    /// <summary>
    /// レイヤー情報
    /// </summary>
    [Serializable]
    public struct LayerEntity
    {
        [SerializeField] private string _guid;
        [SerializeField] private int _group;

        public string Guid => _guid;
        public int Group => _group;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LayerEntity(LayerJsonData layerJsonData)
        {
            _guid = layerJsonData.Guid;
            _group = layerJsonData.Group;

            if (_group == 0)
            {
                _group = 1;
            }
        }
    }
}
using System;
using UnityEngine;

namespace Rhythmium
{
    /// <summary>
    /// カスタムオブジェクト情報
    /// </summary>
    [Serializable]
    public struct OtherObjectEntity
    {
        [SerializeField] private int _type;
        [SerializeField] private float _position;
        [SerializeField] private string _value;

        /// <summary>タイプ</summary>
        public int Type => _type;

        /// <summary>小節位置</summary>
        public float Position => _position;

        /// <summary>値</summary>
        public string Value => _value;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="jsonData">オブジェクト情報</param>
        public OtherObjectEntity(OtherObjectJsonData jsonData)
        {
            _type = jsonData.type;
            _position = jsonData.measureIndex + jsonData.measurePosition.To01();
            _value = jsonData.value;
        }
    }
}

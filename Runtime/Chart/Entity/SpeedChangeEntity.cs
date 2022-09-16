#nullable enable

using System;
using UnityEngine;

namespace Rhythmium
{
    /// <summary>
    /// 速度変更情報
    /// </summary>
    [Serializable]
    public struct SpeedChangeEntity
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _position;
        [SerializeField] private string _layer;

        /// <summary>
        /// 速度
        /// </summary>
        public float Speed => _speed;

        /// <summary>
        /// 小節位置
        /// </summary>
        public float Position => _position;

        public string Layer => _layer;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="speedChangeJsonData">速度変更情報</param>
        public SpeedChangeEntity(SpeedChangeJsonData speedChangeJsonData)
        {
            _position = speedChangeJsonData.measureIndex + speedChangeJsonData.measurePosition.To01();
            _speed = speedChangeJsonData.speed;
            _layer = speedChangeJsonData.layer;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="speedChangeJsonData">速度変更情報</param>
        public SpeedChangeEntity(OtherObjectJsonData speedChangeJsonData)
        {
            _position = speedChangeJsonData.MeasureIndex + speedChangeJsonData.MeasurePosition.To01();
            _speed = float.Parse(speedChangeJsonData.Value);
            _layer = speedChangeJsonData.Layer;
        }
    }
}
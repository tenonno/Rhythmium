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

        /// <summary>速度</summary>
        public float Speed => _speed;

        /// <summary>小節位置</summary>
        public float Position => _position;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="speedChangeJsonData">速度変更情報</param>
        public SpeedChangeEntity(SpeedChangeJsonData speedChangeJsonData)
        {
            _position = speedChangeJsonData.measureIndex + speedChangeJsonData.measurePosition.To01();
            _speed = speedChangeJsonData.speed;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="speedChangeJsonData">速度変更情報</param>
        public SpeedChangeEntity(OtherObjectJsonData speedChangeJsonData)
        {
            _position = speedChangeJsonData.measureIndex + speedChangeJsonData.measurePosition.To01();
            _speed = float.Parse(speedChangeJsonData.value);
        }
    }
}

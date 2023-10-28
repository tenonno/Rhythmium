namespace Rhythmium
{
    /// <summary>
    /// 速度変更情報
    /// </summary>
    public struct SpeedChangeEntity
    {
        /// <summary>
        /// 速度
        /// </summary>
        public readonly float Speed;

        /// <summary>
        /// 小節位置
        /// </summary>
        public readonly float Position;

        public readonly string Layer;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="speedChangeJsonData">速度変更情報</param>
        public SpeedChangeEntity(SpeedChangeJsonData speedChangeJsonData)
        {
            Position = speedChangeJsonData.measureIndex + speedChangeJsonData.measurePosition.To01();
            Speed = speedChangeJsonData.speed;
            Layer = speedChangeJsonData.layer;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="speedChangeJsonData">速度変更情報</param>
        public SpeedChangeEntity(OtherObjectJsonData speedChangeJsonData)
        {
            Speed = float.Parse(speedChangeJsonData.Value);
            Position = speedChangeJsonData.MeasureIndex + speedChangeJsonData.MeasurePosition.To01();
            Layer = speedChangeJsonData.Layer;
        }
    }
}
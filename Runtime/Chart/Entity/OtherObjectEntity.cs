namespace Rhythmium
{
    /// <summary>
    /// カスタムオブジェクト情報
    /// </summary>
    public sealed class OtherObjectEntity
    {
        /// <summary>タイプ</summary>
        public string TypeName { get; }

        /// <summary>小節位置</summary>
        public float Position { get; }

        /// <summary>値</summary>
        public string Value { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="jsonData">オブジェクト情報</param>
        public OtherObjectEntity(OtherObjectJsonData jsonData)
        {
            TypeName = jsonData.TypeName;
            Position = jsonData.MeasureIndex + jsonData.MeasurePosition.To01();
            Value = jsonData.Value;
        }
    }
}
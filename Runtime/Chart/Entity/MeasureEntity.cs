// ReSharper disable InconsistentNaming

namespace Rhythmium
{
    /// <summary>
    /// 小節情報
    /// </summary>
    public sealed class MeasureEntity
    {
        /// <summary>
        /// 判定時間
        /// </summary>
        public readonly float JudgeTime;

        /// <summary>
        /// 小節線を非表示にするか
        /// </summary>
        public readonly bool InvisibleLine;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data">小節情報</param>
        /// <param name="judgeTime">判定時間</param>
        public MeasureEntity(MeasureJsonData data, float judgeTime)
        {
            JudgeTime = judgeTime;
            InvisibleLine = data.InvisibleLine;
        }
    }
}
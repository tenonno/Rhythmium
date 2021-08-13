using System;

// ReSharper disable InconsistentNaming

namespace Rhythmium
{
    /// <summary>
    /// BPM 変更情報
    /// </summary>
    [Serializable]
    public struct BpmChangeEntity
    {
        /// <summary>開始時間</summary>
        public float BeginTime;

        /// <summary>開始小節</summary>
        public float BeginPosition;

        /// <summary>終了小節</summary>
        public float EndPosition;

        /// <summary>区間の秒数</summary>
        public float Duration;

        /// <summary>範囲内に含まれるか</summary>
        public bool Between(float value)
        {
            return value >= BeginPosition && value < EndPosition;
        }

        /// <summary>
        /// 判定時間を取得する
        /// </summary>
        /// <param name="measurePosition">小節位置</param>
        /// <returns>判定時間</returns>
        public float GetJudgeTime(float measurePosition)
        {
            return BeginTime + (measurePosition - BeginPosition) / (EndPosition - BeginPosition) * Duration;
        }
    }
}

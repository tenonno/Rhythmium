using System;

namespace Rhythmium
{
    /// <summary>
    /// ノート情報
    /// </summary>
    public abstract class NoteEntity<TNoteType> where TNoteType : Enum
    {
        private readonly NoteJsonData _jsonData;

        /// <summary>
        /// GUID
        /// </summary>
        public string Guid => _jsonData.Guid;

        /// <summary>
        /// ノートタイプ
        /// </summary>
        public readonly TNoteType Type;

        /// <summary>
        /// サイズ
        /// </summary>
        public int Size { get; protected set; }

        /// <summary>
        /// レーン位置
        /// </summary>
        public int LanePosition { get; protected set; }

        /// <summary>
        /// 判定時間
        /// </summary>
        public readonly float JudgeTime;


        public NoteCustomPropsJsonData CustomProps => _jsonData.CustomProps;
        public string Layer => _jsonData.Layer;

        public int MeasureIndex => _jsonData.MeasureIndex;
        public FractionJsonData MeasurePosition => _jsonData.MeasurePosition;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="noteJsonData">ノート情報</param>
        /// <param name="judgeTime">判定時間</param>
        /// <param name="getNoteType"></param>
        /// <param name="isMirror"></param>
        protected NoteEntity(NoteJsonData noteJsonData, float judgeTime, Func<NoteJsonData, TNoteType> getNoteType,
            bool isMirror)
        {
            _jsonData = noteJsonData;
            Type = getNoteType(noteJsonData);
            Size = noteJsonData.HorizontalSize;
            LanePosition = noteJsonData.HorizontalPosition.Numerator;

            if (isMirror)
            {
                LanePosition = MirrorLanePosition(Size, LanePosition, noteJsonData.HorizontalPosition.Denominator);
            }

            JudgeTime = judgeTime;
        }

        protected int MirrorLanePosition(int size, int lanePosition, int laneDivision)
        {
            return laneDivision - 1 - lanePosition - (size - 1);
        }
    }
}
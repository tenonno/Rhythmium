using System;
using UnityEngine;

namespace Rhythmium
{
    /// <summary>
    /// ノート情報
    /// </summary>
    public abstract class NoteEntity
    {
        /// <summary>
        /// 整数型のノートタイプ
        /// </summary>
        public readonly int IntType; // { get; private set; }

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
        public readonly float JudgeTime; // { get; private set; }

        public readonly NoteJsonData JsonData; // { get; private set; }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="note">ノート情報</param>
        /// <param name="judgeTime">判定時間</param>
        /// <param name="getNoteType"></param>
        protected NoteEntity(NoteJsonData note, float judgeTime, Func<NoteJsonData, int> getNoteType)
        {
            JsonData = note;
            IntType = getNoteType(note);

            LanePosition = note.HorizontalPosition.Numerator;
            Size = note.HorizontalSize;


            JudgeTime = judgeTime;
        }

        public virtual NoteJsonData MirrorJsonData()
        {
            return JsonData.Mirror();
        }


        private static T Create<T>(NoteJsonData note, float judgeTime) where T : NoteEntity
        {
            return (T)Activator.CreateInstance(typeof(T), note, judgeTime);
        }


        public virtual T Mirror<T>() where T : NoteEntity
        {
            var noteLineEntity = Create<T>(this.JsonData.Mirror(), JudgeTime);
            return noteLineEntity;
        }
    }
}
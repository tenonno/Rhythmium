using System;
using UnityEngine;

namespace Rhythmium
{
    /// <summary>
    /// ノート情報
    /// </summary>
    [Serializable]
    public abstract class NoteEntity : ScriptableObject
    {
        /// <summary>
        /// 整数型のノートタイプ
        /// </summary>
        [field: SerializeField]
        public int IntType { get; private set; }

        /// <summary>
        /// サイズ
        /// </summary>
        [field: SerializeField]
        public int Size { get; private set; }

        /// <summary>
        /// レーン位置
        /// </summary>
        [field: SerializeField]
        public int LanePosition { get; private set; }

        /// <summary>
        /// 判定時間
        /// </summary>
        [field: SerializeField]
        public float JudgeTime { get; private set; }

        public NoteJsonData JsonData { get; private set; }

        protected abstract int GetNoteType(NoteJsonData noteJsonData);

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="note">ノート情報</param>
        /// <param name="judgeTime">判定時間</param>
        public virtual void Initialize(NoteJsonData note, float judgeTime)
        {
            JsonData = note;
            IntType = GetNoteType(note);

            switch (note.horizontalPosition.Denominator)
            {
                case 24:
                    LanePosition = note.horizontalPosition.Numerator;
                    Size = note.horizontalSize;
                    break;
                case 6:
                    LanePosition = note.horizontalPosition.Numerator * 4;
                    Size = note.horizontalSize * 4;
                    break;
                default:
                    Debug.LogWarning($"ノーツの位置が不正です: {note}");
                    break;
            }

            JudgeTime = judgeTime;
        }

        protected virtual NoteJsonData MirrorJsonData()
        {
            return JsonData.Mirror();
        }

        public virtual T Mirror<T>() where T : NoteEntity
        {
            var noteEntity = CreateInstance<T>();
            noteEntity.Initialize(MirrorJsonData(), JudgeTime);
            return noteEntity;
        }
    }
}

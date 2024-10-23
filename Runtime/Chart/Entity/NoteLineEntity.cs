using System;
using UnityEngine;

namespace Rhythmium
{
    /// <summary>
    /// ノートライン情報
    /// </summary>
    public abstract class NoteLineEntity<TNoteEntity, TNoteType>
        where TNoteEntity : NoteEntity<TNoteType> where TNoteType : Enum
    {
        public readonly NoteLineJsonData JsonData;

        /// <summary>
        /// 先頭ノート
        /// </summary>
        public readonly TNoteEntity Head;

        /// <summary>
        /// 末尾ノート
        /// </summary>
        public readonly TNoteEntity Tail;

        public bool EnableBezier => JsonData.Bezier.Enable;
        public readonly Vector2 BezierValue;

        public bool EnableVerticalFit => JsonData.VerticalFit.Enable;

        protected NoteLineEntity(NoteLineJsonData jsonData, TNoteEntity head, TNoteEntity tail, bool isMirror)
        {
            JsonData = jsonData;
            Head = head;
            Tail = tail;

            var bezier = jsonData.Bezier.Value;
            if (isMirror)
            {
                bezier = new Vector2(1f - bezier.x, bezier.y);
            }

            BezierValue = bezier;
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rhythmium
{
    /// <summary>
    /// ノートライン情報
    /// </summary>
    public abstract class NoteLineEntity<TNoteEntity> where TNoteEntity : NoteEntity
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
        public Vector2 BezierValue => JsonData.Bezier.Value;

        protected NoteLineEntity(NoteLineJsonData jsonData, TNoteEntity head, TNoteEntity tail)
        {
            JsonData = jsonData;
            Head = head;
            Tail = tail;
        }

        private static T Create<T>(NoteLineJsonData jsonData, TNoteEntity head,
            TNoteEntity tail) where T : NoteLineEntity<TNoteEntity>
        {
            return (T)Activator.CreateInstance(typeof(T), jsonData, head, tail);
        }

        public virtual T Mirror<T>(IReadOnlyDictionary<TNoteEntity, TNoteEntity> mirroredNoteEntities)
            where T : NoteLineEntity<TNoteEntity>
        {
            var noteLineEntity = Create<T>(JsonData, mirroredNoteEntities[Head], mirroredNoteEntities[Tail]);
            return noteLineEntity;
        }
    }
}
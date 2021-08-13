using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rhythmium
{
    /// <summary>
    /// ノートライン情報
    /// </summary>
    [Serializable]
    public abstract class NoteLineEntity<TNoteEntity> : ScriptableObject where TNoteEntity : NoteEntity
    {
        [SerializeField] private NoteLineJsonData _jsonData;
        [SerializeField] private TNoteEntity _head;
        [SerializeField] private TNoteEntity _tail;

        public NoteLineJsonData JsonData => _jsonData;

        /// <summary>
        /// 先頭ノート
        /// </summary>
        public TNoteEntity Head => _head;

        /// <summary>
        /// 末尾ノート
        /// </summary>
        public TNoteEntity Tail => _tail;

        public void Initialize(NoteLineJsonData noteLine, TNoteEntity headNote, TNoteEntity tailNote)
        {
            _jsonData = noteLine;
            _head = headNote;
            _tail = tailNote;
        }

        public virtual T Mirror<T>(IReadOnlyDictionary<TNoteEntity, TNoteEntity> mirroredNoteEntities)
            where T : NoteLineEntity<TNoteEntity>
        {
            var noteLineEntity = CreateInstance<T>();
            noteLineEntity.Initialize(JsonData, mirroredNoteEntities[_head], mirroredNoteEntities[_tail]);
            return noteLineEntity;
        }
    }
}

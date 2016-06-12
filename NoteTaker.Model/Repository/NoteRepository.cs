using System.Collections.Concurrent;
using System.Collections.Generic;
using NoteTaker.Model.Data;

namespace NoteTaker.Model.Repository
{
    internal class NoteRepository : INoteRepository
    {
        private readonly ConcurrentDictionary<string, INote> notes = new ConcurrentDictionary<string, INote>(); 

        public void AddOrUpdate(INote note)
        {
            if (notes.ContainsKey(note.Id))
            {
                notes[note.Id] = note;
            }
            else
            {
                notes.TryAdd(note.Id, note);
            }
        }

        public bool Remove(string noteId)
        {
            INote removedNote;
            return notes.TryRemove(noteId, out removedNote);
        }

        public bool TryFind(string noteId, out INote note)
        {
            return notes.TryGetValue(noteId, out note);
        }

        public List<INote> GetAllNotes()
        {
            return new List<INote>(notes.Values);
        }
    }
}

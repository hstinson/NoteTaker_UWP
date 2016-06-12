using System;
using System.Collections.Generic;
using NoteTaker.Model.Data;
using NoteTaker.Model.Repository;

namespace NoteTaker.Model
{
    /// <summary>
    /// Provides event callbacks for when a note is added, updated, or deleted.  
    /// Provides ability to add, update, remove a note.
    /// </summary>
    internal class NotesMonitor : INotesMonitor
    {
        private readonly INoteRepository repository;
        private readonly NoteFactory factory = new NoteFactory();

        public event Action<INote> NoteAdded;
        public event Action<INote> NoteUpdated;
        public event Action<string> NoteDeleted;

        public NotesMonitor(INoteRepository repository)
        {
            this.repository = repository;
        }

        public void NewNote(NoteType type)
        {
            INote note = null;
            if (type == NoteType.RichText)
            {
                note = factory.CreateRichTextNote();
            }
            else if (type == NoteType.ToDo)
            {
                note = factory.CreateToDoListNote();
            }
            else
            {
                throw new NotImplementedException("Note type not implemented.");
            }

            repository.AddOrUpdate(note);
            OnNoteAdded(note);
        }

        public void AddNotes(List<INote> notes)
        {
            foreach(INote note in notes)
            {
                repository.AddOrUpdate(note);
                OnNoteAdded(note);
            }            
        }

        public void UpdateNote(INote note)
        {
            repository.AddOrUpdate(note);

            OnNoteUpdated(note);
        }

        public void DeleteNote(string noteId)
        {
            repository.Remove(noteId);

            OnNoteDeleted(noteId);
        }

        public void DuplicateNote(string noteId)
        {
            INote originalNote;
            if (repository.TryFind(noteId, out originalNote))
            {
                var duplicate = factory.CreateNewNoteFromExisting(originalNote);
                repository.AddOrUpdate(duplicate);

                OnNoteAdded(duplicate);
            }
        }

        public List<INote> GetAllNotes()
        {
            return repository.GetAllNotes();
        }

        protected virtual void OnNoteAdded(INote obj)
        {
            Action<INote> handler = NoteAdded;
            handler?.Invoke(obj);
        }

        protected virtual void OnNoteUpdated(INote obj)
        {
            Action<INote> handler = NoteUpdated;
            handler?.Invoke(obj);
        }

        protected virtual void OnNoteDeleted(string obj)
        {
            Action<string> handler = NoteDeleted;
            handler?.Invoke(obj);
        }
    }
}

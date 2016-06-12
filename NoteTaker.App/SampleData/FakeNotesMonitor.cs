using System;
using System.Collections.Generic;
using NoteTaker.Model;
using NoteTaker.Model.Data;

namespace NoteTaker.App.SampleData
{
    /// <summary>
    /// Fake notes monitor model class used during design time.
    /// </summary>
    class FakeNotesMonitor : INotesMonitor
    {
        public event Action<INote> NoteAdded;
        public event Action<INote> NoteUpdated;
        public event Action<string> NoteDeleted;

        private readonly List<INote> notes = new List<INote>();

        public FakeNotesMonitor()
        {
            notes.Add(new RichTextNote("1", "My First Note", new DateTime(2015, 6, 1, 7, 0, 0, DateTimeKind.Local), DateTime.Now, "My note text."));

            List<ToDoItem> items = new List<ToDoItem>()
            {
                new ToDoItem() {IsDone = true, Text="Take out trash"},
                new ToDoItem() {IsDone = false, Text="Read book"},
            };
            notes.Add(new ToDoListNote("2", "My To-Do List", new DateTime(2015, 6, 1, 7, 0, 0, DateTimeKind.Local), DateTime.Now, items));
        }

        public void NewNote(NoteType type)
        {

        }

        public void DuplicateNote(string noteId)
        {

        }

        public void UpdateNote(INote note)
        {

        }

        public void DeleteNote(string noteId)
        {

        }

        public List<INote> GetAllNotes()
        {
            return notes;
        }
    }
}

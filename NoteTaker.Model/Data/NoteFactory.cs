using System;
using System.Collections.Generic;

namespace NoteTaker.Model.Data
{
    internal class NoteFactory
    {
        private const string NewNoteDefaultTitle = "[Untitled]";

        public INote CreateRichTextNote()
        {
            DateTime now = DateTime.Now;
            return new RichTextNote(GenerateId(), NewNoteDefaultTitle, now, now, "Note content goes here.");
        }

        public INote CreateToDoListNote()
        {
            DateTime now = DateTime.Now;
            return new ToDoListNote(GenerateId(), NewNoteDefaultTitle, now, now, 
                new List<ToDoItem>(){new ToDoItem {Text = "Do something great!"}});
        }

        public INote CreateNewNoteFromExisting(INote note)
        {
            INote copy = null;

            const string formattedTitle = "[Copy] {0}";

            if (note.TypeOfNote == NoteType.RichText)
            {
                copy = new RichTextNote(GenerateId(), string.Format(formattedTitle, note.Title), note.DateCreated, note.DateLastModified, 
                    (note as RichTextNote).RichText);
            }
            else if (note.TypeOfNote == NoteType.ToDo)
            {
                copy = new ToDoListNote(GenerateId(), string.Format(formattedTitle, note.Title), note.DateCreated, note.DateLastModified,
                    new List<ToDoItem>((note as ToDoListNote).ToDoItems));
            }

            return copy;
        }

        private string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

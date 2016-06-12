using System;
using System.Linq;
using NoteTaker.Model;
using NoteTaker.Model.Data;

namespace NoteTaker.ViewModels.Note
{
    public class NoteViewModelFactory
    {
        private readonly INotesMonitor monitor;

        public NoteViewModelFactory(INotesMonitor monitor)
        {
            this.monitor = monitor;
        }

        public NoteViewModel CreateNoteViewModel(INote note)
        {
            NoteViewModel rv = null;
            if (note.TypeOfNote == NoteType.RichText)
            {
                rv = CreateRichTextNoteViewModel(note);
            }
            else if (note.TypeOfNote == NoteType.ToDo)
            {
                rv = CreateToDoListNoteViewModel(note);
            }
            else
            {
                throw new NotImplementedException(
                    string.Format("Create viewmodel note logic not implemented for type {0}.", note.TypeOfNote));
            }

            return rv;
        }

        private RichTextNoteViewModel CreateRichTextNoteViewModel(INote note)
        {
            RichTextNoteViewModel rv = null;

            RichTextNote richTextNote = (RichTextNote)note;
            if (richTextNote != null)
            {
                rv = new RichTextNoteViewModel(monitor, richTextNote.Id, richTextNote.DateCreated, richTextNote.DateLastModified,
                    richTextNote.Title, richTextNote.RichText);
            }

            return rv;
        }

        private ToDoNoteViewModel CreateToDoListNoteViewModel(INote note)
        {
            ToDoNoteViewModel rv = null;

            ToDoListNote toDo = (ToDoListNote)note;
            if (toDo != null)
            {
                var itemViewModels =
                    toDo.ToDoItems.Select(item => new ToDoItemViewModel() { IsDone = item.IsDone, Text = item.Text });
                rv = new ToDoNoteViewModel(monitor, toDo.Id, toDo.DateCreated, toDo.DateLastModified, toDo.Title, itemViewModels);
            }
            else
            {
                throw new ArgumentException("Mismatch between note type enumeration and actual note type.");
            }

            return rv;
        }
    }
}

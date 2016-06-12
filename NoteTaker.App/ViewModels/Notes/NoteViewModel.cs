using System;
using NoteTaker.Model;
using NoteTaker.ViewModels.Note;

namespace NoteTaker.ViewModels
{
    public enum NoteViewModelType
    {
        RichText,
        ToDo
    }

    public abstract class NoteViewModel : ValidatableViewModelBase
    {
        protected string title = "[Untitled]";
        /// <summary>
        /// Title of the note.
        /// </summary>
        public string Title
        {
            get { return title; }
            set
            {
                if (!Equals(value, title))
                {
                    title = value;
                    OnPropertyChanged();
                    HasChanges = true;

                    if (String.IsNullOrWhiteSpace(value))
                    {
                        AddErrorForProperty("Please enter a title.");
                    }
                    else
                    {
                        ClearErrorsFromProperty();
                    }
                }
            }
        }

        public abstract NoteViewModelType NoteType { get; }

        private readonly INotesMonitor monitor;
        private readonly string id;
        /// <summary>
        /// Unique ID of the note.
        /// </summary>
        public string Id { get { return id; } }

        /// <summary>
        /// Creation date of the note.
        /// </summary>
        public DateTime DateCreated { get; private set; }

        protected DateTime dateModified;
        /// <summary>
        /// Last time the note was modified.
        /// </summary>
        public DateTime DateModified
        {
            get { return dateModified; }
            protected set
            {
                dateModified = value;
                OnPropertyChanged();
            }
        }

        private bool hasChanges;
        /// <summary>
        /// Specifies whether this note has changes.
        /// </summary>
        public bool HasChanges
        {
            get { return hasChanges; }
            protected set
            {
                hasChanges = value;
                if (hasChanges)
                {
                    DateModified = DateTime.Now;
                    monitor.UpdateNote(this.ConvertToModel());
                }
            }
        }

        protected NoteViewModel(INotesMonitor monitor, string id, DateTime dateCreated)
        {
            this.monitor = monitor;
            this.id = id;
            DateCreated = dateCreated;
        }
    }
}

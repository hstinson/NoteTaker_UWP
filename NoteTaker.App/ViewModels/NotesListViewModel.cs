using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using NoteTaker.Model;
using NoteTaker.Model.Data;
using NoteTaker.ViewModels.Note;
using Windows.UI.Xaml.Data;

namespace NoteTaker.ViewModels
{
    /// <summary>
    /// ViewModel for manipulating notes.
    /// </summary>
    public class NotesListViewModel : ViewModelBase
    {
        private readonly INotesMonitor monitor;
        private readonly NoteViewModelFactory viewModelFactory;

        /// <summary>
        /// The collection of notes shown to the user.
        /// </summary>
        public ObservableCollection<NoteViewModel> Notes { get; private set; }

        public ICommand NewRichTextNoteCommand { get; private set; }
        public ICommand NewToDoListNoteCommand { get; private set; }
        public ICommand RemoveNoteCommand { get; private set; }
        public ICommand DuplicateNoteCommand { get; private set; }
        public ICommand ToggleSortDirectionCommand { get; private set; }
        
        private NoteViewModel currentNote;
        /// <summary>
        /// Current note being edited.  This will be updated when the user
        /// selects a new note from the notes collection.
        /// </summary>
        public NoteViewModel CurrentNote
        {
            get { return currentNote; }
            set
            {
                if (!Equals(currentNote, value))
                {
                    // Update the previous note
                    if (currentNote != null && currentNote.HasChanges)
                    {
                        monitor.UpdateNote(currentNote.ConvertToModel());
                    }

                    currentNote = value;
                    OnPropertyChanged();
                }
            }
        }

        public NotesListViewModel(INotesMonitor monitor)
        {
            this.monitor = monitor;
            viewModelFactory = new NoteViewModelFactory(monitor);

            Notes = new ObservableCollection<NoteViewModel>();

            NewRichTextNoteCommand = new RelayCommand(CreateNewRichTextNote);
            NewToDoListNoteCommand = new RelayCommand(CreateNewToDoListNote);
            RemoveNoteCommand = new RelayCommand<NoteViewModel>(RemoveNote);
            DuplicateNoteCommand = new RelayCommand<NoteViewModel>(DuplicateNote);

            foreach (INote noteData in monitor.GetAllNotes())
            {
                Notes.Add(viewModelFactory.CreateNoteViewModel(noteData));
            }

            monitor.NoteAdded += MonitorNoteAdded;
            monitor.NoteDeleted += MonitorNoteDeleted;
        }

        private void CreateNewRichTextNote()
        {
            monitor.NewNote(NoteType.RichText);
        }

        private void CreateNewToDoListNote()
        {
            monitor.NewNote(NoteType.ToDo);
        }

        private void DuplicateNote(NoteViewModel noteViewModel)
        {
            if (noteViewModel != null)
            {
                monitor.DuplicateNote(noteViewModel.Id);
            }           
        }

        private void RemoveNote(NoteViewModel noteViewModel)
        {
            if (noteViewModel != null)
            {
                monitor.DeleteNote(noteViewModel.Id);
            }
        }

        private void MonitorNoteDeleted(string noteId)
        {
            var noteToDelete = Notes.FirstOrDefault(model => Equals(model.Id, noteId));
            if (noteToDelete != null)
            {
                Notes.Remove(noteToDelete);
            }
        }

        private void MonitorNoteAdded(INote note)
        {
            var newViewModel = viewModelFactory.CreateNoteViewModel(note);
            Notes.Add(newViewModel);
        }
    }
}

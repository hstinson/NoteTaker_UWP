using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using NoteTaker.Model;

namespace NoteTaker.ViewModels
{
    public class ToDoNoteViewModel : NoteViewModel
    {
        public override NoteViewModelType NoteType => NoteViewModelType.ToDo;

        public ObservableCollection<ToDoItemViewModel> ToDoItems { get; } = new ObservableCollection<ToDoItemViewModel>();

        public ICommand AddToDoCommand { get; private set; }
        public ICommand RemoveSelectedToDoCommand { get; private set; }

        public ToDoNoteViewModel(
            INotesMonitor monitor,
            string id, 
            DateTime dateCreated, 
            DateTime dateModified, 
            string title, 
            IEnumerable<ToDoItemViewModel> items)
            : base(monitor, id, dateCreated)
        {
            this.dateModified = dateModified;
            this.title = title;

            AddToDoCommand = new RelayCommand(AddToDoItem);
            RemoveSelectedToDoCommand = new RelayCommand<ToDoItemViewModel>(RemoveToDoItem);

            foreach (var item in items)
            {
                ToDoItems.Add(item);
            }

            HasChanges = false;
        }

        private void AddToDoItem()
        {
            ToDoItems.Add(new ToDoItemViewModel { Text = "[Add text]" });
            HasChanges = true;
        }

        private void RemoveToDoItem(ToDoItemViewModel toDo)
        {
            ToDoItems.Remove(toDo);
            HasChanges = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Model.Data;

namespace NoteTaker.ViewModels.Note
{
    public static class NoteViewModelExtensionMethods
    {
        public static INote ConvertToModel(this NoteViewModel viewModel)
        {
            INote rv = null;

            if (viewModel is RichTextNoteViewModel)
            {
                rv = ConvertToModel(viewModel as RichTextNoteViewModel);
            }
            else if (viewModel is ToDoNoteViewModel)
            {
                rv = ConvertToModel(viewModel as ToDoNoteViewModel);
            }
            else
            {
                throw new NotImplementedException("ViewModel to model conversion not implemented.");
            }

            return rv;
        }

        private static INote ConvertToModel(RichTextNoteViewModel viewModel)
        {
            return new RichTextNote(viewModel.Id, viewModel.Title,
                viewModel.DateCreated, viewModel.DateModified,
                viewModel.RtfText);
        }

        private static INote ConvertToModel(ToDoNoteViewModel viewModel)
        {
            var items = viewModel.ToDoItems.Select(vm => new ToDoItem() { IsDone = vm.IsDone, Text = vm.Text });

            return new ToDoListNote(viewModel.Id, viewModel.Title,
                viewModel.DateCreated, viewModel.DateModified,
                items);
        }
    }
}

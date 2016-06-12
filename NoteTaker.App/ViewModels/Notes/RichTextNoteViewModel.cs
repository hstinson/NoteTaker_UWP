using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Model;

namespace NoteTaker.ViewModels
{
    public class RichTextNoteViewModel : NoteViewModel
    {
        public override NoteViewModelType NoteType => NoteViewModelType.RichText;

        private string rtfText;
        public string RtfText
        {
            get { return rtfText; }
            set
            {
                if (!Equals(value, rtfText))
                {
                    rtfText = value;
                    OnPropertyChanged();

                    HasChanges = true;
                }
            }
        }

        public RichTextNoteViewModel(INotesMonitor monitor, string id, DateTime dateCreated, DateTime dateModified, string title, string rtfText)
            : base(monitor, id, dateCreated)
        {
            this.dateModified = dateModified;
            this.title = title;
            this.rtfText = rtfText;
            HasChanges = false;
        }
    }
}

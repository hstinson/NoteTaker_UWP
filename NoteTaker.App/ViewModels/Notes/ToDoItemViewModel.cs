using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTaker.ViewModels
{
    public class ToDoItemViewModel : ViewModelBase
    {
        private bool isDone;
        public bool IsDone
        {
            get { return isDone; }
            set
            {
                if (!Equals(isDone, value))
                {
                    isDone = value;
                    OnPropertyChanged();
                }
            }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                if (!Equals(text, value))
                {
                    text = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}

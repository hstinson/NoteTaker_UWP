using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NoteTaker.ViewModels
{
    /// <summary>
    /// Provides commonly used logic for all view models.
    /// </summary>
    public abstract class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            RaisePropertyChanged(propertyName);
        }
    }
}

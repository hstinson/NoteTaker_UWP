using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NoteTaker.ViewModels
{
    /// <summary>
    /// View model base class with additional support for input validation.
    /// </summary>
    /// <remarks>
    /// Based on http://www.jonathanantoine.com/2011/09/18/wpf-4-5-asynchronous-data-validation/ and
    /// http://www.arrangeactassert.com/using-inotifydataerrorinfo-for-validation-in-mvvm-with-silverlight/ 
    /// </remarks>
    public abstract class ValidatableViewModelBase : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
        private readonly object errorMapLock = new object();

        #region INotifyDataErrorInfo Implementation

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get
            {
                return errors.Any(propErrors => propErrors.Value != null && propErrors.Value.Count > 0);
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            IEnumerable rv;

            if (!string.IsNullOrEmpty(propertyName))
            {
                if (errors.ContainsKey(propertyName) && (errors[propertyName] != null) && errors[propertyName].Count > 0)
                {
                    rv = errors[propertyName].ToList();
                }
                else
                {
                    rv = null;
                }
            }
            else
            {
                rv = errors.SelectMany(err => err.Value.ToList());
            }

            return rv;
        }

        #endregion

        public bool IsValid
        {
            get { return !HasErrors; }
        }

        protected void OnErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        protected void ClearErrorsFromProperty([CallerMemberName] string propertyName = null)
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                lock (errorMapLock)
                {
                    MakeOrCreatePropertyErrorList(propertyName);
                    errors[propertyName].Clear();
                }

                OnErrorsChanged(propertyName);
            }
        }

        protected void AddErrorForProperty(string error, [CallerMemberName] string propertyName = null)
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                lock (errorMapLock)
                {
                    MakeOrCreatePropertyErrorList(propertyName);
                    errors[propertyName].Add(error);
                }

                OnErrorsChanged(propertyName);
            }
        }

        private void MakeOrCreatePropertyErrorList(string propertyName)
        {
            if (!errors.ContainsKey(propertyName))
            {
                errors[propertyName] = new List<string>();
            }
        }
    }
}

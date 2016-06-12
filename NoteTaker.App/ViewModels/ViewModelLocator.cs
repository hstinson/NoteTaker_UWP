using GalaSoft.MvvmLight.Ioc;
using NoteTaker.App.SampleData;
using NoteTaker.Model;
using NoteTaker.Model.FileIO;
using NoteTaker.ViewModels;

namespace NoteTaker.Views.ViewModels
{
    /// <summary>
    /// Provides ViewModel instances for data-binding to the Views in the application.
    /// This class also creates and resolves any dependencies needed by the ViewModels.
    /// </summary>
    public class ViewModelLocator : ViewModelBase
    {
        private readonly DataFileSaver fileSaver;

        public NotesListViewModel NotesListVm
        {
            get { return SimpleIoc.Default.GetInstance<NotesListViewModel>(); }
        }

        public ViewModelLocator()
        {
            RegisterDependencies();
            fileSaver = SimpleIoc.Default.GetInstance<DataFileSaver>();
        }

        /// <summary>
        /// Initializes certain dependencies. Should be called once on application startup / resume.
        /// </summary>
        public void Initialize()
        {
            if (!IsInDesignMode)
            {
                PerformFileLoadSaveSetup();
            }
        }

        /// <summary>
        /// Cleans up dependencies. Should be called once during application exit / suspension.
        /// </summary>
        public override void Cleanup()
        {
            if (!IsInDesignMode)
            {
                fileSaver.CancelFileSaveInterval();
            }

            base.Cleanup();
        }

        private void RegisterDependencies()
        {
            if (!IsInDesignMode)
            {
                // Model registration
                ModelFactory modelFactory = new ModelFactory();
                SimpleIoc.Default.Register(modelFactory.GetMonitorInstance);
                SimpleIoc.Default.Register<DataFileLoader>(modelFactory.CreateDataFileLoader);
                SimpleIoc.Default.Register<DataFileSaver>(modelFactory.CreateFileSaver);
            }
            else
            {
                SimpleIoc.Default.Register<INotesMonitor, FakeNotesMonitor>();
            }

            // ViewModel registration
            SimpleIoc.Default.Register<NotesListViewModel>();     
        }

        private void PerformFileLoadSaveSetup()
        {
            // Load in saved notes, setup auto-save mechanism
            string dataFilePath = "SavedNotes.xml";

            DataFileLoader loader = SimpleIoc.Default.GetInstance<DataFileLoader>();
            loader.LoadDataIntoRepo(dataFilePath).ContinueWith(_ =>
            {
                fileSaver.SaveDataAtInterval(dataFilePath, 30);
            });
        }
    }
}

using NoteTaker.Model.FileIO;
using NoteTaker.Model.Repository;

namespace NoteTaker.Model
{
    /// <summary>
    /// Provides methods for creating instances of Model-based classes.
    /// </summary>
    public class ModelFactory
    {
        private readonly INoteRepository notesRepository = new NoteRepository();
        private readonly NotesMonitor notesMonitor;

        public ModelFactory()
        {
            notesMonitor = new NotesMonitor(notesRepository);
        }

        /// <summary>
        /// Returns the instance of the INotesMonitor implementation.
        /// </summary>
        /// <returns>Notes monitor</returns>
        public INotesMonitor GetMonitorInstance()
        {
            return notesMonitor;
        }

        /// <summary>
        /// Creates an instance of the DataFileLoader class.
        /// </summary>
        /// <returns>File loader</returns>
        public DataFileLoader CreateDataFileLoader()
        {
            return new DataFileLoader(notesMonitor);
        }

        /// <summary>
        /// Creates an instance of the DataFileSave class.
        /// </summary>
        /// <returns>File saver</returns>
        public DataFileSaver CreateFileSaver()
        {
            return new DataFileSaver(notesRepository);
        }
    }
}

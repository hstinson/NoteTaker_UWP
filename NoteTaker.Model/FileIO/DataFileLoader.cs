using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using NoteTaker.Model.Data;
using Windows.Storage;

namespace NoteTaker.Model.FileIO
{
    /// <summary>
    /// Loads data from disk into the repository.
    /// </summary>
    public class DataFileLoader
    {
        private readonly NotesMonitor monitor;

        internal DataFileLoader(NotesMonitor notesMonitor)
        {
            this.monitor = notesMonitor;
        }

        /// <summary>
        /// Loads data from a file into the repository.
        /// </summary>
        /// <param name="fileName">Name of file (includes extension) to read.</param>
        /// <returns>Awaitable task</returns>
        /// <remarks>This method saves the file into the current application's local folder.</remarks>
        public async Task LoadDataIntoRepo(string fileName)
        {
            DataFile dataFile = new DataFile
            {
                RichTextNotes = new List<RichTextNote>(), 
                ToDoNotes = new List<ToDoListNote>()
            };

            try
            {
                using (Stream readStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(fileName))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(DataFile));
                    dataFile = (DataFile)serializer.ReadObject(readStream);                   
                }
            }
            catch (Exception ex)
            {
                // Unable to load data file                
            }

            var allNotes = new List<INote>();
            allNotes.AddRange(dataFile.RichTextNotes);
            allNotes.AddRange(dataFile.ToDoNotes);

            monitor.AddNotes(allNotes);
        }
    }
}

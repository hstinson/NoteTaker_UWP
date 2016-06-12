using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using NoteTaker.Model.Data;
using NoteTaker.Model.Repository;
using Windows.Storage;

namespace NoteTaker.Model.FileIO
{
    /// <summary>
    /// Represents the data to be saved to disk.
    /// </summary>
    [DataContract]
    public class DataFile
    {
        [DataMember]
        public List<RichTextNote> RichTextNotes { get; set; }

        [DataMember]
        public List<ToDoListNote> ToDoNotes { get; set; }
    }

    /// <summary>
    /// Saves the notes in the repository to disk.
    /// </summary>
    public class DataFileSaver
    {
        private string fileName;
        private readonly INoteRepository repo;
        private CancellationTokenSource tokenSource;

        internal DataFileSaver(INoteRepository repo)
        {
            this.repo = repo;
        }
       
        /// <summary>
        /// Sets up saving the current state of the repository at an interval. Saving is performed on a different thread.
        /// </summary>
        /// <param name="fileName">Name of file (includes extension) in which to save contents</param>
        /// <param name="saveIntervalSeconds">Interval, in seconds, at which the file will be saved</param>
        public void SaveDataAtInterval(string fileName, int saveIntervalSeconds)
        {
            CancelFileSaveInterval();

            this.fileName = fileName;

            tokenSource = new CancellationTokenSource();
            SaveAtIntervalAsync(fileName, saveIntervalSeconds, tokenSource.Token);
        }
       
        /// <summary>
        /// Cancels the file saving interval.
        /// </summary>
        public void CancelFileSaveInterval()
        {
            tokenSource?.Cancel();
        }

        private async Task SaveData(string fileName)
        {
            var dataFile = CreateDataFile(repo);
            await SaveDataFileToDisk(fileName, dataFile);
        }

        private async void SaveAtIntervalAsync(string fileName, int saveIntervalSeconds, CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await SaveData(fileName);
                    await Task.Delay(saveIntervalSeconds * 1000, token);
                }
            }
            catch (TaskCanceledException) { }           
        }

        private static DataFile CreateDataFile(INoteRepository repo)
        {
            var notes = repo.GetAllNotes();
            var dataFile = new DataFile
            {
                RichTextNotes = notes.Where(note => note.TypeOfNote == NoteType.RichText)
                    .Cast<RichTextNote>()
                    .ToList(),
                ToDoNotes = notes.Where(note => note.TypeOfNote == NoteType.ToDo)
                    .Cast<ToDoListNote>()
                    .ToList()
            };
            return dataFile;
        }

        private async Task SaveDataFileToDisk(string fileName, DataFile data)
        {
            try
            {
                StorageFile savedDataFile = await ApplicationData.Current.LocalFolder
                    .CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                using (Stream writeStream = await savedDataFile.OpenStreamForWriteAsync())
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(DataFile));
                    serializer.WriteObject(writeStream, data);
                }
            }
            catch (Exception ex)
            {
                // Unable to save data file
            }
        }
    }
}

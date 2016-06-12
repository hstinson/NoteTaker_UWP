using System;
using System.Collections.Generic;
using NoteTaker.Model.Data;

namespace NoteTaker.Model
{
    /// <summary>
    /// Provides access to note-related management capabilities.
    /// </summary>
    public interface INotesMonitor
    {
        /// <summary>
        /// Subscribers are notified when a note has been added.
        /// </summary>
        event Action<INote> NoteAdded;

        /// <summary>
        /// Subscribers are notified when a note has been updated.
        /// </summary>
        event Action<INote> NoteUpdated;

        /// <summary>
        /// Subscribers are notified when a note has been deleted.
        /// </summary>
        event Action<string> NoteDeleted;

        /// <summary>
        /// Creates a new note of the specified type.
        /// </summary>
        /// <param name="type">Note type to create</param>
        void NewNote(NoteType type);

        /// <summary>
        /// Duplicates an existing note
        /// </summary>
        /// <param name="noteId">ID of note to duplicate</param>
        void DuplicateNote(string noteId);

        /// <summary>
        /// Updates an existing note.
        /// </summary>
        /// <param name="note">Note to update</param>
        void UpdateNote(INote note);

        /// <summary>
        /// Deletes a note.
        /// </summary>
        /// <param name="noteId">ID of note to delete</param>
        void DeleteNote(string noteId);

        /// <summary>
        /// Retrieves all notes
        /// </summary>
        /// <returns>Notes</returns>
        List<INote> GetAllNotes();
    }
}
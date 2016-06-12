using System.Collections.Generic;
using NoteTaker.Model.Data;

namespace NoteTaker.Model.Repository
{
    /// <summary>
    /// Repository for all notes in the application. Implementations are expected to be thread safe.
    /// </summary>
    internal interface INoteRepository
    {
        /// <summary>
        /// Adds or updates a note.
        /// </summary>
        /// <param name="note">Note to add or update</param>
        void AddOrUpdate(INote note);

        /// <summary>
        /// Removes a note from the repository.
        /// </summary>
        /// <param name="noteId">ID of note to remove</param>
        /// <returns>True if remove was succesful; false otherwise</returns>
        bool Remove(string noteId);

        /// <summary>
        /// Returns a note with the given ID if it exists in the repo.
        /// </summary>
        /// <param name="noteId">ID of note to retrieve</param>
        /// <param name="note">Note</param>
        /// <returns>True if note was found; false otherwise</returns>
        bool TryFind(string noteId, out INote note);

        /// <summary>
        /// Gets all notes currently stored in the repository.
        /// </summary>
        /// <returns>Notes collection</returns>
        List<INote> GetAllNotes();
    }
}
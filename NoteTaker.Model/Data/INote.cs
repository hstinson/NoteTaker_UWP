using System;

namespace NoteTaker.Model.Data
{
    public enum NoteType
    {
        RichText,
        ToDo
    }

    public interface INote
    {
        /// <summary>
        /// Unique Id of the note.  The service handles creating Id's
        /// for new notes.  The client should not set this value.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Title of the note.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Date the note was created.  This value should
        /// only be set by the service.
        /// </summary>
        DateTime DateCreated { get; }

        /// <summary>
        /// Time the note was last updated.
        /// </summary>
        DateTime DateLastModified { get; }

        /// <summary>
        /// Type of note.
        /// </summary>
        NoteType TypeOfNote { get; }
    }
}
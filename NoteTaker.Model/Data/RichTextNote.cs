using System;
using System.Runtime.Serialization;

namespace NoteTaker.Model.Data
{
    /// <summary>
    /// Data representing a rich text note.  This class is immutable.
    /// </summary>
    [DataContract]
    public class RichTextNote : INote
    {
        /// <summary>
        /// Unique Id of the note.
        /// </summary>
        [DataMember]
        public string Id { get; private set; }

        /// <summary>
        /// Title of the note.
        /// </summary>
        [DataMember]
        public string Title { get; private set; }

        /// <summary>
        /// Date the note was created.  This value should
        /// only be set by the service.
        /// </summary>
        [DataMember]
        public DateTime DateCreated { get; private set; }

        /// <summary>
        /// Time the note was last updated.
        /// </summary>
        [DataMember]
        public DateTime DateLastModified { get; private set; }

        public NoteType TypeOfNote => NoteType.RichText;

        /// <summary>
        /// Rich text note content.
        /// </summary>
        [DataMember]
        public string RichText { get; private set; }

        public RichTextNote(string id, string title, DateTime createdDate, DateTime modifiedDate, string text)
        {
            Id = id;
            Title = title;
            DateCreated = createdDate;
            DateLastModified = modifiedDate;
            RichText = text;
        }

        /// <summary>
        /// Used for serialization/de-serialization.
        /// </summary>
        internal RichTextNote()
        {
            
        }
    }
}

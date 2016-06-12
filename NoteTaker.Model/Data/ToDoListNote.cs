using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NoteTaker.Model.Data
{
    [DataContract]
    public class ToDoItem
    {
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public bool IsDone { get; set; }
    }

    [DataContract]
    public class ToDoListNote : INote
    {
        [DataMember]
        public string Id { get; private set; }

        [DataMember]
        public string Title { get; private set; }

        [DataMember]
        public DateTime DateCreated { get; private set; }

        [DataMember]
        public DateTime DateLastModified { get; private set; }


        public NoteType TypeOfNote => NoteType.ToDo;

        [DataMember]
        public List<ToDoItem> ToDoItems { get; private set; } 

        public ToDoListNote(string id, string title, DateTime createdDate, DateTime modifiedDate, IEnumerable<ToDoItem> todos)
        {
            Id = id;
            Title = title;
            DateCreated = createdDate;
            DateLastModified = modifiedDate;
            ToDoItems = new List<ToDoItem>(todos);
        }

        /// <summary>
        /// Used for serialization/de-serialization.
        /// </summary>
        internal ToDoListNote()
        {
            
        }
    }
}

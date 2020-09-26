using System;

namespace Model.Interface
{
    public interface IMeta : IEntity
    {
        public string Title { get; set; }
        public string AuthorFullName { get; set; }
        public DateTime LastEditedTimestamp { get; set; }
    }
}
using System;
using Model.Interface;

namespace Model
{
    public class Article : IArticle
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        // TODO: add relational author table
        public string AuthorFullName { get; set; }

        public string Body { get; set; }

        // TODO: add version control instead maybe as future work
        public DateTime LastEditedTimestamp { get; set; }
        // TODO: add relational tag logic
    }
}
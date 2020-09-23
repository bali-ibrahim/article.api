using System;

namespace Model
{
    public class Article : IArticle
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string AuthorFullName { get; set; }
        public string Body { get; set; }
        public DateTime? CreatedTimestamp { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }
    }
}
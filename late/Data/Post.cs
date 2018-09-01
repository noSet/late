using System;

namespace late.Data
{
    public class Post
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Time { get; set; }

        public virtual Catalog Catalog { get; set; }
    }
}

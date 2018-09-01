using System;
using System.Collections.Generic;

namespace late.Data
{
    public class Catalog
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public int PRI { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}

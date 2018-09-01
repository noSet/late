using System;
using System.Collections.Generic;
using late.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace late.Models
{
    public class PostCreateViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public Guid Catalog { get; set; }

        public IEnumerable<SelectListItem> CatalogSelectItems { get; set; }       
    }
}

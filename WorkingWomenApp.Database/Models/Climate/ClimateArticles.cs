using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingWomenApp.Database.Core;

namespace WorkingWomenApp.Database.Models.Climate
{

        public class Article:Entity
        {
                         
            public string Title { get; set; }                   // Article title
            public string Author { get; set; }                  // Author name
            public string Summary { get; set; }                 // Short summary
            public string Content { get; set; }                 // Full article content
            public DateTime PublishedDate { get; set; }         // When the article was published
            public string Source { get; set; }                  // Source of the article
            public  ArticleCategory  Category{ get; set; }                // e.g., "Climate Change", "Renewable Energy"
            public string ImageUrl { get; set; }                // Optional image URL
            public bool IsFeatured { get; set; }                // Whether to feature this article
        }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingWomenApp.Database.Core;
using WorkingWomenApp.Database.enums;

namespace WorkingWomenApp.Database.Models.Climate
{

        public class Article: Entity
    {
                         
            public string Title { get; set; }                   // Article title
            public string Author { get; set; }                  // Author name
            public string Summary { get; set; }                 // Short summary
            public string Content { get; set; }                 // Full article content
            public DateTime PublishedDate { get; set; }         
                         
            public  ArticleCategory  Category{ get; set; }                
            public string ImageUrl { get; set; }                
            public bool IsFeatured { get; set; }                
        }
    
}

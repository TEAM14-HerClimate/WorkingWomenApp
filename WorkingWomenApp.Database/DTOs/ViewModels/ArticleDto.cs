using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingWomenApp.Database.Models;

namespace WorkingWomenApp.Database.DTOs.ViewModels
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }                   // Article title
        public string Author { get; set; }                  // Author name
        public string Summary { get; set; }                 // Short summary
        public string Content { get; set; }                 // Full article content
        public DateTime PublishedDate { get; set; }

        public ArticleCategory Category { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFeatured { get; set; }
    }
}

using Feature.ContentScrap.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebScraperLite.Models
{
    public class StatsModel
    {
        [Required(ErrorMessage = "Url field is required")]
        [StringLength(100, ErrorMessage = "Url should be less than or equal to 100 character")]
        [Url(ErrorMessage = "Please enter a valid url")]
        public string Url { get; set; }
        public List<string> Images { get; set; }
        public DocumentStats documentStats { get; set; }
    }
}

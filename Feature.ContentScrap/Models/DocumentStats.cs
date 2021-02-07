using System.Collections.Generic;

namespace Feature.ContentScrap.Models
{
    public class DocumentStats
    {
        public int TotalCount { get; set; }
        public Dictionary<string, int> CountByWords { get; set; }
    }
}

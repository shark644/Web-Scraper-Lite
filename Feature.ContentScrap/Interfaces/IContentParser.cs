using Feature.ContentScrap.Models;
using System.Collections.Generic;

namespace Feature.ContentScrap
{
    public interface IContentParser
    {
        DocumentStats GetDocumentStats(string url);
        List<string> GetImages(string url);
    }
}
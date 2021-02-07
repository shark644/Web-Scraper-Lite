using Feature.ContentScrap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Feature.ContentScrap
{
    public class ContentParser : IContentParser
    {
        IHtmlService _htmlService;

        public ContentParser(IHtmlService htmlService)
        {
            _htmlService = htmlService;
        }

        public List<string> GetImages(string url)
        {
            var images = new List<string>();

            var document = _htmlService.GetDocument(new Uri(url));

            if (document.DocumentNode != null && document.DocumentNode.HasChildNodes)
            {
                var imageUrls = document.DocumentNode.Descendants("img")
                                                .Select(e => e.GetAttributeValue("src", null))
                                                .Where(s => !String.IsNullOrEmpty(s));

                images.AddRange(imageUrls);
            }

            return images;
        }

        public DocumentStats GetDocumentStats(string url)
        {
            var documentStats = new DocumentStats();

            var words = GetWords(url);

            documentStats.TotalCount = words.Count;

            documentStats.CountByWords = words.GroupBy(x => x, StringComparer.InvariantCultureIgnoreCase)
                .OrderByDescending(x => x.Count())
                .Take(10)
                .ToDictionary(x => x.Key.ToLower(), x => x.Count());

            return documentStats;
        }

        private List<string> GetWords(string url)
        {
            var allWords = new List<string>();
            Regex regex = new Regex("[^a-zA-Z0-9 -]");

            var document = _htmlService.GetDocument(new Uri(url));
            if (document.DocumentNode != null && document.DocumentNode.HasChildNodes)
            {
                var nodes = document.DocumentNode
                .SelectNodes("//body//text()[not(parent::script)]");
                var texts = new List<string>();
                if (nodes != null && nodes.Count > 0)
                {
                    texts = nodes.Select(node => node.InnerText).ToList();
                }

                foreach (string text in texts)
                {
                    var cleanText = regex.Replace(text, "");

                    var words = cleanText.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Where(s => Char.IsLetter(s[0]));

                    int wordCount = words.Count();

                    if (wordCount > 0)
                    {
                        allWords.AddRange(words);
                    }
                }
            }

            return allWords;
        }
    }
}
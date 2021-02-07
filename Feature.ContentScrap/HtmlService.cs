using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System;

namespace Feature.ContentScrap
{
    public class HtmlService : IHtmlService
    {
        private readonly ILogger<HtmlService> _logger;

        public HtmlService(ILogger<HtmlService> logger)
        {
            _logger = logger;
        }

        public HtmlDocument GetDocument(Uri uri)
        {
            var document = new HtmlDocument();
            try
            {
                HtmlWeb web = new HtmlWeb();

                document = web.Load(uri);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error while loading Html Docuemnt", ex);
            }

            return document;
        }
    }
}

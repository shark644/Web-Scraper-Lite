using HtmlAgilityPack;

namespace Feature.ContentScrap.Test
{
    internal class MockDocumentProvider
    {
        internal HtmlDocument GetHtmlDocument_Empty()
        {
            var document = new HtmlDocument();
            var node = HtmlNode.CreateNode("<html><head></head><body></body></html>");
            document.DocumentNode.AppendChild(node);
            return document;
        }
        internal HtmlDocument GetHtmlDocument_WithTexts()
        {
            var document = new HtmlDocument();
            var node = HtmlNode.CreateNode("<html><head></head><body>" +
                "<h1>Unit testing</h1>" +
                "<p>The goal of unit testing is to isolate each part of the program</p>" +
                "<p>unit and another unit</p>" +
                "<p>software, software, software</p>" +
                "</body></html>");
            document.DocumentNode.AppendChild(node);
            return document;
        }

        internal HtmlDocument GetHtmlDocument_WithImages()
        {
            var document = new HtmlDocument();
            var node = HtmlNode.CreateNode("<html><head></head><body>" +
                "<img src='/testimage.jpg' />" +
                "</body></html>");
            document.DocumentNode.AppendChild(node);
            return document;
        }
    }
}

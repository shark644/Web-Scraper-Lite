using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Feature.ContentScrap.Test
{
    [TestClass]
    public class ImageParserTests
    {
        const string url = "https://www.microsoft.com/";
        Mock<IHtmlService> htmlService;
        MockDocumentProvider mockDocumentProvider;

        [TestInitialize]
        public void Initialize()
        {
            htmlService = new Mock<IHtmlService>();
            mockDocumentProvider = new MockDocumentProvider();
        }

        [TestMethod]
        [Owner("TeamX")]
        public void WhenDocumentIsEmpty_GetImagesCount_ShouldReturnZeroCount()
        {
            htmlService.Setup(f => f.GetDocument(It.IsAny<Uri>())).Returns(mockDocumentProvider.GetHtmlDocument_Empty());

            IContentParser parser = new ContentParser(htmlService.Object);
            var images = parser.GetImages(url);

            Assert.IsTrue(images.Count == 0, "Images count should be 0");
        }
        
        [TestMethod]
        [Owner("TeamX")]
        public void WhenDocumentIsEmpty_GetTextsCount_ShouldReturnZeroCount()
        {
            htmlService.Setup(f => f.GetDocument(It.IsAny<Uri>())).Returns(mockDocumentProvider.GetHtmlDocument_Empty());

            IContentParser parser = new ContentParser(htmlService.Object);
            var stats = parser.GetDocumentStats(url);

            Assert.IsTrue(stats.TotalCount == 0, "Words count should be 0");
        }

        [TestMethod]
        [Owner("TeamX")]
        public void WhenDocumentIsNotEmpty_GetImagesCount_ShouldReturnValidCount()
        {
            htmlService.Setup(f => f.GetDocument(It.IsAny<Uri>())).Returns(mockDocumentProvider.GetHtmlDocument_WithImages());

            IContentParser parser = new ContentParser(htmlService.Object);
            var images = parser.GetImages(url);

            Assert.IsTrue(images.Count > 0, "Images count should not be 0");
        }

        [TestMethod]
        [Owner("TeamX")]
        public void WhenDocumentIsNotEmpty_GetTextsCount_ShouldReturnValidCount()
        {
            htmlService.Setup(f => f.GetDocument(It.IsAny<Uri>())).Returns(mockDocumentProvider.GetHtmlDocument_WithTexts());

            IContentParser parser = new ContentParser(htmlService.Object);
            var stats = parser.GetDocumentStats(url);

            Assert.IsNotNull(stats, "Html document stats not valid");
            Assert.IsTrue(stats.TotalCount > 0, "Words count should not be 0");
        }

        [TestMethod]
        [Owner("TeamX")]
        public void WhenDocumentIsNotEmpty_GetTopTextsCount_ShouldReturnValidCount()
        {
            htmlService.Setup(f => f.GetDocument(It.IsAny<Uri>())).Returns(mockDocumentProvider.GetHtmlDocument_WithTexts());

            IContentParser parser = new ContentParser(htmlService.Object);
            var stats = parser.GetDocumentStats(url);

            Assert.IsNotNull(stats, "Html document stats not valid");
            Assert.IsTrue(stats.CountByWords.Count > 0, "Top Words count should not be 0");

            var unitWord = stats.CountByWords["unit"];
            Assert.IsTrue(unitWord == 4, "count of word \"unit\" should be 4");

            var softwareWord = stats.CountByWords["software"];
            Assert.IsTrue(softwareWord == 3, "count of word \"software\" should be 4");
        }
    }
}

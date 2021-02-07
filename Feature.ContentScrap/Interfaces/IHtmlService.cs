using HtmlAgilityPack;
using System;

namespace Feature.ContentScrap
{
    public interface IHtmlService
    {
        HtmlDocument GetDocument(Uri uri);
    }
}
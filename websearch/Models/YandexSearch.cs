using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using websearch.Interfaces;

namespace websearch.Models
{
    class YandexSearch : IWebSearchEnginable
    {
        const string ApiKey = "03.320766794:80de4f9f9c0b3703e7f9b6129e896aec";
        const string User = "anurself";
        public Page Search(string input)
        {
            string url = @"https://yandex.ru/search/xml?user=" + User + "&key=" + ApiKey + "&query=" + input;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            XmlReader xmlReader = XmlReader.Create(response.GetResponseStream());
            XDocument xmlResponse = XDocument.Load(xmlReader);

            CheckForError(xmlResponse);
            List<Page> pagesList = ParseXml(xmlResponse);

            var firstItem = pagesList.FirstOrDefault();
            if (firstItem != null)
                return new Page(firstItem.Title, firstItem.Link);

            throw new ProgramException("NotFoundError") { ExtraErrorInfo = "No match data - Please input another query" };;
        }

        private string GetValue(XElement group, string name)
        {
            try
            {
                return group.Element("doc").Element(name).Value;
            }
            catch
            {
                return string.Empty;
            }
        }

        private List<Page> ParseXml(XDocument xmlResponse)
        {
            List<Page> pagesList = new List<Page>();

            var groupQuery = from gr in xmlResponse.Elements().
               Elements("response").
               Elements("results").
               Elements("grouping").
               Elements("group")
                             select gr;

            for (int i = 0; i < groupQuery.Count(); i++)
            {
                string urlQuery = GetValue(groupQuery.ElementAt(i), "url");
                string titleQuery = GetValue(groupQuery.ElementAt(i), "title");
                pagesList.Add(new Page(titleQuery, urlQuery));
            }
            return pagesList;
        }

        private void CheckForError(XDocument xmlResponse)
        {
            var groupQuery = from gr in xmlResponse.Elements().
               Elements("response").
               Elements("error")
                             select gr;
            for (int i = 0; i < groupQuery.Count(); i++)
            {
                string errorCode = groupQuery.ElementAt(i).Attribute("code").Value;
                if (!string.IsNullOrEmpty(errorCode))
                {
                    var message = groupQuery.ElementAt(i).Value;
                    throw new ProgramException("IPError") { ExtraErrorInfo = message };
                }
            }
        }

    }
}

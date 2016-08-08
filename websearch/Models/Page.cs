using System;

namespace websearch.Models
{
    class Page
    {
        public Page(String title, String link)
        {
            Title = title;
            Link = link;
        }

        public String Title { get; set; }
        public String Link { get; set; }
        public override string ToString()
        {
            return "Title : " + Title + Environment.NewLine + "Link : " + Link + Environment.NewLine + Environment.NewLine;
        }
    }
}

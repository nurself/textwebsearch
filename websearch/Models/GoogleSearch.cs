using System.Linq;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using websearch.Interfaces;

namespace websearch.Models
{
    class GoogleSearch : IWebSearchEnginable
    {
        const string ApiKey = "AIzaSyDIm9ZOWD8Zd-2tHy5r3c0R-_XjdEFaXGE";
        const string SearchEngineId = "003470263288780838160:ty47piyybua";

        public Page Search(string input)
        {
            CustomsearchService customSearchService = new CustomsearchService(new Google.Apis.Services.BaseClientService.Initializer { ApiKey = ApiKey });
            CseResource.ListRequest listRequest = customSearchService.Cse.List(input);
            listRequest.Cx = SearchEngineId;
            Search search = listRequest.Execute();
            var firstItem = search.Items.FirstOrDefault();
            if (firstItem != null)
                return new Page(firstItem.Title, firstItem.Link);

            throw new ProgramException("NotFoundError") { ExtraErrorInfo = "No match data - Please input another query" };
        }
    }
}

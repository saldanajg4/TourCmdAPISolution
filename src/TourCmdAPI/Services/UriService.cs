using System;
using Microsoft.AspNetCore.WebUtilities;
using TourCmdAPI.Filter;

namespace TourCmdAPI.Services
{
    public class UriService : IUriServices
    {
        private readonly string baseUri;//to get base(loclahost, api.com etc) dependency injection 

        public UriService()
        {
        }

        //from startup class
        public UriService(string baseUri)
        {
            this.baseUri = baseUri;

        }
        //building the uri string for next and previous pages
        public Uri GetPageUri(PaginationFilter filter, string route)
        {
            var endPointUri = new Uri(string.Concat(this.baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(endPointUri.ToString(),"pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize" , filter.PageSize.ToString());

            return new Uri(modifiedUri);
        }

    }
}
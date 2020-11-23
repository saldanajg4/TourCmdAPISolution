using System;
using TourCmdAPI.Filter;

namespace TourCmdAPI.Services
{
    public interface IUriServices
    {
         Uri GetPageUri(PaginationFilter filter, string route);
    }
}
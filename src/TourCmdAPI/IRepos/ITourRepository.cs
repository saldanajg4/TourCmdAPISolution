using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourCmdAPI.Entities;

namespace TourCmdAPI.IRepos{
    public interface ITourRepository{
        Task<IEnumerable<Tour>> GetTours(bool includeShows = false);
        Task<IEnumerable<Tour>> GetTourById(Guid id);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourCmdAPI.Entities;

namespace TourCmdAPI.IRepos{
    public interface ITourRepository{
        Task<IEnumerable<Tour>> GetTours(bool includeShows = false);
        Task<Tour> GetTourById(Guid tourId, bool includeShows = false);
        Task AddTour(Tour tour);
        Task<bool> SaveAsync();
        Task<IEnumerable<Band>> GetBands();
        Task<IEnumerable<Manager>> GetManagers();
    }
}
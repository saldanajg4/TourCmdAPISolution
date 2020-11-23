using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TourCmdAPI.IRepos
{
    public abstract class IRepositoryBase<T> where T : class
    {
        protected abstract Task<IEnumerable<T>> FindAll();
        protected abstract Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression);
        protected abstract void Create(T entity);



        //  Task<IEnumerable<Tour>> GetTours(int pageNumber, int pageSize, bool includeShows = false);
        // Task<Tour> GetTourById(Guid tourId, bool includeShows = false);
        // Task AddTour(Tour tour);
        // Task<bool> SaveAsync();
    }
}
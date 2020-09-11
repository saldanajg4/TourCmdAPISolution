using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourCmdAPI.Services;
using TourCmdAPI.Entities;
using TourCmdAPI.IRepos;

namespace TourCmdAPI.Repos
{
    public class TourRepository : ITourRepository
    {
        public TourContext _context { get; set; }

        public TourRepository(TourContext ctx)
        {
            _context = ctx;
        }

        public async Task<IEnumerable<Tour>> GetTours(bool includeShows = false)
        {
            if(includeShows){
                return await _context.Tours.Include(t => t.Band).Include(t => t.Shows).ToListAsync();
            }
            else{
                return await _context.Tours.Include(t => t.Band).ToListAsync();
            }
        }
    }
}
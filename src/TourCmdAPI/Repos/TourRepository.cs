using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourCmdAPI.Services;
using TourCmdAPI.Entities;
using TourCmdAPI.IRepos;
using System.Linq;
using System;

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
            // includeShows = true;
            if(includeShows){
                return await _context.Tours.Include(t => t.Band).Include(t => t.Shows).ToListAsync();
            }
            else{
                return await _context.Tours.Include(t => t.Band).ToListAsync();
            }
        }
        public async Task<Entities.Tour> GetTourById(Guid id, bool includeShows = false){
            if(includeShows){
                return await _context.Tours.Include(t => t.Band).Include(t => t.Shows)
                    .Where(t => t.TourId == id).FirstOrDefaultAsync();
            }
            else{
                 return await _context.Tours.Include(b => b.Band)
                    .Where(t => t.TourId == id).FirstOrDefaultAsync();
            }
            
        }

        public async Task AddTour(Tour tour)
        {
            await _context.Tours.AddAsync(tour);
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<IEnumerable<Band>> GetBands()
        {
            return await _context.Bands.ToListAsync();
        }

        public async Task<IEnumerable<Manager>> GetManagers()
        {
            return await _context.Managers.ToListAsync();
        }
    }
}
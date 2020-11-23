using System.Collections.Generic;

namespace TourCmdAPI.Dtos
{
    //Now add this class to the Automapping profile
    public class TourWithShows : Tour
    {
        public ICollection<Show> Shows { get; set; }
            = new List<Show>();
    }
}
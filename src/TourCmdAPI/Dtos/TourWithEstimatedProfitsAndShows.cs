using System.Collections.Generic;

namespace TourCmdAPI.Dtos
{
    public class TourWithEstimatedProfitsAndShows : TourWithEstimatedProfits
    {
        public ICollection<Show> Shows { get; set; }
            = new List<Show>();
    }
}
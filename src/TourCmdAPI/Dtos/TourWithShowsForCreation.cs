using System.Collections.Generic;

namespace TourCmdAPI.Dtos
{
    public class TourWithShowsForCreation : TourForCreation
    {
        public ICollection<ShowForCreation> Shows { get; set; }
            = new List<ShowForCreation>();
    }
}
using System.Collections;
using System.Collections.Generic;

namespace TourCmdAPI.Dtos
{
    public class TourWithManagerAndShowsForCreation : TourWithManagerForCreation
    {
        public ICollection<ShowForCreation> Shows { get; set; }
            = new List<ShowForCreation>();
        
    }
}
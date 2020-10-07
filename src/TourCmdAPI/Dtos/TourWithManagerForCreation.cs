using System;

namespace TourCmdAPI.Dtos
{
    public class TourWithManagerForCreation : TourForCreation
    {
        public Guid ManagerId { get; set; }
    }
}
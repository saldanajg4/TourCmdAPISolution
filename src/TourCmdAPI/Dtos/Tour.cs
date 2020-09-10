using System;

namespace TourCmdAPI.Dtos
{
     public class Tour : TourAbstractBase
    {
        public Guid TourId { get; set; }
        public string Band { get; set; }   
    }
}
using AutoMapper;

namespace TourCmdAPI.Entities
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Entities.Tour, Dtos.Tour>();
        }
    }
}
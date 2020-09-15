using AutoMapper;

namespace TourCmdAPI.Entities
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Entities.Tour, Dtos.Tour>()
                .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));
            // CreateMap<Entities.Tour, Dtos.Tour>();

            CreateMap<Entities.Tour, Dtos.TourWithEstimatedProfits>()
                .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));
        }
    }
}
using AutoMapper;

namespace TourCmdAPI.Entities
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            //mapping for touring
            CreateMap<Entities.Tour, Dtos.Tour>()
                .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));
            CreateMap<Entities.Tour, Dtos.TourWithEstimatedProfits>()
                .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));
            CreateMap<Entities.Band, Dtos.Band>();
            CreateMap<Entities.Manager, Dtos.Manager>();
            CreateMap<Entities.Show, Dtos.Show>();
            CreateMap<Entities.Tour, Dtos.TourWithShows>()
                .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));
            CreateMap<Entities.Tour, Dtos.TourWithEstimatedProfitsAndShows>()
                .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));
            
            CreateMap<Dtos.TourWithShowsForCreation, Entities.Tour>();
            CreateMap<Dtos.TourWithManagerAndShowsForCreation, Entities.Tour>();
            CreateMap<Dtos.ShowForCreation, Entities.Show>();
                

            //mapping for Ordering
            CreateMap<Entities.Order, Dtos.Order>()
                .ForMember(d => d.Employee, o => o.MapFrom(s => s.Employee.EmployeeName));
            CreateMap<Entities.Order, Dtos.OrderWithItems>()
                .ForMember(d => d.Employee, o => o.MapFrom(s => s.Employee.EmployeeName));
            CreateMap<Entities.Employee, Dtos.Employee>();
            CreateMap<Entities.Item, Dtos.Item>();

            //mapping for input tour
            CreateMap<Dtos.TourForCreation, Entities.Tour>();
            CreateMap<Dtos.TourWithManagerForCreation, Entities.Tour>();


        }
    }
}
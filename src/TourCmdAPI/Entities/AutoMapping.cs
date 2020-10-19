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
            
            //mapping for input tour, for http post creation of objects
            CreateMap<Dtos.TourWithShowsForCreation, Entities.Tour>();
            CreateMap<Dtos.TourWithManagerAndShowsForCreation, Entities.Tour>();
            CreateMap<Dtos.ShowForCreation, Entities.Show>();
             CreateMap<Dtos.TourForCreation, Entities.Tour>();
            CreateMap<Dtos.TourWithManagerForCreation, Entities.Tour>();
            CreateMap<Dtos.ItemForCreation, Entities.Item>();
                

            //mapping for Ordering for http get retrieving objects
            CreateMap<Entities.Order, Dtos.Order>()
                .ForMember(d => d.Employee, o => o.MapFrom(s => s.Employee.EmployeeName));
            CreateMap<Entities.Order, Dtos.OrderWithItems>()
                .ForMember(d => d.Employee, o => o.MapFrom(s => s.Employee.EmployeeName));
            //this one is used after emp is created and sent Dtos back having empId and Name
            CreateMap<Entities.Employee, Dtos.Employee>();
            CreateMap<Entities.Item, Dtos.Item>();
            CreateMap<Entities.Item, Dtos.ItemForCreation>();
            CreateMap<Entities.Item, Dtos.ItemWithEstimatedCost>();
            CreateMap<Entities.Customer, Dtos.Customer>();

            //mapping for Ordering for http post creating objects
            CreateMap<Dtos.EmployeeForCreation, Entities.Employee>();
            CreateMap<Dtos.CustomerForCreation, Entities.Customer>();
            

            
           


        }
    }
}
using System.Linq;
using AutoMapper;

namespace TourCmdAPI.Entities
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            //mapping for touring httpget from wep api to client
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
            
            
                

            //mapping for Ordering for http get retrieving objects, because I get entities
            //from dbContext and send Dtos to client
            // CreateMap<Entities.Order, Dtos.Order>()
            //     .ForMember(d => d.Employee, o => o.MapFrom(s => s.Employee.EmployeeName));
            // CreateMap<Entities.Order, Dtos.OrderWithItems>()
            //     .ForMember(d => d.Employee, o => o.MapFrom(s => s.Employee.EmployeeName));

            //this one is used after emp is created and sent Dtos back having empId and Name
            CreateMap<Entities.Employee, Dtos.Employee>();
            CreateMap<Entities.Item, Dtos.Item>();
            CreateMap<Entities.Item, Dtos.ItemForCreation>();
            CreateMap<Entities.Item, Dtos.ItemWithEstimatedCost>();
            CreateMap<Entities.Customer, Dtos.Customer>();
            CreateMap<Entities.Ingredient, Dtos.Ingredient>()
                .ForMember(d => d.IngredientCategory, o => o.MapFrom(s => s.IngredientCategory.IngredientCategoryName));
            CreateMap<Entities.IngredientCategory, Dtos.IngredientCategory>();
            CreateMap<Entities.OrderItem,  Dtos.OrderItem>();
             CreateMap<Entities.Order,  Dtos.OrderForCreation>();
             CreateMap<Entities.Order, Dtos.Order>();
             CreateMap<Entities.OrderItemForCreation, Dtos.OrderItemForCreation>();
             CreateMap<Entities.PaymentDetail, Dtos.PaymentDetail>();
             CreateMap<Entities.PaymentDetail, Dtos.PaymentDetailForCreation>();
             CreateMap<Entities.OrderItem, Dtos.OrderItem>();
             CreateMap<Entities.Order, Dtos.Order>()
                .ForMember(dto => dto.Items, 
                opt => opt.MapFrom(child => child.OrderItems
                    .Select(grandChild => grandChild.Item).ToList()));

            
            //mapping for Ordering for http post creating objects
            CreateMap<Dtos.EmployeeForCreation, Entities.Employee>();
            CreateMap<Dtos.CustomerForCreation, Entities.Customer>();
            CreateMap<Dtos.ItemForCreation, Entities.Item>();
            CreateMap<Dtos.Ingredient, Entities.Ingredient>();
            CreateMap<Dtos.IngredientCategoryForCreation, Entities.IngredientCategory>();
            CreateMap<Dtos.IngredientForCreation, Entities.Ingredient>();
            CreateMap<Dtos.IngredientCategory, Entities.IngredientCategory>();
            CreateMap<Dtos.OrderItem,  Entities.OrderItem>();
            CreateMap<Dtos.OrderForCreation,  Entities.Order>();
            CreateMap<Dtos.OrderItemForCreation, Entities.OrderItem>();
            CreateMap<Dtos.PaymentDetail, Entities.PaymentDetail>();
            CreateMap<Dtos.PaymentDetailForCreation, Entities.PaymentDetail>();

        }
    }
}
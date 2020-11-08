using System;
using System.Collections.Generic;
using TourCmdAPI.Entities;

namespace TourCmdAPI.Services
{
    public static class OrderManagementContextExtensions
    {
        public static void EnsureSeedDataForOrderContext(this OrderContext context)
        {
            // first, clear the database.  This ensures we can always start 
            // fresh with each demo.  Not advised for production environments, obviously :-)

            
            context.Orders.RemoveRange(context.Orders);
            context.Employees.RemoveRange(context.Employees);
            context.Customers.RemoveRange(context.Customers);
     

            context.SaveChanges();

            // init seed data
            

            var emps = new List<Employee>()
            {
                new Employee()
                {
                    EmployeeName = "Jose Gabriel",
                     CreatedBy = "system",
                    CreatedOn = DateTime.UtcNow
                },
                new Employee()
                {
                    EmployeeName = "Felix",
                     CreatedBy = "system",
                    CreatedOn = DateTime.UtcNow
                }
            };

            var customers = new List<Customer>()
            {
                new Customer()
                {
                    CustomerName = "Sandra",
                     CreatedBy = "system",
                    CreatedOn = DateTime.UtcNow
                },
                new Customer()
                {
                    CustomerName = "Alberto",
                     CreatedBy = "system",
                    CreatedOn = DateTime.UtcNow
                }
            };


            // var orders = new List<Order>()
            // {
            //     new Order()
            //     {
            //         CustomerId = 2,
            //         Description = "Llega a las 11am",
            //          CreatedBy = "system",
            //         CreatedOn = DateTime.UtcNow,
            //         EmployeeId = 1,
            //         Items = new List<Item>()
            //         {
            //             new Item() {
            //                 ItemName = "Huarache",
            //                 Description = "Chicharron",
            //                 Price = 10,
            //                  CreatedBy = "system",
            //                 CreatedOn = DateTime.UtcNow
            //             },
            //             new Item() {
            //                 ItemName = "Quesadilla",
            //                 Description = "Veggies",
            //                 Price = 10,
            //                  CreatedBy = "system",
            //         CreatedOn = DateTime.UtcNow
            //             },
            //             new Item() {
            //                 ItemName = "Menudo",
            //                 Description = "Grande",
            //                 Price = 9,
            //                  CreatedBy = "system",
            //         CreatedOn = DateTime.UtcNow
            //             },
            //             new Item() {
            //                 ItemName = "Agua",
            //                 Description = "Chica",
            //                 Price = 2,
            //                  CreatedBy = "system",
            //         CreatedOn = DateTime.UtcNow
            //             }
            //         }
            //     },                        
            //     new Order()
            //     {
            //         CustomerId = 1,
            //         Description = "Llega a las 12pm",
            //         EmployeeId = 2,
            //          CreatedBy = "system",
            //         CreatedOn = DateTime.UtcNow,
            //         Items = new List<Item>()
            //         {
            //             new Item() {
            //                 ItemName = "Quesadilla",
            //                 Description = "Champinon",
            //                 Price = 10,
            //                  CreatedBy = "system",
            //         CreatedOn = DateTime.UtcNow
            //             },
            //             new Item() {
            //                 ItemName = "Fish Dinner Plate",
            //                 Description = "Filetes de mojarra",
            //                 Price = 12,
            //                  CreatedBy = "system",
            //         CreatedOn = DateTime.UtcNow
            //             },
            //             new Item() {
            //                 ItemName = "Ceviche",
            //                 Description = "Grande",
            //                 Price = 15,
            //                  CreatedBy = "system",
            //         CreatedOn = DateTime.UtcNow
            //             },
            //             new Item() {
            //                 ItemName = "Tostada Regia",
            //                 Description = "To Go",
            //                 Price = 8,
            //                  CreatedBy = "system",
            //         CreatedOn = DateTime.UtcNow
            //             }
            //         }
            //     }
            // };
            //  context.Employees.AddRange(emps);
            //  context.Customers.AddRange(customers);
            // context.Orders.AddRange(orders);
            
            
            context.SaveChanges();
        }
    }
}
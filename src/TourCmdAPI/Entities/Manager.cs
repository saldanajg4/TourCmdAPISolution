using System;

namespace TourCmdAPI.Entities
{
    public class Manager : AuditableEntity
    {
         public Guid ManagerId { get; set; }
        public string Name { get; set; }
    }
}
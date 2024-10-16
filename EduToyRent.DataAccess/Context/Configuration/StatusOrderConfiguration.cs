using EduToyRent.DAL.Entities;
using EduToyRent.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DataAccess.Context.Configuration
{
    public class StatusOrderConfiguration : IEntityTypeConfiguration<StatusOrder>
    {
        public void Configure(EntityTypeBuilder<StatusOrder> builder)
        {
            builder.HasData
           (
               new StatusOrder
               {
                   StatusId = 1,
                   StatusName = "Wating for approvement"
               },
                new StatusOrder
                {
                    StatusId = 2,
                    StatusName = "Prepare to ship"
                },
                new StatusOrder
                {
                    StatusId = 3,
                    StatusName = "Shipping"
                }, 
                new StatusOrder
                {
                    StatusId = 4,
                    StatusName = "Shipped"
                },
                new StatusOrder
                {
                    StatusId = 5,
                    StatusName = "Returned"
                }, 
                new StatusOrder
                {
                    StatusId = 6,
                    StatusName = "Returned because of reason"
                }, 
                new StatusOrder
                {
                    StatusId = 7,
                    StatusName = "Complete get toy"
                }, 
                new StatusOrder
                {
                    StatusId = 8,
                    StatusName = "Completed"
                }, 
                new StatusOrder
                {
                    StatusId = 9,
                    StatusName = "Canceled"
                }
           );
        }
    }
}

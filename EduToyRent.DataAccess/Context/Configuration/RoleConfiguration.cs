﻿using EduToyRent.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DataAccess.Context.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData
            (
                new Role
                {
                    RoleId = 1,
                    RoleName = "User"
                },
                new Role
                {
                    RoleId = 2,
                    RoleName = "Supplier"
                },
                new Role
                {
                    RoleId = 3,
                    RoleName = "Staff"
                },
                new Role
                {
                    RoleId = 4,
                    RoleName = "Admin"
                }
            );
        }
    }
}

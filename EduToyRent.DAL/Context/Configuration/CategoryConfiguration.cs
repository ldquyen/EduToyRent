using EduToyRent.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Context.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
                (
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Toan Hoc"
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Y Hoc"
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Vat Ly"
                },
                new Category
                {
                    CategoryId = 4,
                    CategoryName = "Dia Ly"
                }
                );
        }
    }
}

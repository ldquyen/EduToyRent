using EduToyRent.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DataAccess.Context.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasData
           (
               new Account
               {
                   AccountId = 1,
                   AccountEmail = "user@gmail.com",
                   AccountName = "user",
                   AccountPassword = "ca6f3892687b200b296a8c4712c2d5f7ad09228a0bfeee5483de2fa67cffcf89", //edutoyrent123
                   RoleId = 1,
                   Address = "HCM",
                   PhoneNumber = "1234567890",
               },
              new Account
              {
                  AccountId = 2,
                  AccountEmail = "supplier@gmail.com",
                  AccountName = "supplier",
                  AccountPassword = "ca6f3892687b200b296a8c4712c2d5f7ad09228a0bfeee5483de2fa67cffcf89", //edutoyrent123
                  RoleId = 2,
                  Address = "HCM",
                  PhoneNumber = "1234567890",
              }, new Account
              {
                  AccountId = 3,
                  AccountEmail = "staff@gmail.com",
                  AccountName = "staff",
                  AccountPassword = "ca6f3892687b200b296a8c4712c2d5f7ad09228a0bfeee5483de2fa67cffcf89", //edutoyrent123
                  RoleId = 3,
                  Address = "HCM",
                  PhoneNumber = "1234567890",
              }, new Account
              {
                  AccountId = 4,
                  AccountEmail = "admin@gmail.com",
                  AccountName = "admin",
                  AccountPassword = "ca6f3892687b200b296a8c4712c2d5f7ad09228a0bfeee5483de2fa67cffcf89", //edutoyrent123
                  RoleId = 4,
                  Address = "HCM",
                  PhoneNumber = "1234567890",
              }
           );
        }
    }
}

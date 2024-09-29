using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EduToyRent.BLL.DTOs.AccountDTO;
using EduToyRent.DAL.Entities;

namespace EduToyRent.BLL.Mappings
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<LoginDTO, Account>()
                .ForMember(x => x.AccountEmail, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.AccountPassword, opt => opt.MapFrom(x => x.Password));
            CreateMap<Account, CurrentUserObject>().ReverseMap();

        }
    }
}

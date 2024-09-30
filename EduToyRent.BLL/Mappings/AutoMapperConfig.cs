using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EduToyRent.BLL.DTOs.AccountDTO;
using EduToyRent.BLL.DTOs.CategoryDTO;
using EduToyRent.BLL.DTOs.RequestFormDTO;
using EduToyRent.BLL.DTOs.ToyDTO;
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

            CreateMap<SignupAccountDTO, Account>();

            CreateMap<CreateRentalToyDTO, Toy>();
            CreateMap<CreateSaleToyDTO, Toy>();

            CreateMap<CreateNewCategoryDTO, Category>();

            CreateMap<CreateRentalRequestDTO, RequestForm>();
            CreateMap<CreateSaleRequestDTO, RequestForm>();

        }
    }
}

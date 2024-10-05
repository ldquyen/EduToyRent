using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.CategoryDTO;
using EduToyRent.Service.DTOs.RequestFormDTO;
using EduToyRent.Service.DTOs.ToyDTO;
using EduToyRent.DAL.Entities;

namespace EduToyRent.Service.Mappings
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
            CreateMap<ProfileDTO, Account>().ReverseMap();

            CreateMap<CreateRentalToyDTO, Toy>();
            CreateMap<CreateSaleToyDTO, Toy>();

            CreateMap<CreateNewCategoryDTO, Category>();


            //request
            CreateMap<CreateRentalRequestDTO, RequestForm>();
            CreateMap<CreateSaleRequestDTO, RequestForm>();
            CreateMap<RequestForm, ResponseRequestListDTO>();
            CreateMap<RequestForm, ResponseRequestDetailDTO>();
            CreateMap<RequestForm, UpdateRequestDTO>();
        }
    }
}

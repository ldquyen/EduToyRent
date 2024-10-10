﻿using System;
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
using EduToyRent.Service.DTOs.OrderDTO;

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

            CreateMap<AccountDTO , Account>().ReverseMap();

            //toy
            CreateMap<CreateRentalToyDTO, Toy>();
            CreateMap<CreateSaleToyDTO, Toy>();
            CreateMap<UpdateToyDTO, Toy>();
            CreateMap<Toy, ViewToyDTO>().ReverseMap();
            CreateMap<Toy, ViewToyDTO>().ReverseMap();
            CreateMap<Toy, ViewToyDetailDTO>().ReverseMap();
            CreateMap<Toy, ResponseRentalToyDTO>();
            CreateMap<Toy, ResponseSaleToyDTO>();

            CreateMap<CreateNewCategoryDTO, Category>();

            //request
            CreateMap<CreateRentalRequestDTO, RequestForm>();
            CreateMap<CreateSaleRequestDTO, RequestForm>();
            CreateMap<RequestForm, ResponseRequestListDTO>();
            CreateMap<RequestForm, ResponseRequestDetailDTO>();
            CreateMap<RequestForm, UpdateRequestDTO>();

            //order
            CreateMap<CreateOrderDTO, Order>();
            CreateMap<Order, ResponseOrderForStaff>();
        }
    }
}

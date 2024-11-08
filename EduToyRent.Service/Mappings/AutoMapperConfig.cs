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
using EduToyRent.Service.DTOs.OrderDTO;
using EduToyRent.Service.DTOs.CartDTO;
using Net.payOS.Types;
using EduToyRent.Service.DTOs.VoucherDTO;
using EduToyRent.DataAccess.Entities;
using EduToyRent.Service.DTOs.ReportDTO;

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
            CreateMap<Account, AccountDTO>();

            //toy
            CreateMap<CreateRentalToyDTO, Toy>();
            CreateMap<CreateSaleToyDTO, Toy>();
            CreateMap<UpdateToyDTO, Toy>();
            CreateMap<Toy, ViewToyForRentDTO>().ReverseMap();
            CreateMap<Toy, ViewToyForSaleDTO>().ReverseMap();
            CreateMap<Toy, ViewToyForRentDetailDTO>().ReverseMap();
            CreateMap<Toy, ViewToyForSaleDetailDTO>().ReverseMap();
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
            CreateMap<CreateRentOrderDetailDTO, OrderDetail>()
                .ForMember(dest => dest.RentalDate, opt => opt.MapFrom(src => src.RentalDate))
                .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate));
            CreateMap<CreateSaleOrderDetailDTO, OrderDetail>();

            CreateMap<Order, ResponseOrderRentDetailForUserDTO>()
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.AccountName))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.StatusOrder.StatusName));
            CreateMap<OrderDetail, ODRentDTO>()
                .ForMember(dest => dest.ToyName, opt => opt.MapFrom(src => src.Toy.ToyName))
                .ForMember(dest => dest.ShipperPhone, opt => opt.MapFrom(src => src.ShipDates != null ? src.ShipDates.ShipperPhone : null));


            CreateMap<Order, ResponseOrderSaleDetailForUserDTO>()
                 .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.AccountName))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.StatusOrder.StatusName));
            CreateMap<OrderDetail, ODSaleDTO>()
                .ForMember(dest => dest.ToyName, opt => opt.MapFrom(src => src.Toy.ToyName))
                .ForMember(dest => dest.ShipperPhone, opt => opt.MapFrom(src => src.ShipDates != null ? src.ShipDates.ShipperPhone : null));

            CreateMap<Order, ResponseOrderForUser>()
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.StatusOrder.StatusName));

            CreateMap<Order, InformationForShipDTO>()
            .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.AccountName));


            CreateMap<OrderDetail, ReponseOrderSaleForSupplierDTO>();
            CreateMap<OrderDetail, ReponseOrderRentForSupplierDTO>();

            //cart
            CreateMap<CartItem, GetCartResponse>();

            //Payment
            CreateMap<ODRentDTO, ItemData>()
            .ForMember(dest => dest.price, opt => opt.MapFrom(src => (int)(src.RentalPrice ?? 0)));
            CreateMap<ODSaleDTO, ItemData>()
           .ForMember(dest => dest.price, opt => opt.MapFrom(src => (int)(src.Price)));

            //voucher
            CreateMap<AccountVoucher, VoucherForAccountDTO>()
            .ForMember(dest => dest.VoucherId, opt => opt.MapFrom(src => src.Voucher.VoucherId))
            .ForMember(dest => dest.VoucherName, opt => opt.MapFrom(src => src.Voucher.VoucherName))
            .ForMember(dest => dest.ExpiredDate, opt => opt.MapFrom(src => src.Voucher.ExpiredDate))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Voucher.Discount))
            .ForMember(dest => dest.IsUsed, opt => opt.MapFrom(src => src.IsUsed));

            //report
            CreateMap<CreateReportDTO, Report>()
                .ForMember(dest => dest.ReportDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending"));
            CreateMap<ReportListDTO, Report>();
            CreateMap<Report, ReportListDTO>()
    .ForMember(dest => dest.ToyName, opt => opt.MapFrom(src => src.Toy.ToyName))
    .ForMember(dest => dest.ReporterName, opt => opt.MapFrom(src => src.Account.AccountName))
    .ForMember(dest => dest.ReportDetail, opt => opt.MapFrom(src => src.ReportDetail))
    .ForMember(dest => dest.ReportDate, opt => opt.MapFrom(src => src.ReportDate))
    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));


        }
    }
}

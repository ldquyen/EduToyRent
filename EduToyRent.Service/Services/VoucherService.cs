using AutoMapper;
using EduToyRent.DAL.Entities;
using EduToyRent.Repository.Interfaces;
using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.CategoryDTO;
using EduToyRent.Service.DTOs.VoucherDTO;
using EduToyRent.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VoucherService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<dynamic> CreateVoucher(CreateVoucherDTO createVoucherDTO)
        {
            var voucher = new Voucher() 
            {
                VoucherName = createVoucherDTO.VoucherName,
                CreatedDate = DateTime.Now,
                ExpiredDate = createVoucherDTO.ExpiredDate,
                Discount = createVoucherDTO.Discount,
                Quantity = createVoucherDTO.Quantity,
                Used = 0,
                IsActive = true
            };
            await _unitOfWork.VoucherRepository.AddAsync(voucher);
            await _unitOfWork.SaveAsync();
            return Result.Success();
        }
        public async Task<dynamic> GetListVoucher()
        {
            var list = await _unitOfWork.VoucherRepository.GetAllAsync();
            List<ViewVoucherDTO> viewVoucherDTO = new List<ViewVoucherDTO>();
            foreach (var voucher in list)
            {
                var dto = new ViewVoucherDTO
                {
                    VoucherName = voucher.VoucherName,
                    Discount = voucher.Discount,
                    ExpiredDate= voucher.ExpiredDate,
                    Quantity = voucher.Quantity,
                    IsActive = voucher.IsActive
                };

                viewVoucherDTO.Add(dto);
            }
            return Result.SuccessWithObject(viewVoucherDTO);
        }
        public async Task<dynamic> GiveVoucherToAccount(int accountid, int voucherid)
        {
            var accountVoucher = new AccountVoucher()
            {
                AccountId = accountid,
                VoucherId = voucherid,
                IsUsed = false
            };
            await _unitOfWork.AccountVoucherRepository.AddAsync(accountVoucher);
            await _unitOfWork.SaveAsync();
            return Result.Success();
        }
    }
}

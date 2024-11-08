using AutoMapper;
using EduToyRent.DAL.Entities;
using EduToyRent.Repository.Interfaces;
using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.CategoryDTO;
using EduToyRent.Service.DTOs.VoucherDTO;
using EduToyRent.Service.Exceptions;
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
            var save = await _unitOfWork.VoucherRepository.AddAsync(voucher);
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
                    VoucherId = voucher.VoucherId,
                    VoucherName = voucher.VoucherName,
                    Discount = voucher.Discount,
                    ExpiredDate = voucher.ExpiredDate,
                    Quantity = voucher.Quantity,
                    Used = voucher.Used,
                    IsActive = voucher.IsActive
                };

                viewVoucherDTO.Add(dto);
            }
            return Result.SuccessWithObject(viewVoucherDTO);
        }
        public async Task<dynamic> GiveVoucherToAccount(int accountid, int voucherid)
        {
            var voucher = await _unitOfWork.VoucherRepository.GetByIdAsync(voucherid);
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountid);
            if (voucher.IsActive)
            {
                if (voucher != null && account != null)
                {
                    var accountVoucher = new AccountVoucher()
                    {
                        AccountId = accountid,
                        VoucherId = voucherid,
                        IsUsed = false
                    };
                    var reducevoucher = await _unitOfWork.VoucherRepository.GetByIdAsync(voucherid);
                    reducevoucher.Used++;
                    if (reducevoucher.Quantity == reducevoucher.Used)
                    {
                        reducevoucher.IsActive = false;
                    }
                    var update = await _unitOfWork.VoucherRepository.UpdateAsync(reducevoucher);
                    var save = await _unitOfWork.AccountVoucherRepository.AddAsync(accountVoucher);
                    await _unitOfWork.SaveAsync();
                    return Result.Success();
                }
                else
                {
                    return Result.Failure(VoucherErrors.InvalidAccountIdAndVoucherId);
                }
            }
            else
            {
                return Result.Failure(VoucherErrors.VoucherDeativated);

            }
        }
        public async Task<dynamic> ActiveDeactiveVoucher(int voucherid, int flag)
        {
            var voucher = await _unitOfWork.VoucherRepository.GetByIdAsync(voucherid);
            if (voucher.Quantity != voucher.Used)
            {
                if (voucher != null)
                {
                    if (flag == 1 || flag == 0)
                    {
                        if (flag == 0)
                        {
                            voucher.IsActive = false;
                            var update = await _unitOfWork.VoucherRepository.UpdateAsync(voucher);
                            await _unitOfWork.SaveAsync();
                            return Result.Success();
                        }
                        else
                        {
                            voucher.IsActive = true;
                            var update = await _unitOfWork.VoucherRepository.UpdateAsync(voucher);
                            await _unitOfWork.SaveAsync();
                            return Result.Success();
                        }
                    }
                    else
                    {
                        return Result.Failure(VoucherErrors.InvalidFlag);
                    }
                }
                else
                {
                    return Result.Failure(VoucherErrors.InvalidVoucherId);
                }
            }
            else
            {
                return Result.Failure(VoucherErrors.UsedAllCantActive);

            }
        }
        public async Task<dynamic> EditVoucher(int voucherid, EditVoucherDTO editVoucherDTO)
        {
            var voucher = await _unitOfWork.VoucherRepository.GetByIdAsync(voucherid);
            if (voucher != null)
            {
                voucher.VoucherName = editVoucherDTO.VoucherName;
                voucher.ExpiredDate = editVoucherDTO.ExpiredDate;
                voucher.Discount = editVoucherDTO.Discount;
                voucher.Quantity = editVoucherDTO.Quantity;
                var update = await _unitOfWork.VoucherRepository.UpdateAsync(voucher);
                await _unitOfWork.SaveAsync();
                return Result.Success();
            }
            else
            {
                return Result.Failure(VoucherErrors.InvalidVoucherId);
            }
        }
        public async Task<dynamic> GetVoucherForUser(int accountId)
        {
            var avList = await _unitOfWork.AccountVoucherRepository.GetVoucherForUser(accountId);
            var vouchers = _mapper.Map<List<VoucherForAccountDTO>>(avList);
            return Result.SuccessWithObject(vouchers);
        }
    }
}

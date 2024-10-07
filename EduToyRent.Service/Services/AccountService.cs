using AutoMapper;
using EduToyRent.Service.Common;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.Exceptions;
using EduToyRent.Service.Interfaces;
using EduToyRent.DAL.Entities;
using EduToyRent.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<dynamic> SignUpAccount(SignupAccountDTO signupAccountDTO)
        {
            var account = _mapper.Map<Account>(signupAccountDTO);
            if (await _unitOfWork.AccountRepository.CheckEmailExistAsync(account.AccountEmail)) return Result.Failure(SignupErrors.DuplicateEmail);
            if (await _unitOfWork.AccountRepository.CheckPhoneExistAsync(account.PhoneNumber)) return Result.Failure(SignupErrors.DuplicatePhone);
            account.AccountPassword = await HashPassword.HassPass(account.AccountPassword);
            account.RoleId = 1;
            account.IsBan = false;
            await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.SaveAsync();

            var cart = new Cart
            {
                AccountId = account.AccountId
            };
            await _unitOfWork.CartRepository.AddAsync(cart);
            await _unitOfWork.SaveAsync();
            return Result.Success();
        }
        public async Task<dynamic> UpdateProfile(EditAccountProfileDTO editAccountProfileDTO, CurrentUserObject currentUserObject)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(currentUserObject.AccountId);
            account.AccountName = editAccountProfileDTO.AccountName;
            account.PhoneNumber = editAccountProfileDTO.PhoneNumber;
            account.Address = editAccountProfileDTO.Address;
            await _unitOfWork.AccountRepository.UpdateAsync(account);
            await _unitOfWork.SaveAsync();
            return Result.Success();
        }
        public async Task<dynamic> GetProfile(CurrentUserObject currentUserObject)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(currentUserObject.AccountId);
            ProfileDTO profile = new ProfileDTO()
            {
                AccountEmail = account.AccountEmail,
                AccountName = account.AccountName,
                RoleId = account.RoleId,
                Address = account.Address,
                PhoneNumber = account.PhoneNumber,
            };
            return Result.SuccessWithObject(profile);
        }
        public async Task<dynamic> ChangePassword(PasswordDTO password, CurrentUserObject currentUserObject)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(currentUserObject.AccountId);
            string oldPassword = await HashPassword.HassPass(password.OldAccountPassword);
            if (account.AccountPassword == oldPassword)
            {
                account.AccountPassword = await HashPassword.HassPass(password.AccountPassword);
                await _unitOfWork.AccountRepository.UpdateAsync(account);
                await _unitOfWork.SaveAsync();
                return Result.Success();
            }
            else
            {
                return Result.Failure(ChangePasswordErrors.WrongOldPassword);
            }
            
        }
    }
}

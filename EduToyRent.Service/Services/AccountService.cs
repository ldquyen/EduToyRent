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
            if(account.AccountPassword == password.OldAccountPassword)
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

        //public async Task<IEnumerable<Account>> ViewAllAcount()
        //{
        //    var accounts = await _unitOfWork.AccountRepository.GetAllAsync();
        //    return account1;
        //}
        public async Task<IEnumerable<AccountDTO>> ViewAllAccount()
        {
            var accounts = await _unitOfWork.AccountRepository.GetAllAsync();

            // Ánh xạ thủ công từ Account sang AccountDTO
            var accountDtos = accounts.Select(a => new AccountDTO
            {
                AccountId = a.AccountId,                 // Giả sử `Account.Id` tương ứng với `AccountDTO.AccountId`
                AccountName = a.AccountName,         // Giả sử `Account.Username` tương ứng với `AccountDTO.AccountName`
                AccountEmail = a.AccountEmail,           // Giả sử `Account.Email` tương ứng với `AccountDTO.AccountEmail`
                AccountPassword = a.AccountPassword,     // Giả sử `Account.Password` tương ứng với `AccountDTO.AccountPassword`
                RoleId = a.RoleId,                // Giả sử `Account.RoleId` tương ứng với `AccountDTO.RoleId`
                Address = a.Address,              // Giả sử `Account.Address` tương ứng với `AccountDTO.Address`
                PhoneNumber = a.PhoneNumber,      // Giả sử `Account.PhoneNumber` tương ứng với `AccountDTO.PhoneNumber`
                IsBan = a.IsBan,                  // Giả sử `Account.IsBan` tương ứng với `AccountDTO.IsBan`
                /*Role = a.Role     */                // Nếu `Role` là một entity khác được ánh xạ vào DTO
            });

            return accountDtos;
        }



        public async Task<dynamic> BanAccount(int accountDTO)
        {

            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountDTO);
            account.IsBan = true;
            await _unitOfWork.AccountRepository.UpdateAsync(account);
            await _unitOfWork.SaveAsync();
            return Result.Success();
        }

        public async Task<dynamic> SignUpAccountToySupplier(SignupAccountDTO signupAccountDTO)
        {
            var account = _mapper.Map<Account>(signupAccountDTO);
            if (await _unitOfWork.AccountRepository.CheckEmailExistAsync(account.AccountEmail)) return Result.Failure(SignupErrors.DuplicateEmail);
            if (await _unitOfWork.AccountRepository.CheckPhoneExistAsync(account.PhoneNumber)) return Result.Failure(SignupErrors.DuplicatePhone);
            account.AccountPassword = await HashPassword.HassPass(account.AccountPassword);
            account.RoleId = 2;
            account.IsBan = false;
            await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.SaveAsync();

            //var cart = new Cart
            //{
            //    AccountId = account.AccountId
            //};
            //await _unitOfWork.CartRepository.AddAsync(cart);
            //await _unitOfWork.SaveAsync();
            return Result.Success();
        }
    }
}

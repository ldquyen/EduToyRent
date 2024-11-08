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
using Microsoft.Identity.Client;
using EduToyRent.Service.DTOs.ForgotPasswordDTO;
using System.Security.Cryptography;
using EduToyRent.DataAccess.Entities;
using EduToyRent.Service.DTOs.EmailDTO;

namespace EduToyRent.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
		private readonly GmailSender _mailSender;


        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, GmailSender sender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
			_mailSender = sender;
        }
        public async Task<dynamic> SignUpAccount(SignupAccountDTO signupAccountDTO)
        {
            var account = _mapper.Map<Account>(signupAccountDTO);
            if (await _unitOfWork.AccountRepository.CheckEmailExistAsync(account.AccountEmail)) return Result.Failure(SignupErrors.DuplicateEmail);
            if (await _unitOfWork.AccountRepository.CheckPhoneExistAsync(account.PhoneNumber)) return Result.Failure(SignupErrors.DuplicatePhone);
            account.AccountPassword = await HashPassword.HassPass(account.AccountPassword);
            account.RoleId = 1;
            account.IsBan = false;
            var saveaccount = await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.SaveAsync();


            var cart = new Cart
            {
                AccountId = account.AccountId
            };
            Cart cartrent = new()
            {
                AccountId = account.AccountId,
                IsRental = true,
            };
            var cartRent = await _unitOfWork.CartRepository.AddCartAsync(cartrent);

            Cart cartsale = new()
            {
                AccountId = account.AccountId,
                IsRental = false,
            };
            var cartSale = await _unitOfWork.CartRepository.AddCartAsync(cartsale);
            if (account == null || cartRent == null || cartSale == null)
                return Result.Failure(AccountError.CannotCreate);
            else
                return Result.Success();
        }
        public async Task<dynamic> UpdateProfile(EditAccountProfileDTO editAccountProfileDTO, CurrentUserObject currentUserObject)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(currentUserObject.AccountId);
            account.AccountName = editAccountProfileDTO.AccountName;
            account.PhoneNumber = editAccountProfileDTO.PhoneNumber;
            account.Address = editAccountProfileDTO.Address;
            var update = await _unitOfWork.AccountRepository.UpdateAsync(account);
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
                var update = await _unitOfWork.AccountRepository.UpdateAsync(account);
                await _unitOfWork.SaveAsync();
                return Result.Success();
            }
            else
            {
                return Result.Failure(ChangePasswordErrors.WrongOldPassword);
            }

        }


        public async Task<dynamic> ViewAllAccount(int page)
        {
            var accounts = await _unitOfWork.AccountRepository.GetAllAsync(x => x.RoleId == 1 || x.RoleId == 2, null, page, 10);
            var list = _mapper.Map<List<AccountDTO>>(accounts);
            return Result.SuccessWithObject(list);
        }

        public async Task<dynamic> ViewAllStaffAccount(int page)
        {
            var accounts = await _unitOfWork.AccountRepository.GetAllAsync(x => x.RoleId == 3, null, page, 10);
            var list = _mapper.Map<List<AccountDTO>>(accounts);
            return Result.SuccessWithObject(list);
        }




        public async Task<dynamic> BanUserAccount(int userId)
        {

            var account = await _unitOfWork.AccountRepository.GetByIdAsync(userId);
            if (account.RoleId == 1 || account.RoleId == 2)
            {
                account.IsBan = true;
                var update = await _unitOfWork.AccountRepository.UpdateAsync(account);
                await _unitOfWork.SaveAsync();
                return Result.Success();
            }
            else
            {
                return Result.Failure(AccountError.Cannotban);
            }

        }

        public async Task<dynamic> BanStaffAccount(int userId)
        {

            var account = await _unitOfWork.AccountRepository.GetByIdAsync(userId);
            if (account.RoleId == 3)
            {
                account.IsBan = true;
                var update = await _unitOfWork.AccountRepository.UpdateAsync(account);
                await _unitOfWork.SaveAsync();
                return Result.Success();
            }
            else
            {
                return Result.Failure(AccountError.Cannotban);
            }

        }

        public async Task<dynamic> SignUpAccountToySupplier(SignupAccountDTO signupAccountDTO)
        {
            var account = _mapper.Map<Account>(signupAccountDTO);
            if (await _unitOfWork.AccountRepository.CheckEmailExistAsync(account.AccountEmail)) return Result.Failure(SignupErrors.DuplicateEmail);
            if (await _unitOfWork.AccountRepository.CheckPhoneExistAsync(account.PhoneNumber)) return Result.Failure(SignupErrors.DuplicatePhone);
            account.AccountPassword = await HashPassword.HassPass(account.AccountPassword);
            account.RoleId = 2;
            account.IsBan = false;
            var save = await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.SaveAsync();

            //var cart = new Cart
            //{
            //    AccountId = account.AccountId
            //};
            //await _unitOfWork.CartRepository.AddAsync(cart);
            //await _unitOfWork.SaveAsync();
            return Result.Success();
        }
        public async Task<dynamic> SignUpStaffToySupplier(SignupAccountDTO signupAccountDTO)
        {
            var account = _mapper.Map<Account>(signupAccountDTO);
            if (await _unitOfWork.AccountRepository.CheckEmailExistAsync(account.AccountEmail)) return Result.Failure(SignupErrors.DuplicateEmail);
            if (await _unitOfWork.AccountRepository.CheckPhoneExistAsync(account.PhoneNumber)) return Result.Failure(SignupErrors.DuplicatePhone);
            account.AccountPassword = await HashPassword.HassPass(account.AccountPassword);
            account.RoleId = 3;
            account.IsBan = false;
            var save = await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.SaveAsync();

            //var cart = new Cart
            //{
            //    AccountId = account.AccountId
            //};
            //await _unitOfWork.CartRepository.AddAsync(cart);
            //await _unitOfWork.SaveAsync();
            return Result.Success();
        }
        public async Task<dynamic> SendPasswordResetOTP(ForgotPasswordDto request)
        {
            try
            {
                bool isEmailExist = await _unitOfWork.AccountRepository.CheckEmailExistAsync(request.Email);
                if (!isEmailExist) return Result.Failure(ResetPasswordErrors.EmailNotFound);
				
                ResetPasswordOTP? otp = await _unitOfWork.ResetPasswordOTPRepository.GetAsync
                    (q => q.Email == request.Email);

                if (otp != null) // refresh OTP entity
                {
                    otp.OTP = Convert.ToHexString(RandomNumberGenerator.GetBytes(24)).Substring(0, 5);
                    otp.expires = DateTime.Now.AddDays(1);
                    var update = await _unitOfWork.ResetPasswordOTPRepository.UpdateAsync(otp);
                }
                else //create new OTP entity
                {
                    otp = new()
                    {
                        Email = request.Email,
                        OTP = Convert.ToHexString(RandomNumberGenerator.GetBytes(24)).Substring(0, 5),
                        expires = DateTime.Now.AddDays(1)
                    };
                    var save = await _unitOfWork.ResetPasswordOTPRepository.AddAsync(otp);
                }
                
				if (!SendOTPEmail(request.Email, otp.OTP))
				{
					return Result.Failure(new Error("500", "Cannot send email"));
				}
				await _unitOfWork.SaveAsync();
				return Result.Success();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Result.Failure(new Error("500", ex.Message));
            }
        }

		private bool SendOTPEmail(string email, string code) 
		{
			try
			{
				var request = new EmailRequestDTO()
				{
					ReceiverEmail = email,
					EmailSubject = "EduToyRent Password Reset Code",
					IsHtml = true,
					EmailBody = $"<div style=\"font-family: Helvetica,Arial,sans-serif;min-width:1000px;overflow:auto;line-height:2\">\r\n  <div style=\"margin:50px auto;width:70%;padding:20px 0\">\r\n    <div style=\"border-bottom:1px solid #eee\">\r\n      <a href=\"\" style=\"font-size:1.4em;color: #ee0000;text-decoration:none;font-weight:600\">EduToyRent Platform</a>\r\n    </div>\r\n    <p style=\"font-size:1.1em\">Hi,</p>\r\n    <p>This is your password reset code. This code will expire in 24 hours.</p>\r\n    <h2 style=\"background: #aa0000;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;\">{code}</h2>\r\n    <p style=\"font-size:0.9em;\">Regards,<br />EduToyRent Staff</p>\r\n    <hr style=\"border:none;border-top:1px solid #eee\" />\r\n    <div style=\"float:right;padding:8px 0;color:#aaa;font-size:0.8em;line-height:1;font-weight:300\">\r\n      <p>EduToyRent Platform</p>\r\n      <p>Ho Chi Minh City</p>\r\n      <p>Vietnam</p>\r\n    </div>\r\n  </div>\r\n</div>"
				};
				return _mailSender.SendEmailSingle(request);
			}
			catch (Exception ex) 
			{
                Console.WriteLine(ex.Message);
				return false;
			}
		}


        public async Task<dynamic> ResetPasswordUsingOTP(ResetPasswordDto request)
        {
            try
            {
                ResetPasswordOTP? otp = await _unitOfWork.ResetPasswordOTPRepository.GetAsync
                (q => q.OTP == request.OTP && q.Email.Equals(request.Email));

                if (otp == null)
                    return Result.Failure(ResetPasswordErrors.IncorrectOTP);

                if (otp.expires < DateTime.Now)
                    return Result.Failure(ResetPasswordErrors.OTPExpired);

                await _unitOfWork.ResetPasswordOTPRepository.DeleteAsync(otp);

                Account? account = await _unitOfWork.AccountRepository.GetAsync(a => a.AccountEmail == request.Email);
                if (account == null)
                    return Result.Failure(ResetPasswordErrors.EmailNotFound);

                account.AccountPassword = await HashPassword.HassPass(request.NewPassword);
                var update = await _unitOfWork.AccountRepository.UpdateAsync(account);
                await _unitOfWork.SaveAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Result.Failure(new Error("500", ex.Message));
            }
        }
    }

}

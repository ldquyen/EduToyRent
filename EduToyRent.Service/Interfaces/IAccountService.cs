using EduToyRent.DAL.Entities;
using EduToyRent.Service.DTOs.AccountDTO;
using EduToyRent.Service.DTOs.ForgotPasswordDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Interfaces
{
	public interface IAccountService
	{
		Task<dynamic> SignUpAccount(SignupAccountDTO signupAccountDTO);
		Task<dynamic> UpdateProfile(EditAccountProfileDTO editAccountProfileDTO, CurrentUserObject currentUserObject);
		Task<dynamic> GetProfile(CurrentUserObject currentUserObject);
		Task<dynamic> ChangePassword(PasswordDTO password, CurrentUserObject currentUserObject);
		Task<dynamic> ViewAllAccount(int page);
		Task<dynamic> ViewAllStaffAccount(int page);
		Task<dynamic> BanUserAccount(int userId);
		Task<dynamic> BanStaffAccount(int userId);
		Task<dynamic> SignUpAccountToySupplier(SignupAccountDTO signupAccountDTO);
		Task<dynamic> SendPasswordResetOTP(ForgotPasswordDto request);
		Task<dynamic> ResetPasswordUsingOTP(ResetPasswordDto request);
		Task<dynamic> SignUpStaffToySupplier(SignupAccountDTO signupAccountDTO);
	}
}

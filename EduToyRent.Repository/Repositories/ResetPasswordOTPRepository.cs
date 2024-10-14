using EduToyRent.DAL.Context;
using EduToyRent.DataAccess.Entities;
using EduToyRent.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Repositories
{
    public class ResetPasswordOTPRepository : Repository<ResetPasswordOTP>, IResetPasswordOTPRepository
    {
		private readonly EduToyRentDbContext _context;
		public ResetPasswordOTPRepository(EduToyRentDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<bool> DeleteAsync(ResetPasswordOTP otp)
		{
			try
			{
				_context.ResetPasswordOTPs.Remove(otp);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return false;
			};
			return true;
		}
	}
}

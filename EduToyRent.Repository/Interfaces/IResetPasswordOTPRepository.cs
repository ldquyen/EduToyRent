﻿using EduToyRent.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Interfaces
{
	public interface IResetPasswordOTPRepository : IRepository<ResetPasswordOTP>
	{

		Task<bool> DeleteAsync(ResetPasswordOTP oTP);

	}
}
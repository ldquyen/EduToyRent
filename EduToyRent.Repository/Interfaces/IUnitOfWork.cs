﻿using EduToyRent.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; } 
		IResetPasswordOTPRepository ResetPasswordOTPRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; } 
        ICartRepository CartRepository { get; } 
		ICartItemRepository CartItemRepository { get; }
        IToyRepository ToyRepository { get; }
        ICategoryRepository CategoryRepository { get; } 
        IRequestFormRepository RequestFormRepository { get; } 
        IOrderRepository OrderRepository { get; } 
        IStatusOrderRepository StatusOrderRepository { get; }
        IVoucherRepository VoucherRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IAccountVoucherRepository AccountVoucherRepository { get; }
        IDepositOrderRepository DepositOrderRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IShipDateRepository ShipDateRepository { get; }
        IReportRepository ReportRepository { get; }
        Task SaveAsync();
    }
}

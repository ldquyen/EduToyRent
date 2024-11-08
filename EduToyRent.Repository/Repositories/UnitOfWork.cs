using EduToyRent.DAL.Context;
using EduToyRent.DAL.Interfaces;
using EduToyRent.DAL.Repositories;
using EduToyRent.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EduToyRentDbContext _context;
        public IAccountRepository AccountRepository { get; set; }
		public IResetPasswordOTPRepository ResetPasswordOTPRepository { get; set; }
        public IRefreshTokenRepository RefreshTokenRepository { get; set; }
        public ICartRepository CartRepository { get; set; }
        public ICartItemRepository CartItemRepository {  get; set; }
        public IToyRepository ToyRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }
        public IRequestFormRepository RequestFormRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public IStatusOrderRepository StatusOrderRepository { get; set; }
        public IOrderDetailRepository OrderDetailRepository { get; set; }
        public IVoucherRepository VoucherRepository { get; set; }
        public IAccountVoucherRepository AccountVoucherRepository   { get; set; }
        public IDepositOrderRepository DepositOrderRepository { get; set; }
        public IPaymentRepository PaymentRepository { get; set; }
        public IShipDateRepository ShipDateRepository {  get; set; }
        public IReportRepository ReportRepository { get; set; }
        public IReportReplyRepository ReportReplyRepository { get; set; }
        public UnitOfWork(EduToyRentDbContext context)
        {
            _context = context;
            AccountRepository = new AccountRepository(_context);
			ResetPasswordOTPRepository = new ResetPasswordOTPRepository(_context);
            RefreshTokenRepository = new RefreshTokenRepository(_context);  
            CartRepository = new CartRepository(_context);
            CartItemRepository = new CartItemRepository(_context);
            ToyRepository = new ToyRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            RequestFormRepository = new RequestFormRepository(_context);
            OrderRepository = new OrderRepository(_context);
            StatusOrderRepository = new StatusOrderRepository(_context);
            OrderDetailRepository = new OrderDetailRepository(_context);
            VoucherRepository = new VoucherRepository(_context);
            AccountVoucherRepository = new AccountVoucherRepository(_context);
            DepositOrderRepository = new DepositOrderRepository(_context);
            PaymentRepository = new PaymentRepository(_context);
            ShipDateRepository = new ShipDateRepository(_context);
            ReportRepository = new ReportRepository(_context);
            ReportReplyRepository = new ReportReplyRepository(_context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

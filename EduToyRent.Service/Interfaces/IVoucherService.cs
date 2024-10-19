using EduToyRent.Service.DTOs.VoucherDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.Service.Interfaces
{
    public interface IVoucherService
    {
        Task<dynamic> CreateVoucher(CreateVoucherDTO createVoucherDTO);
        Task<dynamic> GetListVoucher();
        Task<dynamic> GiveVoucherToAccount(int accountid, int voucherid);
        Task<dynamic> ActiveDeactiveVoucher(int voucherid, int flag);
    }
}

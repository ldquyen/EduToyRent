﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        ICartRepository CartRepository { get; }
        IToyRepository ToyRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IRequestFormRepository RequestFormRepository { get; }
        Task SaveAsync();
    }
}

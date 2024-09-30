using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.BLL.Interfaces
{ 
    public interface IFirebaseService
    {
        Task<string> UploadImage(IFormFile file, string folder);
        Task<string> GetImage(string filePath);
    }
}

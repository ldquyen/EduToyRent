using EduToyRent.BLL.DTOs.AccountDTO;
using static Azure.Core.HttpHeader;
using System.Security.Claims;

namespace EduToyRent.API.Helper
{
    public class TokenHelper
    {
        private static TokenHelper instance;
        public static TokenHelper Instance
        {
            get { if (instance == null) instance = new TokenHelper(); return TokenHelper.instance; }
            private set { TokenHelper.instance = value; }
        }
        public async Task<CurrentUserObject> GetThisUserInfo(HttpContext httpContext)
        {
            CurrentUserObject currentUser = new();

            var checkUser = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (checkUser != null)
            {
                var accountIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "AccountId")?.Value;
                if (int.TryParse(accountIdClaim, out int accountId))
                {
                    currentUser.AccountId = accountId;
                }
                var roleClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (int.TryParse(roleClaim, out int roleId))
                {
                    currentUser.RoleId = roleId;
                }
                else
                {
                    currentUser.RoleId = -1;
                }
                currentUser.AccountEmail = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                currentUser.AccountName = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
                currentUser.PhoneNumber = httpContext.User.Claims.FirstOrDefault(c => c.Type == "Phone").Value;
                return currentUser;
            }
            else
            {
                return null;
            }
        }
    }
}

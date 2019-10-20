using Microsoft.AspNetCore.Http;

namespace BankingApp.Utilities
{
    public class BankingAppContext : Interfaces.IBankingAppContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BankingAppContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            if (int.TryParse(_httpContextAccessor.HttpContext.User?.FindFirst("UserID").Value, out int userId))
            {
                return userId;
            }
            return 0;
        }

        public string GetUsername()
        {
            return _httpContextAccessor.HttpContext.User?.Identity.Name;
        }
    }
}

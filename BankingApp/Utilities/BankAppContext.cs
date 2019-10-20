using Microsoft.AspNetCore.Http;

namespace BankingApp.Utilities
{
    public interface IBankingAppContext
    {
        string GetUsername();
        int GetUserId();
    }

    public class BankingAppContext : IBankingAppContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BankingAppContext(IHttpContextAccessor HttpContextAccessor)
        {
            this._httpContextAccessor = HttpContextAccessor;
        }

        public int GetUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User?.FindFirst("UserID").Value);
        }
        public string GetUsername()
        {
            return _httpContextAccessor.HttpContext.User?.Identity.Name;
        }
    }
}

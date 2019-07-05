using Microsoft.AspNetCore.Http;

namespace BankingApp.Utilities
{
    public interface IBankingAppContext
    {
        string GetUsername();
        int GetUserID();
    }

    public class BankingAppContext : IBankingAppContext
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        public BankingAppContext(IHttpContextAccessor HttpContextAccessor)
        {
            this.HttpContextAccessor = HttpContextAccessor;
        }

        public int GetUserID()
        {
            return int.Parse(HttpContextAccessor.HttpContext.User?.FindFirst("UserID").Value);
        }
        public string GetUsername()
        {
            return HttpContextAccessor.HttpContext.User?.Identity.Name;
        }
    }
}

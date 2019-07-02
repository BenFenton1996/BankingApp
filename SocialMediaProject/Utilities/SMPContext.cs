using Microsoft.AspNetCore.Http;

namespace SocialMediaProject.Utilities
{
    public interface ISMPContext
    {
        string GetUsername();
        int GetUserID();
    }

    public class SMPContext : ISMPContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public SMPContext(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int GetUserID()
        {
            return int.Parse(httpContextAccessor.HttpContext.User?.FindFirst("UserID").Value);
        }
        public string GetUsername()
        {
            return httpContextAccessor.HttpContext.User?.Identity.Name;
        }
    }
}

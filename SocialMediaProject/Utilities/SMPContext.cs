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
        private readonly IHttpContextAccessor HttpContextAccessor;
        public SMPContext(IHttpContextAccessor HttpContextAccessor)
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

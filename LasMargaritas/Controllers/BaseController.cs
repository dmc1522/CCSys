using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;

namespace LasMargaritas.Controllers
{
    public class BaseController : ApiController
    {
        protected string CurrentUserName
        {
            get
            {
                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                string userName = string.Empty;
                foreach (Claim claim in claims)
                {
                    if (claim.Type == ClaimTypes.Name)
                    {
                        userName = claim.Value;
                        break;
                    }
                }
                return userName;
            }
        }
        protected string CurrentUserId
        {
            get
            {
                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                string userId = string.Empty;
                foreach (Claim claim in claims)
                {
                    if (claim.Type == ClaimTypes.NameIdentifier)
                    {
                        userId = claim.Value;
                        break;
                    }
                }
                return userId;
            }
        }

    }
}
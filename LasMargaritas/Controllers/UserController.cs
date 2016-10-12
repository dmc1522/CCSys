using System;
using System.Web.Http;
using LasMargaritas.Models;
using LasMargaritas.BL;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using LasMargaritas.DL.EF;
using System.Net;

namespace LasMargaritas.Controllers
{
    [RoutePrefix("Account")]
    public class UserController : ApiController
    {
        private AuthRepository userRepository; 
       
        #region Constructor & Properties
        public UserController()
        {
               userRepository = new AuthRepository();           
        }

        #endregion

        #region Post Methods
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                IdentityResult result = await userRepository.RegisterUser(user);

                IHttpActionResult errorResult = GetErrorResult(result);

                if (errorResult != null)
                {
                    return errorResult;
                }
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
              
            return Ok();
        }

        #endregion


        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }
            return null;
        }

    }
}

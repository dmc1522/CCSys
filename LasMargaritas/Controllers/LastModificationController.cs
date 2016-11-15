using System;
using System.Web.Http;
using LasMargaritas.Models;
using LasMargaritas.BL;
using System.Configuration;
using System.Collections.Generic;

namespace LasMargaritas.Controllers
{
    [RoutePrefix("LastModification")]
    public class LastModificationController : ApiController
    {
        private LastModificationBL lastModificationBL;
        #region Constructor & Properties

        public LastModificationController()
        {
            string connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDBDev;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            lastModificationBL = new LastModificationBL(connectionString);
        }
        #endregion

        #region Get Methods
        [Authorize(Roles = "Admin")]
        [Route("GetLastModification")]
        [HttpGet]
        public IHttpActionResult GetLastModification(Module module)
        {
            GetLastModificationResponse response = new GetLastModificationResponse();
            try
            {
                List<LastModification> lastModifications = new List<LastModification>();
                LastModification lastModification = lastModificationBL.GetLastModification(module);
                lastModifications.Add(lastModification);
                response.LastModifications = lastModifications;
                response.Success = true;
            }           
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Success = false;
            }
            return Ok(response);
        }
       
        #endregion
    }
}
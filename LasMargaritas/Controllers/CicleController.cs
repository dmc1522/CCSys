using System;
using System.Web.Http;
using LasMargaritas.Models;
using LasMargaritas.BL;
using System.Configuration;
using System.Collections.Generic;

namespace LasMargaritas.Controllers
{
    [RoutePrefix("Cicle")]
    public class CicleController : ApiController
    {
        private CicleBL cicleBL;

        #region Constructor & Properties

        public CicleController()
        {
            string connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDBDev;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            cicleBL = new CicleBL(connectionString);
        }
        #endregion

        #region Post Methods
        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Add(Cicle cicle)
        {
            CicleResponse response = new CicleResponse();
            try
            {
                //cicle.UserId = CurrentUserId;
                Cicle cicleSaved = cicleBL.InsertCicle(cicle);
                response.Cicle = cicleSaved;
                response.Success = true;
            }
            catch (CicleException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Cicle = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Cicle = null;
                response.Success = false;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Update")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Update(Cicle cicle)
        {
            CicleResponse response = new CicleResponse();
            try
            {
                Cicle cicleSaved = cicleBL.UpdateCicle(cicle);
                response.Cicle = cicleSaved;
                response.Success = true;
            }
            catch (CicleException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Cicle = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Cicle = null;
                response.Success = false;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Delete")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Delete(IdModel id)
        {
            CicleResponse response = new CicleResponse();
            try
            {
                bool success = cicleBL.DeleteCicle(id.Id);
                response.Success = success;
            }
            catch (CicleException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Cicle = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Cicle = null;
                response.Success = false;
            }
            return Ok(response);
        }

        #endregion

        #region Get Methods
        [Authorize(Roles = "Admin")]
        [Route("GetAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            GetCicleResponse response = new GetCicleResponse();
            try
            {
                List<Cicle> cicles = cicleBL.GetCicle();
                response.Cicles = cicles;
                response.Success = true;
            }
            catch (CicleException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Success = false;
            }
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [Route("GetById")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            GetCicleResponse response = new GetCicleResponse();
            try
            {
                List<Cicle> cicles = cicleBL.GetCicle(id);
                response.Cicles = cicles;
                response.Success = true;
            }
            catch (CicleException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Success = false;
            }
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [Route("GetSelectableModels")]
        [HttpGet]
        public IHttpActionResult GetSelectableModels()
        {
            GetSelectableModelResponse response = new GetSelectableModelResponse();
            try
            {
                List<SelectableModel> cicles = cicleBL.GetBasicModels();
                response.SelectableModels = cicles;
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
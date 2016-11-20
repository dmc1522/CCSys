using System;
using System.Web.Http;
using LasMargaritas.Models;
using LasMargaritas.BL;
using System.Configuration;
using System.Collections.Generic;


namespace LasMargaritas.Controllers
{
    [RoutePrefix("Rancher")]
    public class RancherController : ApiController
    {
        private RancherBL rancherBL;

        #region Constructor & Properties
        public RancherController()
        {
            string connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDBDev;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            rancherBL = new RancherBL(connectionString);
        }

        #endregion

        #region Post Methods


        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Add(Rancher rancher)
        {
            RancherResponse response = new RancherResponse();
            try
            {
                Rancher rancherSaved = rancherBL.InsertRancher(rancher);
                response.Rancher = rancherSaved;
                response.Success = true;
            }
            catch (RancherException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Rancher = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Rancher = null;
                response.Success = false;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Update")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Update(Rancher rancher)
        {
            RancherResponse response = new RancherResponse();
            try
            {
                Rancher rancherSaved = rancherBL.UpdateRancher(rancher);
                response.Rancher = rancherSaved;
                response.Success = true;
            }
            catch (RancherException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Rancher = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Rancher = null;
                response.Success = false;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Delete")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Delete(IdModel id)
        {
            RancherResponse response = new RancherResponse();
            try
            {
                bool success = rancherBL.DeleteRancher(id.Id);
                response.Success = success;
            }
            catch (RancherException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Rancher = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Rancher = null;
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
            GetRancherResponse response = new GetRancherResponse();
            try
            {
                List<Rancher> ranchers = rancherBL.GetRancher();
                response.Ranchers = ranchers;
                response.Success = true;
            }
            catch (RancherException ex)
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
                List<SelectableModel> ranchers = rancherBL.GetBasicModels();
                response.SelectableModels = ranchers;
                response.Success = true;
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
            GetRancherResponse response = new GetRancherResponse();
            try
            {
                List<Rancher> ranchers = rancherBL.GetRancher(id);
                response.Ranchers = ranchers;
                response.Success = true;
            }
            catch (RancherException ex)
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
        #endregion
    }
}
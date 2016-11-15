using System;
using System.Web.Http;
using LasMargaritas.Models;
using LasMargaritas.BL;
using System.Configuration;
using System.Collections.Generic;

namespace LasMargaritas.Controllers
{
    [RoutePrefix("Catalog")]
    public class CatalogController : ApiController
    {
        private CatalogBL catalogBL;

        #region Constructor & Properties

        public CatalogController()
        {
            string connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDBDev;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            catalogBL = new CatalogBL(connectionString);
        }
        #endregion 

        #region Get Methods
        [Authorize(Roles = "Admin")]
        [Route("GetStates")]
        [HttpGet]
        public IHttpActionResult GetStates()
        {
            GetSelectableModelResponse response = new GetSelectableModelResponse();
            try
            {
                List<SelectableModel> states = catalogBL.GetStates();
                response.SelectableModels = states;
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
        [Route("GetGenders")]
        [HttpGet]
        public IHttpActionResult GetGenders()
        {
            GetSelectableModelResponse response = new GetSelectableModelResponse();
            try
            {
                List<SelectableModel> genders = catalogBL.GetGenders();
                response.SelectableModels = genders;
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
        [Route("GetCivilStatus")]
        [HttpGet]
        public IHttpActionResult GetCivilStatus()
        {
            GetSelectableModelResponse response = new GetSelectableModelResponse();
            try
            {
                List<SelectableModel> civilStatus = catalogBL.GetCivilStatus();
                response.SelectableModels = civilStatus;
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
        [Route("GetRegimes")]
        [HttpGet]
        public IHttpActionResult GetRegimes()
        {
            GetSelectableModelResponse response = new GetSelectableModelResponse();
            try
            {
                List<SelectableModel> regimes = catalogBL.GetRegimes();
                response.SelectableModels = regimes;
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
        [Route("GetAgriculturalBrands")]
        [HttpGet]
        public IHttpActionResult GetAgriculturalBrands()
        {
            GetSelectableModelResponse response = new GetSelectableModelResponse();
            try
            {
                List<SelectableModel> brands = catalogBL.GetAgriculturalBrands();
                response.SelectableModels = brands;
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
        [Route("GetPresentations")]
        [HttpGet]
        public IHttpActionResult GetPresentations()
        {
            GetSelectableModelResponse response = new GetSelectableModelResponse();
            try
            {
                List<SelectableModel> presentations = catalogBL.GetPresentations();
                response.SelectableModels = presentations;
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
        [Route("GetProductGroups")]
        [HttpGet]
        public IHttpActionResult GetProductGroups()
        {
            GetSelectableModelResponse response = new GetSelectableModelResponse();
            try
            {
                List<SelectableModel> groups = catalogBL.GetProductGroups();
                response.SelectableModels = groups;
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
        [Route("GetProductGroups")]
        [HttpGet]
        public IHttpActionResult GetUnits()
        {
            GetSelectableModelResponse response = new GetSelectableModelResponse();
            try
            {
                List<SelectableModel> units = catalogBL.GetUnits();
                response.SelectableModels = units;
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
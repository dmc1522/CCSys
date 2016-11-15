using System;
using System.Web.Http;
using LasMargaritas.Models;
using LasMargaritas.BL;
using System.Configuration;
using System.Collections.Generic;


namespace LasMargaritas.Controllers
{
    [RoutePrefix("WareHouse")]
    public class WareHouseController : ApiController
    {
        private WareHouseBL wareHouseBL;
        #region Constructor & Properties

        public WareHouseController()
        {
            string connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDBDev;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            wareHouseBL = new WareHouseBL(connectionString);
        }
        #endregion
        #region Post Methods
        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Add(WareHouse wareHouse)
        {
            WareHouseResponse response = new WareHouseResponse();
            try
            {
                WareHouse wareHouseSaved = wareHouseBL.InsertWareHouse(wareHouse);
                response.WareHouse = wareHouseSaved;
                response.Success = true;
            }
            catch (WareHouseException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.WareHouse = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.WareHouse = null;
                response.Success = false;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Update")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Update(WareHouse wareHouse)
        {
            WareHouseResponse response = new WareHouseResponse();
            try
            {
                WareHouse wareHouseSaved = wareHouseBL.UpdateWareHouse(wareHouse);
                response.WareHouse = wareHouseSaved;
                response.Success = true;
            }
            catch (WareHouseException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.WareHouse = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.WareHouse = null;
                response.Success = false;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Delete")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Delete(IdModel id)
        {
            WareHouseResponse response = new WareHouseResponse();
            try
            {
                bool success = wareHouseBL.DeleteWareHouse(id.Id);
                response.Success = success;
            }
            catch (WareHouseException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.WareHouse = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.WareHouse = null;
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
            GetWareHouseResponse response = new GetWareHouseResponse();
            try
            {
                List<WareHouse> wareHouses = wareHouseBL.GetWareHouse();
                response.WareHouses = wareHouses;
                response.Success = true;
            }
            catch (WareHouseException ex)
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
            GetWareHouseResponse response = new GetWareHouseResponse();
            try
            {
                List<WareHouse> wareHouses = wareHouseBL.GetWareHouse(id);
                response.WareHouses = wareHouses;
                response.Success = true;
            }
            catch (WareHouseException ex)
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
using System;
using System.Web.Http;
using LasMargaritas.Models;
using LasMargaritas.BL;
using System.Configuration;
using System.Collections.Generic;


namespace LasMargaritas.Controllers
{
    [RoutePrefix("SaleCustomer")]
    public class SaleCustomerController : ApiController
    {
        private SaleCustomerBL saleCustomerBL;

        #region Constructor & Properties
        public SaleCustomerController()
        {
            string connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDBDev;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            saleCustomerBL = new SaleCustomerBL(connectionString);
        }

        #endregion

        #region Post Methods


        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Add(SaleCustomer saleCustomer)
        {
            SaleCustomerResponse response = new SaleCustomerResponse();
            try
            {
                SaleCustomer saleCustomerSaved = saleCustomerBL.InsertSaleCustomer(saleCustomer);
                response.SaleCustomer = saleCustomerSaved;
                response.Success = true;
            }
            catch (SaleCustomerException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.SaleCustomer = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.SaleCustomer = null;
                response.Success = false;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Update")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Update(SaleCustomer saleCustomer)
        {
            SaleCustomerResponse response = new SaleCustomerResponse();
            try
            {
                SaleCustomer saleCustomerSaved = saleCustomerBL.UpdateSaleCustomer(saleCustomer);
                response.SaleCustomer = saleCustomerSaved;
                response.Success = true;
            }
            catch (SaleCustomerException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.SaleCustomer = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.SaleCustomer = null;
                response.Success = false;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Delete")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Delete(IdModel id)
        {
            SaleCustomerResponse response = new SaleCustomerResponse();
            try
            {
                bool success = saleCustomerBL.DeleteSaleCustomer(id.Id);
                response.Success = success;
            }
            catch (SaleCustomerException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.SaleCustomer = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.SaleCustomer = null;
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
            GetSaleCustomerResponse response = new GetSaleCustomerResponse();
            try
            {
                List<SaleCustomer> saleCustomers = saleCustomerBL.GetSaleCustomer();
                response.SaleCustomers = saleCustomers;
                response.Success = true;
            }
            catch (SaleCustomerException ex)
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
                List<SelectableModel> saleCustomers = saleCustomerBL.GetBasicModels();
                response.SelectableModels = saleCustomers;
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
            GetSaleCustomerResponse response = new GetSaleCustomerResponse();
            try
            {
                List<SaleCustomer> saleCustomers = saleCustomerBL.GetSaleCustomer(id);
                response.SaleCustomers = saleCustomers;
                response.Success = true;
            }
            catch (SaleCustomerException ex)
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
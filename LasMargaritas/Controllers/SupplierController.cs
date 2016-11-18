using System;
using System.Web.Http;
using LasMargaritas.Models;
using LasMargaritas.BL;
using System.Configuration;
using System.Collections.Generic;


namespace LasMargaritas.Controllers
{
    [RoutePrefix("Supplier")]
    public class SupplierController : ApiController
    {
        private SupplierBL supplierBL;

        #region Constructor & Properties
        public SupplierController()
        {
            string connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDBDev;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            supplierBL = new SupplierBL(connectionString);
        }

        #endregion

        #region Post Methods


        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Add(Supplier supplier)
        {
            SupplierResponse response = new SupplierResponse();
            try
            {
                Supplier supplierSaved = supplierBL.InsertSupplier(supplier);
                response.Supplier = supplierSaved;
                response.Success = true;
            }
            catch (SupplierException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Supplier = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Supplier = null;
                response.Success = false;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Update")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Update(Supplier supplier)
        {
            SupplierResponse response = new SupplierResponse();
            try
            {
                Supplier supplierSaved = supplierBL.UpdateSupplier(supplier);
                response.Supplier = supplierSaved;
                response.Success = true;
            }
            catch (SupplierException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Supplier = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Supplier = null;
                response.Success = false;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Delete")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Delete(IdModel id)
        {
            SupplierResponse response = new SupplierResponse();
            try
            {
                bool success = supplierBL.DeleteSupplier(id.Id);
                response.Success = success;
            }
            catch (SupplierException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Supplier = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Supplier = null;
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
            GetSupplierResponse response = new GetSupplierResponse();
            try
            {
                List<Supplier> suppliers = supplierBL.GetSupplier();
                response.Suppliers = suppliers;
                response.Success = true;
            }
            catch (SupplierException ex)
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
            GetSupplierResponse response = new GetSupplierResponse();
            try
            {
                List<Supplier> suppliers = supplierBL.GetSupplier(id);
                response.Suppliers = suppliers;
                response.Success = true;
            }
            catch (SupplierException ex)
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
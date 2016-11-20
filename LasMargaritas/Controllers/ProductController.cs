using System;
using System.Web.Http;
using LasMargaritas.Models;
using LasMargaritas.BL;
using System.Configuration;
using System.Collections.Generic;

namespace LasMargaritas.Controllers
{
    [RoutePrefix("Product")]
    public class ProductController : ApiController
    {
        private ProductBL productBL;
        #region Constructor & Properties
        
        public ProductController()
        {           
            string connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDBDev;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            productBL = new ProductBL(connectionString);
        }
        #endregion

        #region Post Methods
        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Add(Product product)
        {
            ProductResponse response = new ProductResponse();
            try
            {
                Product productSaved = productBL.InsertProduct(product);
                response.Product = productSaved;
                response.Success = true;
            }
            catch (ProductException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Product = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Product = null;
                response.Success = false;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Update")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Update(Product product)
        {
            ProductResponse response = new ProductResponse();
            try
            {
                Product productSaved = productBL.UpdateProduct(product);
                response.Product = productSaved;
                response.Success = true;
            }
            catch (ProductException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Product = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Product = null;
                response.Success = false;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Delete")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Delete(IdModel id)
        {
            ProductResponse response = new ProductResponse();
            try
            {
                bool success = productBL.DeleteProduct(id.Id);
                response.Success = success;
            }
            catch (ProductException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Product = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Product = null;
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
            GetProductResponse response = new GetProductResponse();
            try
            {
                List<Product> products = productBL.GetProduct();
                response.Products = products;
                response.Success = true;
            }
            catch (ProductException ex)
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
            GetProductResponse response = new GetProductResponse();
            try
            {
                List<Product> products = productBL.GetProduct(id);
                response.Products = products;
                response.Success = true;
            }
            catch (ProductException ex)
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
        [Route("GetWeightTicketProducts")]
        [HttpGet]
        public IHttpActionResult GetWeightTicketProducts()
        {
            GetSelectableModelResponse response = new GetSelectableModelResponse();
            try
            {
                List<SelectableModel> products = productBL.GetWeightTicketProducts();
                response.SelectableModels = products;
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
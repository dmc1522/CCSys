using System;
using System.Web.Http;
using LasMargaritas.Models;
using LasMargaritas.BL;
using System.Configuration;
using System.Collections.Generic;


namespace LasMargaritas.Controllers
{
    [RoutePrefix("Producer")]
    public class ProducerController: ApiController
    {
        private ProducerBL producerBL;

        #region Constructor & Properties
        public ProducerController()
        {
            string connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDBDev;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            producerBL = new ProducerBL(connectionString);
        }

        #endregion

        #region Post Methods


        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Add(Producer producer)
        {
            ProducerResponse response = new ProducerResponse();
            try
            {
                Producer producerSaved = producerBL.InsertProducer(producer);
                response.Producer = producerSaved;
                response.Success = true;
            }
            catch (ProducerException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Producer = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Producer = null;
                response.Success = false;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Update")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Update(Producer producer)
        {
            ProducerResponse response = new ProducerResponse();
            try
            {
                Producer producerSaved = producerBL.UpdateProducer(producer);
                response.Producer = producerSaved;
                response.Success = true;
            }
            catch (ProducerException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Producer = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Producer = null;
                response.Success = false;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Delete")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Delete(IdModel id)
        {
            ProducerResponse response = new ProducerResponse();
            try
            {
                bool success = producerBL.DeleteProducer(id.Id);
                response.Success = success;
            }
            catch (ProducerException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Producer = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Producer = null;
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
            GetProducerResponse response = new GetProducerResponse();
            try
            {
                List<Producer> producers = producerBL.GetProducer();
                response.Producers = producers;
                response.Success = true;
            }
            catch (ProducerException ex)
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
                List<SelectableModel> producers = producerBL.GetBasicModels();
                response.SelectableModels = producers;
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
            GetProducerResponse response = new GetProducerResponse();
            try
            {
                List<Producer> producers = producerBL.GetProducer(id);
                response.Producers = producers;
                response.Success = true;
            }
            catch (ProducerException ex)
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
using System;
using System.Web.Http;
using LasMargaritas.Models;
using LasMargaritas.BL;
using System.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System.Data;
using System.Linq;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace LasMargaritas.Controllers
{
    [RoutePrefix("Settlement")]
    public class SettlementController : BaseController
    {

        private SettlementBL settlementBL; 
       
        #region Constructor & Properties
        public SettlementController()
        {
            string connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDBDev;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;               
            }
            settlementBL = new SettlementBL(connectionString);
        }

        #endregion

        #region Post Methods

        [HttpPost]
        [Route("Print")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage Print(IdModel id)
        {
            try
            {
                MemoryStream result = settlementBL.PrintSettlement(id.Id);
                var response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StreamContent(result)
                };
                response.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "MyPdf.pdf"
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                return response;
            }
            catch(Exception ex)
            {
                var response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(ex.Message)
                };
                return response;
            }
           
        }

        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Add(Settlement settlement)
        {
            SettlementResponse response = new SettlementResponse();
            try
            {
                settlement.User = CurrentUserId;
                Settlement settlementSaved = settlementBL.InsertSettlement(settlement);
                response.Settlement = settlementSaved;
                response.Success = true;                
            }
            catch (SettlementException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Settlement = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Settlement = null;
                response.Success = false;
            }
            return Ok(response);
        }
        #region Payments
        [HttpPost]
        [Route("AddSettlementPayment")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult AddSettlementPayment(SettlementPayment payment)
        {
            SettlementPaymentResponse response = new SettlementPaymentResponse();
            try
            {
                payment.User = CurrentUserId;
                SettlementPayment paymentSaved = settlementBL.AddSettlementPayment(payment);
                response.Payment = paymentSaved;
                response.Success = true;
            }
            catch (SettlementException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Payment = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Payment = null;
                response.Success = false;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("DeleteAllSettlementPayments")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteAllSettlementPayments(IdModel settlementId) 
        {
            SettlementPaymentResponse response = new SettlementPaymentResponse();
            try
            {               
                settlementBL.DeleteAllSettlementPayments(settlementId.Id);
                response.Payment = null;
                response.Success = true;
            }
            catch (SettlementException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Payment = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Payment = null;
                response.Success = false;
            }
            return Ok(response);
        }
        [HttpPost]
        [Route("DeleteSettlementPayment")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteSettlementPayment(IdModel payment)
        {
            SettlementPaymentResponse response = new SettlementPaymentResponse();
            try
            {
                settlementBL.DeleteSettlementPayment(payment.Id);
                response.Payment = null;
                response.Success = true;
            }
            catch (SettlementException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Payment = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Payment = null;
                response.Success = false;
            }
            return Ok(response);
        }


        [HttpGet]
        [Route("GetSettlementPayments")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetSettlementPayments(int settlementId)
        {
            GetSettlementPaymentResponse response = new GetSettlementPaymentResponse();
            try
            {
                List<SettlementPayment> payments = settlementBL.GetSettlementPayments(settlementId);
                response.Payments = payments;
                response.Success = true;
            }
            catch (SettlementException ex)
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


        [HttpPost]
        [Route("Update")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Update(Settlement settlement)
        {
            SettlementResponse response = new SettlementResponse();
            try
            {
                settlement.User = CurrentUserId;
                Settlement settlementSaved = settlementBL.UpdateSettlement(settlement);
                response.Settlement = settlementSaved;
                response.Success = true;
            }
            catch (SettlementException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Settlement = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Settlement = null;
                response.Success = false;
            }
            return Ok(response);
        }
        [HttpPost]
        [Route("Delete")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Delete(IdModel id)
        {            
            SettlementResponse response = new SettlementResponse();
            try
            {
                bool success = settlementBL.DeleteSettlement(id.Id);                
                response.Success = success;                
            }
            catch (SettlementException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.Settlement = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.Settlement = null;
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
            GetSettlementResponse response = new GetSettlementResponse();
            try
            {
                List<Settlement> settlements = settlementBL.GetSettlement();
                response.Settlements = settlements;
                response.Success = true;
            }
            catch (SettlementException ex)
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
        [Route("GetAllByCicleId")]
        [HttpGet]
        public IHttpActionResult GetAllByCicleId(int cicleId)
        {
            GetSettlementResponse response = new GetSettlementResponse();
            try
            {
                List<Settlement> settlements = settlementBL.GetSettlement(null, cicleId);
                response.Settlements = settlements;
                response.Success = true;
            }
            catch (SettlementException ex)
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
            GetSettlementResponse response = new GetSettlementResponse();
            try
            {
                List<Settlement> settlement = settlementBL.GetSettlement(id);
                response.Settlements = settlement;
                response.Success = true;
            }
            catch (SettlementException ex)
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

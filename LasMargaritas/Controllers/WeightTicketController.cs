using System;
using System.Web.Http;
using LasMargaritas.Models;
using LasMargaritas.BL;
using System.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System.Data;
using System.Linq;

namespace LasMargaritas.Controllers
{
    [RoutePrefix("WeightTicket")]
    public class WeightTicketController : BaseController
    {

        private WeightTicketsBL weightTicketsBL; 
       
        #region Constructor & Properties
        public WeightTicketController()
        {
            string connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDBDev;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;               
            }
            weightTicketsBL = new WeightTicketsBL(connectionString);
        }

        #endregion

        #region Post Methods
        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Add(WeightTicket weightTicket)
        {
            WeightTicketResponse response = new WeightTicketResponse();
            try
            {
                weightTicket.UserId = CurrentUserId;
                WeightTicket weightTicketSaved =  weightTicketsBL.InsertWeightTicket(weightTicket);
                response.WeightTicket = weightTicketSaved;
                response.Success = true;                
            }
            catch (WeightTicketException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.WeightTicket = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.WeightTicket = null;
                response.Success = false;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Update")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Update(WeightTicket weightTicket)
        {
            WeightTicketResponse response = new WeightTicketResponse();
            try
            {
                weightTicket.UserId = CurrentUserId;
                WeightTicket weightTicketSaved = weightTicketsBL.UpdateWeightTicket(weightTicket);
                response.WeightTicket = weightTicketSaved;
                response.Success = true;                
            }
            catch (WeightTicketException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.WeightTicket = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.WeightTicket = null;
                response.Success = false;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Delete")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Delete(IdModel id)
        {            
            WeightTicketResponse response = new WeightTicketResponse();
            try
            {
                bool success = weightTicketsBL.DeleteWeightTicket(id.Id);                
                response.Success = success;                
            }
            catch (WeightTicketException ex)
            {
                response.ErrorCode = ex.Error;
                response.ErrorMessage = "Error. " + ex.Error.ToString();
                response.WeightTicket = null;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error. " + ex.Message;
                response.WeightTicket = null;
                response.Success = false;
            }
            return Ok(response);
        }

        #endregion

        #region Get Methods

        [HttpGet]
        [Route("GetWeightTicketsReport")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult  GetWeightTicketsReport([FromUri]WeightTicketReportFilterModel filters)
        {
            GetReportDataResponse response = new GetReportDataResponse();
            try
            {
                response.ReportData = weightTicketsBL.GetWeightTicketsReport(filters);                
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
        [Route("GetWeightTicketsInSettlementFullDetails")]
        [HttpGet]
        public IHttpActionResult GetWeightTicketsInSettlementFullDetails(int settlementId)
        {
            GetWeightTicketResponse response = new GetWeightTicketResponse();
            try
            {
                List<WeightTicket> tickets = weightTicketsBL.GetWeightTicketsInSettlementFullDetails(settlementId);
                response.WeightTickets = tickets;
                response.Success = true;
            }
            catch (WeightTicketException ex)
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
        [Route("GetWeightTicketsAvailablesToSettleFullDetails")]
        [HttpGet]
        public IHttpActionResult GetWeightTicketsAvailablesToSettleFullDetails(int cicleId, int producerId)
        {
            GetWeightTicketResponse response = new GetWeightTicketResponse();
            try
            {
                List<WeightTicket> tickets = weightTicketsBL.GetWeightTicketsAvailablesToSettleFullDetails(cicleId, producerId);
                response.WeightTickets = tickets;
                response.Success = true;
            }
            catch (WeightTicketException ex)
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
        [Route("GetAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            GetWeightTicketResponse response = new GetWeightTicketResponse();
            try
            {
                List<WeightTicket> tickets = weightTicketsBL.GetWeightTicket();
                response.WeightTickets = tickets;
                response.Success = true;
            }
            catch (WeightTicketException ex)
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
            GetWeightTicketResponse response = new GetWeightTicketResponse();
            try
            {
                List<WeightTicket> tickets = weightTicketsBL.GetWeightTicket(id);
                response.WeightTickets = tickets;
                response.Success = true;
            }
            catch (WeightTicketException ex)
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
        public IHttpActionResult GetSelectableModels(int cicleId, bool onlyPendingTickets)
        {
            GetSelectableModelResponse response = new GetSelectableModelResponse();
            try
            {
                List<SelectableModel> weightTickets = weightTicketsBL.GetSelectableModels(cicleId, onlyPendingTickets);
                response.SelectableModels = weightTickets;
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

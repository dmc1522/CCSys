using System;
using System.Web.Http;
using LasMargaritas.Models;
using LasMargaritas.BL;
using System.Configuration;
using System.Collections.Generic;

namespace LasMargaritas.Controllers
{
    [RoutePrefix("WeightTicket")]
    public class WeightTicketController : ApiController
    {

        private WeightTicketsBL weightTicketsBL; 
       
        #region Constructor & Properties
        public WeightTicketController()
        {
            string connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDB;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;               
            }
            weightTicketsBL = new WeightTicketsBL(connectionString);
        }

        #endregion

        #region Post Methods
        [HttpPost]
        public AddWeightTicketResponse AddWeightTicket(WeightTicket weightTicket)
        {
            AddWeightTicketResponse response = new AddWeightTicketResponse();
            try
            {
                WeightTicket WeightTicketSaved =  weightTicketsBL.InsertWeightTicket(weightTicket);
                response.WeightTicket = WeightTicketSaved;
                response.Success = true;
                response.Message = "WeightTicket agregada exitosamente";
            }
            catch(Exception ex)
            {
                response.Message = "Error. " + ex.Message;
                response.WeightTicket = null;
                response.Success = false;
            }
            return response;
        }
      
        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            GetWeightTicketResponse response = new GetWeightTicketResponse();
            List<WeightTicket> tickets = weightTicketsBL.GetWeightTicket(); ;
            response.WeightTickets = tickets;
            response.Success = true;
            return Ok(weightTicketsBL.GetWeightTicket());
        }

        #endregion

    }
}

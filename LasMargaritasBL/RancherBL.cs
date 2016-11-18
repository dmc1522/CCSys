using LasMargaritas.DL;
using LasMargaritas.Models;
using System.Collections.Generic;


namespace LasMargaritas.BL
{
    public class RancherBL
    {
        private RancherDL rancherDL;
        private const int WeightTicketRancherGroupId = 3;
        public RancherBL(string connectionString)
        {
            rancherDL = new RancherDL(connectionString);
        }

        public Rancher InsertRancher(Rancher customer)
        {
            //Add validations here!
            RancherError result = RancherError.None;
            if (string.IsNullOrEmpty(customer.Name))
                result |= RancherError.InvalidName;
            if (customer.StateId <= 0)
                result |= RancherError.InvalidState;
            if (result != RancherError.None)
                throw new RancherException(result);
            else
                return rancherDL.InsertRancher(customer);
        }

        public Rancher UpdateRancher(Rancher customer)
        {
            //Add validations here!
            RancherError result = RancherError.None;
            if (string.IsNullOrEmpty(customer.Name))
                result |= RancherError.InvalidName;
            if (customer.StateId <= 0)
                result |= RancherError.InvalidState;
            if (result != RancherError.None)
                throw new RancherException(result);
            else
                return rancherDL.UpdateRancher(customer);
        }

        public List<Rancher> GetRancher(int? id = null)
        {
            //Add validations here!
            RancherError result = RancherError.None;
            if (id != null && id <= 0)
                result |= RancherError.InvalidId;

            if (result != RancherError.None)
                throw new RancherException(result);
            else
                return rancherDL.GetRancher(id);
        }

        public bool DeleteRancher(int id)
        {
            //Add validations here!
            RancherError result = RancherError.None;
            if (id <= 0)
                result |= RancherError.InvalidId;

            if (result != RancherError.None)
                throw new RancherException(result);
            else
                return rancherDL.DeleteRancher(id);
        }

    }
}

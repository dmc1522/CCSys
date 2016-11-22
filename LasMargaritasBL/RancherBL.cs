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

        public Rancher InsertRancher(Rancher rancher)
        {
            //Add validations here!
            RancherError result = RancherError.None;
            if (string.IsNullOrEmpty(rancher.Name))
                result |= RancherError.InvalidName;
            if (rancher.StateId <= 0)
                result |= RancherError.InvalidState;
            if (result != RancherError.None)
                throw new RancherException(result);
            else
                return rancherDL.InsertRancher(rancher);
        }

        public Rancher UpdateRancher(Rancher rancher)
        {
            //Add validations here!
            RancherError result = RancherError.None;
            if (string.IsNullOrEmpty(rancher.Name))
                result |= RancherError.InvalidName;
            if (rancher.StateId <= 0)
                result |= RancherError.InvalidState;
            if (result != RancherError.None)
                throw new RancherException(result);
            else
                return rancherDL.UpdateRancher(rancher);
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

        public List<SelectableModel> GetBasicModels()
        {
            return rancherDL.GetBasicModels();
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

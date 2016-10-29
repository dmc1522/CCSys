using LasMargaritas.DL;
using LasMargaritas.Models;
using System.Collections.Generic;


namespace LasMargaritas.BL
{
    public class CicleBL
    {
        private CicleDL cicleDL;

        public CicleBL(string connectionString)
        {
            cicleDL = new CicleDL(connectionString);
        }

        public Cicle InsertCicle(Cicle cicle)
        {
            CicleError result = CicleError.None;
            if (string.IsNullOrEmpty(cicle.Name))
                result |= CicleError.InvalidName;
            if (cicle.StartDate == System.DateTime.MinValue || cicle.StartDate == System.DateTime.MaxValue) 
                result |= CicleError.InvalidStartDate;
            if (cicle.EndDateZone1 == System.DateTime.MinValue || cicle.EndDateZone1 == System.DateTime.MaxValue)
                result |= CicleError.InvalidDateZone1;
            if (cicle.EndDateZone2 == System.DateTime.MinValue || cicle.EndDateZone2 == System.DateTime.MaxValue)
                result |= CicleError.InvalidDateZone2;
            if (cicle.AmountPerHectarea <= 0)
                result |= CicleError.InvalidAmount;
            if (result != CicleError.None)
                throw new CicleException(result);
            else
                return cicleDL.InsertCicle(cicle);
        }

        public Cicle UpdateCicle(Cicle cicle)
        {
            CicleError result = CicleError.None;
            if (string.IsNullOrEmpty(cicle.Name))
                result |= CicleError.InvalidName;
            if (cicle.StartDate == System.DateTime.MinValue || cicle.StartDate == System.DateTime.MaxValue)
                result |= CicleError.InvalidStartDate;
            if (cicle.EndDateZone1 == System.DateTime.MinValue || cicle.EndDateZone1 == System.DateTime.MaxValue)
                result |= CicleError.InvalidDateZone1;
            if (cicle.EndDateZone2 == System.DateTime.MinValue || cicle.EndDateZone2 == System.DateTime.MaxValue)
                result |= CicleError.InvalidDateZone2;
            if (cicle.AmountPerHectarea <= 0)
                result |= CicleError.InvalidAmount;
            if (result != CicleError.None)
                throw new CicleException(result);
            else
                return cicleDL.UpdateCicle(cicle);
        }

        public List<Cicle> GetCicle(int? id = null)
        {
            //Add validations here!
            CicleError result = CicleError.None;
            if (id != null && id <= 0)
                result |= CicleError.InvalidId;

            if (result != CicleError.None)
                throw new CicleException(result);
            else
                return cicleDL.GetCicle(id);
        }

        public bool DeleteCicle(int id)
        {
            //Add validations here!
            CicleError result = CicleError.None;
            if (id <= 0)
                result |= CicleError.InvalidId;

            if (result != CicleError.None)
                throw new CicleException(result);
            else
                return cicleDL.DeleteCicle(id);
        }

    }
}

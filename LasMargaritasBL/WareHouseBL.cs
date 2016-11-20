using LasMargaritas.DL;
using LasMargaritas.Models;
using System.Collections.Generic;


namespace LasMargaritas.BL
{
    public class WareHouseBL
    {
        private WareHouseDL wareHouseDL;

        public WareHouseBL(string connectionString)
        {
            wareHouseDL = new WareHouseDL(connectionString);
        }

        public List<SelectableModel> GetBasicModels()
        {
            return wareHouseDL.GetBasicModels();
        }

        public WareHouse InsertWareHouse(WareHouse wareHouse)
        {
            //Add validations here!
            WareHouseError result = WareHouseError.None;
            if (string.IsNullOrEmpty(wareHouse.Name))
                result |= WareHouseError.InvalidName;
            if (string.IsNullOrEmpty(wareHouse.Address))
                result |= WareHouseError.InvalidAddress;
            if (result != WareHouseError.None)
                throw new WareHouseException(result);
            else
                return wareHouseDL.InsertWareHouse(wareHouse);
        }

        public WareHouse UpdateWareHouse(WareHouse wareHouse)
        {
            //Add validations here!
            WareHouseError result = WareHouseError.None;
            if (string.IsNullOrEmpty(wareHouse.Name))
                result |= WareHouseError.InvalidName;
            if (string.IsNullOrEmpty(wareHouse.Address))
                result |= WareHouseError.InvalidAddress;
            if (result != WareHouseError.None)
                throw new WareHouseException(result);
            else
                return wareHouseDL.UpdateWareHouse(wareHouse);
        }

        public List<WareHouse> GetWareHouse(int? id = null)
        {
            //Add validations here!
            WareHouseError result = WareHouseError.None;
            if (id != null && id <= 0)
                result |= WareHouseError.InvalidId;

            if (result != WareHouseError.None)
                throw new WareHouseException(result);
            else
                return wareHouseDL.GetWareHouse(id);
        }

        public bool DeleteWareHouse(int id)
        {
            //Add validations here!
            WareHouseError result = WareHouseError.None;
            if (id <= 0)
                result |= WareHouseError.InvalidId;

            if (result != WareHouseError.None)
                throw new WareHouseException(result);
            else
                return wareHouseDL.DeleteWareHouse(id);
        }

    }
}

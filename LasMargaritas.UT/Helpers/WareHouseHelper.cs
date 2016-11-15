using LasMargaritas.Models;
using System;


namespace LasMargaritas.ULT
{
    public class WareHouseHelper
    {
        public static WareHouse  CreateDummyWareHouse()
        {
            WareHouse wareHouse = new WareHouse();
            wareHouse.Address = "TestAddres1";
            wareHouse.Name = "WareHouseTestName";
            return wareHouse;
        }
    }

}

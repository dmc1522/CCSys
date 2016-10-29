using System.Collections.Generic;

namespace LasMargaritas.Models
{
    public class GetWareHouseResponse
    {
        public GetWareHouseResponse()
        {
            WareHouses = new List<WareHouse>();
        }
        public WareHouseError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public List<WareHouse> WareHouses { get; set; }
    }
}

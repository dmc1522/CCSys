using LasMargaritas.Models;
using System;

namespace LasMargaritas.ULT
{
    public class WeightTicketHelper
    {     
        public static WeightTicket CreateDummyTicket()
        {
            WeightTicket weightTicket = new WeightTicket();
            weightTicket.Amount = 10000;
            weightTicket.ApplyDrying = true;
            weightTicket.ApplyHumidity = true;
            weightTicket.ApplyImpurities = true;
            weightTicket.BrokenGrainDiscount = 10;
            weightTicket.CicleId = 1;
            weightTicket.CrashedGrainDiscount = 10;
            weightTicket.DamagedGrainDiscount = 10;
            weightTicket.Driver = "Driver";
            weightTicket.DryingDiscount = 10;
            weightTicket.EntranceDate = DateTime.Now;
            weightTicket.EntranceNetWeight = 1000;
            weightTicket.EntranceWeigher = "Weigher";
            weightTicket.EntranceWeightKg = 1500;
            weightTicket.ExitWeightKg = 1000;
            weightTicket.ExitDate = DateTime.Now;
            weightTicket.ExitNetWeight = 200;
            weightTicket.ExitWeigher = "Weigher";
            weightTicket.Folio = "Folio" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            weightTicket.Humidity = 10;
            weightTicket.HumidityDiscount = 10;
            weightTicket.Impurities = 10;
            weightTicket.ImpuritiesDiscount = 10;
            weightTicket.NetWeight = 400;
            weightTicket.Number = "Number" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            weightTicket.Paid = false;
            weightTicket.Plate = "Plate";
            weightTicket.Price = 10;
            weightTicket.ProducerId = 1;
            weightTicket.ProductId = 1;
            weightTicket.SmallGrainDiscount = 10;
            weightTicket.StoreTs = DateTime.Now;
            weightTicket.TotalDiscount = 50;
            weightTicket.TotalToPay = 100;
            weightTicket.UpdateTs = DateTime.Now;
            weightTicket.UserId = "35d360f3-c296-4113-ab34-9b91fe729c18";
            weightTicket.WarehouseId = 1;
            return weightTicket;
        }     
    }
}


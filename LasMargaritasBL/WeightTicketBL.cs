using LasMargaritas.DL;
using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace LasMargaritas.BL
{

    public class WeightTicketsBL
    {

        private WeightTicketsDL weightTicketsDL;

        public WeightTicketsBL(string connectionString)
        {
            weightTicketsDL = new WeightTicketsDL(connectionString);
        }

        public WeightTicket InsertWeightTicket(WeightTicket weightTicket)
        {
            //Add validations here!
            WeightTicketError result = WeightTicketError.None;
            if (weightTicket.CicleId <= 0)
                result |= WeightTicketError.InvalidCicle;
            if (weightTicket.ProductId <= 0)
                result |= WeightTicketError.InvalidProduct;
            if (weightTicket.WarehouseId <= 0)
                result |= WeightTicketError.InvalidWareHouse;
            if (string.IsNullOrEmpty(weightTicket.Folio))
                result |= WeightTicketError.InvalidFolio;

            if (result != WeightTicketError.None)
                throw new WeightTicketException(result);
            else
                return weightTicketsDL.InsertWeightTicket(weightTicket);
        }

        public List<List<ReportDataItem>> GetWeightTicketsReport(WeightTicketReportFilterModel filters)
        {
            List<List<ReportDataItem>> reportData = new List<List<ReportDataItem>>();
            DataTable data = weightTicketsDL.GetWeightTicketsReport(filters);           
            foreach(DataRow row in data.Rows)
            {
                List<ReportDataItem> rowItem = new List<ReportDataItem>();
                for(int i =0; i<row.ItemArray.Length; i++)
                {
                    ReportDataItem item = new ReportDataItem();
                    item.Name = data.Columns[i].ColumnName;
                    item.Value = row.ItemArray[i];
                    item.Type = data.Columns[i].DataType;
                    rowItem.Add(item);
                }
                reportData.Add(rowItem);
            }
            return reportData;
        }
        public WeightTicket UpdateWeightTicket(WeightTicket weightTicket)
        {
            //Add validations here!
            WeightTicketError result = WeightTicketError.None;
            if (weightTicket.CicleId <= 0)
                result |= WeightTicketError.InvalidCicle;
            if (weightTicket.ProducerId <= 0)
                result |= WeightTicketError.InvalidProducer;
            if (weightTicket.ProductId <= 0)
                result |= WeightTicketError.InvalidProduct;
            if (string.IsNullOrEmpty(weightTicket.Folio))
                result |= WeightTicketError.InvalidFolio;
            if (weightTicket.WarehouseId <= 0)
                result |= WeightTicketError.InvalidWareHouse;

            if (result != WeightTicketError.None)
                throw new WeightTicketException(result);
            else
                return weightTicketsDL.UpdateWeightTicket(weightTicket);
        }

        public List<WeightTicket> GetWeightTicket(int? id = null)
        {
            //Add validations here!
            WeightTicketError result = WeightTicketError.None;
            if (id != null && id<=0)
                result |= WeightTicketError.InvalidId;

            if (result != WeightTicketError.None)
                throw new WeightTicketException(result);
            else
                return weightTicketsDL.GetWeightTicket(id);
        }


        public List<SelectableModel> GetSelectableModels(int? cicleId)
        {
            //Add validations here!
            return weightTicketsDL.GetSelectableModels(cicleId);
        }

        public bool DeleteWeightTicket(int id)
        {
            //Add validations here!
            WeightTicketError result = WeightTicketError.None;
            if (id <= 0)
                result |= WeightTicketError.InvalidId;

            if (result != WeightTicketError.None)
                throw new WeightTicketException(result);
            else
                return weightTicketsDL.DeleteWeightTicket(id);
        }

    }
}

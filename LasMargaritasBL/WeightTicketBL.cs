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

        public ReportData GetWeightTicketsReport(WeightTicketReportFilterModel filters)
        {
            ReportData reportData = new ReportData();
            DataTable data = weightTicketsDL.GetWeightTicketsReport(filters);
            foreach (DataColumn column in data.Columns)
            {
                ReportDataColumn reportColumn = new ReportDataColumn();
                reportColumn.Type = column.DataType;
                reportColumn.Name = column.ColumnName;
                reportData.Columns.Add(reportColumn);
            }
            foreach(DataRow row in data.Rows)
            {               
                ReportDataRow reportRow = new ReportDataRow();
                reportRow.Items = new List<object>(row.ItemArray);
                reportData.Rows.Add(reportRow);
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
        public List<WeightTicket> GetWeightTicketsInSettlementFullDetails(int settlementId)
        {
            //Add validations here!
            WeightTicketError result = WeightTicketError.None;
            if (settlementId <= 0)
                result |= WeightTicketError.InvalidId;

            if (result != WeightTicketError.None)
                throw new WeightTicketException(result);
            else
                return weightTicketsDL.GetWeightTicketsInSettlementFullDetails(settlementId);
        }
        public List<WeightTicket> GetWeightTicketsAvailablesToSettleFullDetails(int cicleId, int producerId)
        {
            //Add validations here!
            WeightTicketError result = WeightTicketError.None;
            if (cicleId <= 0)
                result |= WeightTicketError.InvalidId;
            if (producerId <= 0)
                result |= WeightTicketError.InvalidId;

            if (result != WeightTicketError.None)
                throw new WeightTicketException(result);
            else
                return weightTicketsDL.GetWeightTicketsAvailablesToSettleFullDetails(cicleId, producerId);
        }
        

        public List<SelectableModel> GetSelectableModels(int? cicleId, bool? onlyPendingTickets)
        {
            //Add validations here!
            return weightTicketsDL.GetSelectableModels(cicleId, onlyPendingTickets);
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

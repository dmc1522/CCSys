using LasMargaritas.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using System.Collections.ObjectModel;
using LasMargaritas.DL;
using LasMargaritas.BL;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System.IO;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using LasMargaritas.BL.Utils;
using System.Globalization;

namespace LasMargaritas.BL
{
    public class SettlementBL
    {
        private SettlementDL settlementDL;

        private Dictionary<string, string> weightTicketsPrintColumns;
        private Dictionary<string, string> paymentsPrintColumns;
        private List<string> weightTicketColumnsToSum;
        private float[] percentageColumns;
        private static bool fontsLoaded;
        #region Constructor        
        public SettlementBL(string connectionString)
        { 
            //Probably best to move it somewhere else
            if(!fontsLoaded)
            {
                FontResolver.Apply();
                fontsLoaded = true;
            }
            settlementDL = new SettlementDL(connectionString);
            weightTicketsPrintColumns = new Dictionary<string, string>();
            paymentsPrintColumns = new Dictionary<string, string>();
            weightTicketColumnsToSum = new List<string>();

            weightTicketsPrintColumns.Add("Folio", "Folio");
            weightTicketsPrintColumns.Add("EntranceDate", "F. Entrada");
            weightTicketsPrintColumns.Add("ProductName", "Producto");
            weightTicketsPrintColumns.Add("NetWeight", "P. Neto (KG)");
            weightTicketsPrintColumns.Add("Humidity", "Humedad");
            weightTicketsPrintColumns.Add("HumidityDiscount", "Dcto. Humedad");
            weightTicketsPrintColumns.Add("Impurities", "Impurezas");
            weightTicketsPrintColumns.Add("ImpuritiesDiscount", "Dcto. Impurezas");
            weightTicketsPrintColumns.Add("TotalWeightToPay", "Peso a pagar (KG)");
            weightTicketsPrintColumns.Add("Price", "Precio");
            weightTicketsPrintColumns.Add("DryingDiscount", "Dcto. Secado");
            weightTicketsPrintColumns.Add("TotalToPay", "Total a pagar");

            weightTicketColumnsToSum.Add("NetWeight");
            weightTicketColumnsToSum.Add("TotalWeightToPay");
            weightTicketColumnsToSum.Add("TotalToPay");

            paymentsPrintColumns.Add("Date", "Fecha");
            paymentsPrintColumns.Add("TypeString", "Tipo");
            paymentsPrintColumns.Add("Description", "Description");
            paymentsPrintColumns.Add("CheckOrVoucher", "Cheque/Vale");
            paymentsPrintColumns.Add("Total", "Total");
            percentageColumns = new float[weightTicketsPrintColumns.Count];
            float widthForNumbers = 0.08F;

            percentageColumns[0] = 0.07F;//Folio
            percentageColumns[1] = 0.07F;//Entrance date
            percentageColumns[2] = 0.18F;//Product
            percentageColumns[3] = widthForNumbers;//Net Weight
            percentageColumns[4] = 0.06F;//Humidity
            percentageColumns[5] = widthForNumbers;//Humidity discount
            percentageColumns[6] = 0.06F;//Impurities
            percentageColumns[7] = widthForNumbers;//Impurities discount
            percentageColumns[8] = widthForNumbers;//Total Weight to pay
            percentageColumns[9] = widthForNumbers;//Price
            percentageColumns[10] = widthForNumbers;//Drying discount
            percentageColumns[11] = widthForNumbers;//Total To Pay



        }

        #endregion

        private void DefineStyles(Document document)
        {

            // Get the predefined style Normal.
            Style style = document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Times New Roman";

            // Heading1 to Heading9 are predefined styles with an outline level. An outline level
            // other than OutlineLevel.BodyText automatically creates the outline (or bookmarks) 
            // in PDF.

            style = document.Styles["Heading1"];
            style.Font.Name = "Tahoma";
            style.Font.Size = 12;
            style.Font.Bold = true;
            style.Font.Color = Colors.DarkBlue;
            style.ParagraphFormat.PageBreakBefore = true;
            style.ParagraphFormat.SpaceAfter = 4;

            style = document.Styles["Heading2"];
            style.Font.Size = 10;
            style.Font.Bold = true;
            style.ParagraphFormat.PageBreakBefore = false;
            style.ParagraphFormat.SpaceBefore = 5;
            style.ParagraphFormat.SpaceAfter = 5;

            style = document.Styles["Heading3"];
            style.Font.Size = 8;
            style.Font.Bold = true;
            //style.Font.Italic = true;
            style.ParagraphFormat.SpaceBefore = 6;
            style.ParagraphFormat.SpaceAfter = 3;

            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called TextBox based on style Normal
            style = document.Styles.AddStyle("TextBox", "Normal");
            style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
            style.ParagraphFormat.Borders.Width = 2.5;
            style.ParagraphFormat.Borders.Distance = "3pt";
            //TODO: Colors
            style.ParagraphFormat.Shading.Color = Colors.SkyBlue;

            // Create a new style called TOC based on style Normal
            style = document.Styles.AddStyle("TOC", "Normal");
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right, TabLeader.Dots);
            style.ParagraphFormat.Font.Color = Colors.Blue;

            style = document.Styles.AddStyle("RightAlignedTitleHeading2","Heading2");
            style.Font.Size = 12;
            style.Font.Bold = true;
            style.ParagraphFormat.PageBreakBefore = false;
            style.ParagraphFormat.Alignment = ParagraphAlignment.Right;
            style.ParagraphFormat.SpaceAfter = -12;
        }

        private void AddLogoDateProducerNameAddressAndCicle(Document document, DateTime date, Producer producer, string cicle)
        {
            string producerName = producer.Name + " " + producer.PaternalSurname + " " + producer.MaternalSurname;
            SelectableModel state= new CatalogBL(settlementDL.ConnectionString).GetStates().Where(x => x.Id == producer.StateId).FirstOrDefault();         
            string producerAddress = producer.Address + ". C.P: " + producer.ZipCode + ". Colonia " + producer.DistrinctOrColony + ". " + producer.City + ", " + state.Name;
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            string dateString = textInfo.ToUpper(date.ToString("D", CultureInfo.CreateSpecificCulture("es-MX")));
            var assembly = Assembly.GetExecutingAssembly();
            Paragraph currentParagraph = document.LastSection.AddParagraph();
            using (Stream stream = assembly.GetManifestResourceStream("LasMargaritas.BL.Images.LogoMargaritasMedium.jpg"))
            {
                if (stream == null)
                    throw new ArgumentException("No resource with name");
                int count = (int)stream.Length;
                byte[] data = new byte[count];
                stream.Read(data, 0, count);
                currentParagraph.Format.Alignment = ParagraphAlignment.Right;
                currentParagraph.AddImage("base64:"+Convert.ToBase64String(data));                
            }
            currentParagraph = document.LastSection.AddParagraph();
            currentParagraph.AddFormattedText("Ciclo: ", "Heading2");
            currentParagraph.AddText(cicle);
            currentParagraph = document.LastSection.AddParagraph();
            currentParagraph.AddFormattedText("Fecha: ", "Heading2");
            currentParagraph.AddText(dateString);
            currentParagraph = document.LastSection.AddParagraph();
            currentParagraph.AddFormattedText("Productor: ", "Heading2");
            currentParagraph.AddText(producerName);
            currentParagraph = document.LastSection.AddParagraph();
            currentParagraph.AddFormattedText("Domicilio: ", "Heading2");
            currentParagraph.AddText(producerAddress);


        }
        private Document CreateDocument()
        {
            Document document = new Document();
            document.Info.Title = "Liquidación";
            document.Info.Subject = "Liquidación";
            document.Info.Author = "BCS - Las Margaritas";
            document.AddSection();
            PageSetup pageSetup = document.DefaultPageSetup.Clone();
            // set orientation
            pageSetup.Orientation = Orientation.Landscape;
            document.LastSection.PageSetup = pageSetup;
            DefineStyles(document);
            return document;
        }
     
        private void AddWeightTicketsTable(List<WeightTicket> weightTickets, Document document)
        {
            PdfPrinterHelper<WeightTicket> helper = new PdfPrinterHelper<WeightTicket>();
            Paragraph currentParagraph = document.LastSection.AddParagraph();
            currentParagraph = document.LastSection.AddParagraph();
            currentParagraph.AddFormattedText("Boletas en liquidación", "Heading2");
            if (weightTickets.Count > 0)
            {
                Table table = helper.GetTableFromList(weightTickets, weightTicketsPrintColumns, percentageColumns,true, weightTicketColumnsToSum);
                table.Rows.Alignment = RowAlignment.Left;
                document.LastSection.Add(table);
            }
            else
            {
                currentParagraph = document.LastSection.AddParagraph();
                currentParagraph.AddText("No hay boletas");
            }
        }

        private Table CreateSummaryTable(List<SettlementPayment> payments, Settlement settlement)
        {
            //Create summary table
            Table summaryTable = new Table();
            summaryTable.Borders.Width = 0.75;
            double portraitPaperSizeInCm = 25; //aprox considering margines
            summaryTable.AddColumn(Unit.FromCentimeter(portraitPaperSizeInCm / (double)2));
            summaryTable.AddColumn(Unit.FromCentimeter(portraitPaperSizeInCm / (double)2));
            //WeightTicketsTotal 
            Row row = summaryTable.AddRow();
            Cell cell = row.Cells[0];
            Paragraph paragraph = cell.AddParagraph();
            paragraph.AddFormattedText("+Total en boletas", "Heading3");
            cell = row.Cells[1];
            paragraph = cell.AddParagraph();
            paragraph.AddFormattedText(settlement.WeightTicketsTotal.ToString("C2"), TextFormat.Bold);
            //CashAdvancestotal
            row = summaryTable.AddRow();
            cell = row.Cells[0];
            paragraph = cell.AddParagraph();
            paragraph.AddFormattedText("-Total en anticipos", "Heading3");
            cell = row.Cells[1];
            paragraph = cell.AddParagraph();
            paragraph.AddFormattedText(settlement.CashAdvancesTotal.ToString("C2"), TextFormat.Bold);
            //CreditsTotal
            row = summaryTable.AddRow();
            cell = row.Cells[0];
            paragraph = cell.AddParagraph();
            paragraph.AddFormattedText("-Total en creditos", "Heading3");
            cell = row.Cells[1];
            paragraph = cell.AddParagraph();
            paragraph.AddFormattedText(settlement.CreditsTotal.ToString("C2"), TextFormat.Bold);
            //Total
            row = summaryTable.AddRow();
            cell = row.Cells[0];
            paragraph = cell.AddParagraph();
            paragraph.AddFormattedText("=Total a pagar", "Heading3");
            cell = row.Cells[1];
            paragraph = cell.AddParagraph();
            paragraph.AddFormattedText(settlement.Total.ToString("C2"), TextFormat.Bold);
            //PaymentsTotal
            row = summaryTable.AddRow();
            cell = row.Cells[0];
            paragraph = cell.AddParagraph();
            paragraph.AddFormattedText("-Pagos", "Heading3");
            cell = row.Cells[1];
            paragraph = cell.AddParagraph();
            paragraph.AddFormattedText((payments.Sum(x => x.Total)).ToString("C2"), TextFormat.Bold);
            //PaymentsTotal
            row = summaryTable.AddRow();
            cell = row.Cells[0];
            paragraph = cell.AddParagraph();
            paragraph.AddFormattedText("=Balance", "Heading3");
            cell = row.Cells[1];
            paragraph = cell.AddParagraph();
            paragraph.AddFormattedText(((settlement.Total) - (payments.Sum(x => x.Total))).ToString("C2"), TextFormat.Bold);
            return summaryTable;
        }
        private void AddPaymentsTableAndSummaryTable(List<SettlementPayment> payments, Document document, Settlement settlement)
        {
            PdfPrinterHelper<SettlementPayment> helper = new PdfPrinterHelper<SettlementPayment>();
           
            Paragraph currentParagraph = document.LastSection.AddParagraph();
            currentParagraph = document.LastSection.AddParagraph();
            currentParagraph.AddFormattedText("Pagos en liquidación:", "Heading2");
            currentParagraph = document.LastSection.AddParagraph();
            if (payments.Count > 0)
            {
                Table paymentsTable = helper.GetTableFromList(payments, paymentsPrintColumns);
                paymentsTable.Rows.Alignment = RowAlignment.Left;
                document.LastSection.Add(paymentsTable);
            }
            else
            {
                currentParagraph = document.LastSection.AddParagraph();
                currentParagraph.AddText("No hay pagos");
            }
            document.LastSection.AddParagraph();
            currentParagraph = document.LastSection.AddParagraph();
            currentParagraph.AddFormattedText("Resumen:", "Heading2");
            document.LastSection.AddParagraph();
            Table summaryTable = CreateSummaryTable(payments, settlement);
            summaryTable.Rows.Alignment = RowAlignment.Left;
            document.LastSection.Add(summaryTable);
            /*  //We need a master table with two columns borderless and two cells
              Table masterTable = new Table();
              masterTable.Borders.Width = 0;
              masterTable.Columns.AddColumn();
              masterTable.Columns.AddColumn();
              Row firstTableRow = masterTable.AddRow();
              firstTableRow.Cells[0].Elements.Add(paymentsTable);
              firstTableRow.Cells[1].Elements.Add(summaryTable);*/


        }
        public MemoryStream PrintSettlement(int settlementId)
        {
            Document document = CreateDocument();
            Settlement settlement = settlementDL.GetSettlement(settlementId, null)[0]; //TODO check for missing settlement
            Cicle cicle = new CicleBL(settlementDL.ConnectionString).GetCicle(settlement.CicleId)[0]; //TODO check for missing cicle
            Producer producer = new ProducerBL(settlementDL.ConnectionString).GetProducer(settlement.ProducerId)[0]; //TODO check for missing cicle
            List<WeightTicket> tickets = new WeightTicketsDL(settlementDL.ConnectionString).GetWeightTicketsInSettlementFullDetails(settlementId);
            List<SettlementPayment> payments = settlementDL.GetSettlementPayments(settlementId);
            //Add logo, date,  producer data and cicle            
            AddLogoDateProducerNameAddressAndCicle(document, settlement.Date,  producer, cicle.Name);
            document.LastSection.AddParagraph();
            //Print weightickets table
            AddWeightTicketsTable(tickets, document);
            document.LastSection.AddParagraph();
            //Print payments
            AddPaymentsTableAndSummaryTable(payments, document, settlement);
            document.LastSection.AddParagraph();
            //Print producer signature section
            Paragraph currentParagraph = document.LastSection.AddParagraph();
            currentParagraph.Format.Alignment = ParagraphAlignment.Center;
            currentParagraph.AddText("______________________________________"); 
            currentParagraph = document.LastSection.AddParagraph();
            currentParagraph.Format.Alignment = ParagraphAlignment.Center;
            currentParagraph.AddText(producer.Name + " " + producer.PaternalSurname + " " + producer.MaternalSurname);
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
            renderer.Document = document;
            renderer.RenderDocument();
            MemoryStream stream = new MemoryStream();
            renderer.Save(stream, false);
            return stream;

        }
    
        public List<SettlementPayment> GetSettlementPayments(int settlementId)
        {
            return settlementDL.GetSettlementPayments(settlementId);
        }

      
        public bool DeleteAllSettlementPayments(int settlementId)
        {
            return settlementDL.DeleteAllSettlementPayments(settlementId);
        }

        public bool DeleteSettlementPayment(int paymentId)
        {
            return settlementDL.DeleteSettlementPayment(paymentId);
        }


        public Settlement InsertSettlement(Settlement settlement)
        {
            //Save settlement
            Settlement settlementSaved = settlementDL.InsertSettlement(settlement);
            settlementSaved.WeightTicketsIds = settlement.WeightTicketsIds;
            //Add WeightTickets
            if (settlementSaved.WeightTicketsIds != null && settlementSaved.WeightTicketsIds.Count > 0)
            {
                foreach (int weightTicket in settlementSaved.WeightTicketsIds)
                {
                    settlementDL.AddWeightTicketToSettlement(settlementSaved.Id, weightTicket);
                }
            }
            return settlementSaved;
        }
        public SettlementPayment AddSettlementPayment(SettlementPayment payment)
        {
            return settlementDL.AddSettlementPayment(payment);
        }
        public Settlement UpdateSettlement(Settlement settlement)
        {
            //First delete all weight Tickets
            settlementDL.DeleteAllWeightTicketsFromSettlement(settlement.Id);
            //Save settlement
            Settlement settlementSaved = settlementDL.UpdateSettlement(settlement);
            settlementSaved.WeightTicketsIds = settlement.WeightTicketsIds;
            //Add WeightTickets
            if (settlementSaved.WeightTicketsIds != null && settlementSaved.WeightTicketsIds.Count > 0)
            {
                WeightTicketsBL weightTicketDL = new WeightTicketsBL(settlementDL.ConnectionString);
                foreach (int weightTicket in settlementSaved.WeightTicketsIds)
                {
                    settlementDL.AddWeightTicketToSettlement(settlementSaved.Id, weightTicket);
                    //Update paid
                    WeightTicket ticket = weightTicketDL.GetWeightTicket(weightTicket).ElementAt(0);
                    ticket.Paid = settlement.Paid;
                    weightTicketDL.UpdateWeightTicket(ticket);
                }
            }               
            return settlementSaved;
        }


        public List<Settlement> GetSettlement(int? id = null, int? cicleId = null)
        {
            return settlementDL.GetSettlement(id, cicleId);
        }

        public bool DeleteSettlement(int id)
        {
            return settlementDL.DeleteSettlement(id);
        }
        public Collection<int> GetWeightTicketsInSettlement(int id)
        {
            return settlementDL.GetWeightTicketsInSettlement(id);
        }
     
    }
}


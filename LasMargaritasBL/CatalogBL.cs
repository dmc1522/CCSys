using LasMargaritas.DL;
using LasMargaritas.Models;
using System.Collections.Generic;


namespace LasMargaritas.BL
{
    public class CatalogBL
    {
        private CatalogDL catalogDL;

        public CatalogBL(string connectionString)
        {
            catalogDL = new CatalogDL(connectionString);
        }
        public List<SelectableModel> GetStates()
        {
            return catalogDL.GetStates();
        }
        public List<SelectableModel> GetAgriculturalBrands()
        {
            return catalogDL.GetAgriculturalBrands();
        }
        public List<SelectableModel> GetCivilStatus()
        {
            return catalogDL.GetCivilStatus();
        }

        public List<SelectableModel> GetGenders()
        {
            return catalogDL.GetGenders();
        }

        public List<SelectableModel> GetPresentations()
        {
            return catalogDL.GetPresentations();
        }

        public List<SelectableModel> GetProductGroups()
        {
            return catalogDL.GetProductGroups();
        }

        public List<SelectableModel> GetRegimes()
        {
            return catalogDL.GetRegimes();
        }

        public List<SelectableModel> GetUnits()
        {
            return catalogDL.GetUnits();
        }

    }
}

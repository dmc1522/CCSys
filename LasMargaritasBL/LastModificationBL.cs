using LasMargaritas.DL;
using LasMargaritas.Models;
using System;
using System.Collections.Generic;


namespace LasMargaritas.BL
{
    public class LastModificationBL
    {
        private LastModificationDL lastModificationDL;

        public LastModificationBL(string connectionString)
        {
            lastModificationDL = new LastModificationDL(connectionString);
        }

        public LastModification GetLastModification(Module module)
        {           
            return lastModificationDL.GetLastModification(module);
        }

        public bool SetLastModification(LastModification lastModification)
        {            
            return lastModificationDL.SetLastModification(lastModification);
        }
    }
}

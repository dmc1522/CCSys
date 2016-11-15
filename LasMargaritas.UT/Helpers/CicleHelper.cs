using LasMargaritas.Models;
using System;


namespace LasMargaritas.ULT
{
    public class CicleHelper
    {
        public static Cicle CreateDummyCicle()
        {
            Cicle cicle = new Cicle();
            cicle.AmountPerHectarea = 5;
            cicle.Closed = false;
            cicle.EndDateZone1 = DateTime.Now;
            cicle.EndDateZone2 = DateTime.Now;
            cicle.Name = "TestCicle";
            cicle.StartDate = DateTime.Now;
            return cicle;
        }
    }

}

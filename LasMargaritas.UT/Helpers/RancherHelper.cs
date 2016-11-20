using LasMargaritas.Models;
using System;


namespace LasMargaritas.ULT
{
    public class RancherHelper
    {
        public static Rancher  CreateDummyRancher()
        {
            Rancher rancher = new Rancher();
            rancher.Address = "TestAddres1";
            rancher.Name = "RancherTestName";
            return rancher;
        }
    }

}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.Models
{
    public class WareHouseResponse
    {
        public WareHouseError ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public WareHouse WareHouse { get; set; }
    }
}

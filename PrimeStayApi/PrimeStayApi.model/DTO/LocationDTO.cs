﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStayApi.Model.DTO
{
    public class LocationDTO : BaseModelDTO
    {
        public string Street_Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip_code { get; set; }
    }
}
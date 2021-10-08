﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStayApi.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Num_of_avaliable { get; set; }
        public int Num_of_beds { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int HotelId { get; set; }
    }
}
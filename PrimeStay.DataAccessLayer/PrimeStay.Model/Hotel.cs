﻿namespace PrimeStay.Model
{
    public class Hotel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Staffed_hours { get; set; }
        public int? Stars { get; set; }
        public int? Location_Id { get; set; }

    }

}
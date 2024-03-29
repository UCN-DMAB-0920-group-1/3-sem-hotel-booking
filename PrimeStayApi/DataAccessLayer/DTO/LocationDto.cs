﻿namespace DataAccessLayer.DTO
{
    public class LocationDto : BaseModelDto
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}

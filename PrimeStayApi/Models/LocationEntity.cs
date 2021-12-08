using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LocationEntity : BaseEntity
    {
        public string Street_Address { get; set; }
        public string Zip_code { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}

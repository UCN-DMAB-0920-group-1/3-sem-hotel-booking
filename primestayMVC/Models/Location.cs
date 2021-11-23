using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Location : BaseModel
    {
        public string Street_Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip_code { get; set; }

    }
}

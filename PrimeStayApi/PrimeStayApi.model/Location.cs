using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStayApi.Model
{
    public class Location
    {
        public int Id { get; set; }
        public string Street_Address { get; set; }
        public string Zip_code { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

    }
}

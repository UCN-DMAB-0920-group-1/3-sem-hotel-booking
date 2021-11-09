using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStayApi.Model
{
    public class PictureEntity:BaseEntity
    {
        public int? Hotel_id { get; set; }
        public int? Room_id { get; set; }
        public string Type { get; set; } // TODO Brug ENUM i stedet?
        public string Path { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }


    }
}

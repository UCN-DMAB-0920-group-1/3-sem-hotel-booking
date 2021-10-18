using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace primestayMVC.Model
{
    public class Hotel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Staffed_hours { get; set; }
        public int Stars { get; set; }
        public string LocationHref { get; set; }
        public Location Location { get; set; }
    }
}

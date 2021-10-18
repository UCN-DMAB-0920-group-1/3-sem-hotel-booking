using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace primestayMVC.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Staffed_hours { get; set; }
        public int Stars { get; set; }
    }
}

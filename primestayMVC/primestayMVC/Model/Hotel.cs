using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeStay.MVC.Model
{
    public class Hotel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Staffed_hours { get; set; }
        public int? Stars { get; set; }
        public int? Location_Id { get; set; }
        public Location Location { get; set; }
        public IEnumerable<Room> rooms { get; set; }

        public bool Matches(string input)
        {
            if (input is null) return false;
            //things to match search with should be in this list
            var matches = new List<string> { Name, Location.City, Location.Country };
            return matches.Select(x => x.Contains(input, StringComparison.CurrentCultureIgnoreCase)).Contains(true);

        }


    }
}

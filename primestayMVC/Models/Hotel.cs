using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class Hotel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string StaffedHours { get; set; }
        public int? Stars { get; set; }
        public int? LocationId { get; set; }
        public bool? Active { get; set; }
        public Location Location { get; set; }
        public IEnumerable<RoomType> rooms { get; set; }

        public bool Matches(string input)
        {
            if (input is null) return false;
            //things to match search with should be in this list
            var matches = new List<string> { Name, Location.City, Location.Country };
            return matches.Select(x => x.Contains(input, StringComparison.CurrentCultureIgnoreCase)).Contains(true);

        }


    }
}

namespace primestayMVC.Model
{
    public class Hotel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Staffed_hours { get; set; }
        public int? Stars { get; set; }
        public string LocationHref { get; set; }

        public Hotel(string name, string description, string staffed_hours, int? stars, string locationHref) : base(locationHref)
        {
            Name = name;
            Description = description;
            Staffed_hours = staffed_hours;
            Stars = stars;
            LocationHref = locationHref;
           

        }
        public Hotel() : base()
        {

        }
    }
}
    

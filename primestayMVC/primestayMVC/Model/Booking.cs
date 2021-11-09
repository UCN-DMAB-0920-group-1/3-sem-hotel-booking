using primestay.MVC.Model;
using System;

namespace PrimeStay.MVC.Model
{
    public class Booking : BaseModel
    {
        public DateTime? Start_date { get; set; }
        public DateTime? End_date { get; set; }
        public int? Num_of_guests { get; set; }
        public string Room_Type_href { get; set; }
        public string Customer_href { get; set; }
        public Customer Customer { get; set; }
    }
}

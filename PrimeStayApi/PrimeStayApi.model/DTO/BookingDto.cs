using System;


namespace PrimeStayApi.Model.DTO
{
   public class BookingDto:BaseModelDto
    {
        public DateTime? Start_date { get; set; }
        public DateTime? End_date { get; set; }
        public int? Num_of_guests { get; set; }
        public string Room_href { get; set; }
        public string Customer_href { get; set; }
    }
}

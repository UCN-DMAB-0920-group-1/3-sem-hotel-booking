namespace PrimeStayApi.Model.DTO
{
    public class HotelDto : BaseModelDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string StaffedHours { get; set; }
        public int? Stars { get; set; }
        public string LocationHref { get; set; }
        public bool? Active { get; set; }

    }
}

using System;


namespace Models
{
    public class PriceEntity : BaseEntity
    {
        public DateTime? Start_Date { get; set; }
        public int? Value { get; set; }
        public int? Room_Type_Id { get; set; }
    }
}

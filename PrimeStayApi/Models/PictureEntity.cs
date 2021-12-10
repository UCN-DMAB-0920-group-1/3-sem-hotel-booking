namespace Models
{
    public class PictureEntity : BaseEntity
    {
        public int? Hotel_id { get; set; }
        public int? Room_type_id { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }


    }
}

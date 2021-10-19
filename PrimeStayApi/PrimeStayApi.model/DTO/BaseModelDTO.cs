namespace PrimeStayApi.Model.DTO
{
    public class BaseModelDTO
    {
        public string Href {  get; set; }
        public int Id { get; }

        public BaseModelDTO()
        {
        }

        public BaseModelDTO(int id)
        {
            Id = id;
        }
    }
}
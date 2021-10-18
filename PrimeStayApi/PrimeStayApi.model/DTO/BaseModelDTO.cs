namespace PrimeStayApi.Model.DTO
{
    public class BaseModelDTO
    {
        public string Href {  get; set; }

        public BaseModelDTO(int id) => this.Href = $"api/Hotel?Id={id}";

        public BaseModelDTO()
        {
        }
    }
}
namespace PrimeStayApi.Model.DTO
{
    internal class BaseModelDTO
    {
        public string Href {  get; set; }

        public BaseModelDTO(string href) => this.Href = href; 
    }
}
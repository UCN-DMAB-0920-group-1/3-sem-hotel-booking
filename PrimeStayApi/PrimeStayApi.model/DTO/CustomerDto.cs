using System;

namespace PrimeStayApi.Model.DTO
{
    public class CustomerDto : BaseModelDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
    }
}
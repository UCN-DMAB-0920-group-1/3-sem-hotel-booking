using System;

namespace PrimeStayApi.Model
{
    public class CustomerEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
    }
}

using System;

namespace Models
{
    public class Customer : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }

    }
}

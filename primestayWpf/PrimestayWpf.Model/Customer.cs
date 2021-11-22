using System;

namespace PrimestayWpf.Model
{
    public class Customer : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDay { get; set; }
    }
}

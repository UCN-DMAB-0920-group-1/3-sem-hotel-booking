﻿using System;

namespace Models
{
    public class CustomerEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public int? User_id { get; set; }
    }
}

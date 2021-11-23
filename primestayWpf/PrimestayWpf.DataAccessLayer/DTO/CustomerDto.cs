﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PrimeStay.WPF.DataAccessLayer.DTO
{
    public class CustomerDto : BaseDto
    {
        public string Name{ get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public DateTime BirthDay { get; set; }
    }
}
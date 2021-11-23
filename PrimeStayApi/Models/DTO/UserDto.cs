﻿using System;


namespace Models.DTO
{
    public class UserDto : BaseModelDto
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string Salt { get; set; }

    }
}

using System;


namespace DataAccessLayer.DTO
{
    public class UserDto : BaseDto
    {
        public string name { get; set; }
        public string Token { get; set; }

        public DateTime Expires { get; set; }
    }
}

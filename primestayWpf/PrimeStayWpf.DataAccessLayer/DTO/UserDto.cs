using System;


namespace PrimeStay.WPF.DataAccessLayer.DTO
{
    public class UserDto : BaseDto
    {
        public string name{ get; set; }
        public string accessToken { get; set; }
    }
}

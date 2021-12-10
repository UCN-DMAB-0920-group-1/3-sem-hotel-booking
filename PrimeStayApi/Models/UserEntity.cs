namespace Models
{
    public class UserEntity : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Salt { get; set; }
    }
}

using System.Collections.Generic;

namespace B.Framework.Domain.Shared.User
{
    public class UserBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
    }
}
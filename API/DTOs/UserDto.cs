namespace API.DTOs
{
    // needed for transfering the token to Angular 
    public class UserDto
    {
        public UserDto(int id, string name, string email, string token)
        {
            Id = id;
            Name = name;
            Email = email;
            Token = token;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
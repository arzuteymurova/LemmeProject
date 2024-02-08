namespace LemmeProject.Application.DTOs.Users
{
    public class LoginedUserResponse
    {
        public string UserFullName { get; set; }
        public string UserName { get; set; }
        public IList<string> UserRoles { get; set; }
        public string SecurityToken { get; set; }
    }
}
namespace LemmeProject.Application.DTOs.Users
{
    public class UserChangePasswordRequest
    {
        public int Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

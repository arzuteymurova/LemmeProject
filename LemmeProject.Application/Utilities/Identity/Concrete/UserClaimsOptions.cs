using LemmeProject.Application.Utilities.Identity.Abstract;

namespace LemmeProject.Application.Utilities.Identity.Concrete
{
    public class UserClaimsOptions : IUserClaimsOptions
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}

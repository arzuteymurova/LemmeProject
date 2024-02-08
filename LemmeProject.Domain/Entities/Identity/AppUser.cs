using LemmeProject.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace LemmeProject.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public EntityStatus EntityStatus { get; set; } = EntityStatus.Active;
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}

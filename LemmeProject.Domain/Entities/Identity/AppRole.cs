using LemmeProject.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace LemmeProject.Domain.Entities.Identity
{
    public class AppRole : IdentityRole<int>
    {
        public EntityStatus EntityStatus { get; set; } = EntityStatus.Active;
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemmeProject.Application.DTOs.Users
{
    public class UserResetPasswordRequest 
    {
        public int Id { get; set; }
        public string NewPassword { get; set; }
    }
}

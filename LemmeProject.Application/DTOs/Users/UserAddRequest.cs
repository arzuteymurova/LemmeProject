﻿using LemmeProject.Application.DTOs.Roles;

namespace LemmeProject.Application.DTOs.Users
{
    public class UserAddRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }       
        public IList<RoleAddRequest> Roles { get; set; }
    }
}

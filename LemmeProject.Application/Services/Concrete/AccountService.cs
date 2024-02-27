using AutoMapper;
using LemmeProject.Application.DTOs.Roles;
using LemmeProject.Application.DTOs.Users;
using LemmeProject.Application.Services.Abstract;
using LemmeProject.Application.Utilities.Identity.Abstract;
using LemmeProject.Application.Utilities.Identity.Concrete;
using LemmeProject.Domain.Entities.Identity;
using LemmeProject.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;
using LemmeProject.Application.Utilities.Results.Abstract;
using LemmeProject.Application.Utilities.Results.Concrete;
using HotelAPI.Application.Utilities.Constants;


namespace HotelAPI.Application.Services.Concrete;

public class AccountService : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly JWTOptions _jwtSettings;
    private readonly IJWTTokenService _jwtTokenService;
    public AccountService(UserManager<AppUser> userManager, IMapper mapper, RoleManager<AppRole> roleManager,IOptionsSnapshot<JWTOptions> jwtSettings,
        IJWTTokenService jwtTokenService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _jwtSettings = jwtSettings.Value;
        _jwtTokenService = jwtTokenService;
    }


    public async Task<IDataResult<IdentityResult>> RegisterUserAsync(UserAddRequest userAddRequest)
    {
        AppUser user = _mapper.Map<AppUser>(userAddRequest);
        IdentityResult result = await _userManager.CreateAsync(user, userAddRequest.Password);

        if (result.Succeeded)
        {
            foreach (var roleName in userAddRequest.Roles)
            {
                var role = _mapper.Map<AppRole>(roleName);
                await _userManager.AddToRoleAsync(user, role.ToString());
            }

        }
        return new SuccessDataResult<IdentityResult>(result,Messages.UserRegistered);
    }
    public async Task<IDataResult<LoginedUserResponse>> Login(LoginRequest loginRequest)
    {

        AppUser user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginRequest.UserName && x.EntityStatus == EntityStatus.Active);
        if (user == null)
        {
            return new ErrorDataResult<LoginedUserResponse>(Messages.UserNotFound);
        }

        bool checkPassword = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
        if (!checkPassword)
        {
            return new ErrorDataResult<LoginedUserResponse>(Messages.PasswordError);
        }

        IList<string> roles = await _userManager.GetRolesAsync(user);

        UserClaimsOptions userModelForTokenGen = new UserClaimsOptions() { Id = user.Id, UserName = user.UserName };
        string jwt = _jwtTokenService.GenerateJwt(userModelForTokenGen, roles, _jwtSettings);

        LoginedUserResponse loginedUserResponse = new LoginedUserResponse()
        {
            UserFullName = $"{user.FirstName} {user.LastName}",
            UserName = user.UserName,
            UserRoles = roles,
            SecurityToken = jwt

        };

        return new SuccessDataResult<LoginedUserResponse>(loginedUserResponse);        
    }
    public List<UserTableResponse> GetAllUsers()
    {
        var query = from user in _userManager.Users.ToList()

                    select new UserTableResponse
                    {
                        Id = user.Id,
                        FullName = $"{user.FirstName} {user.LastName}",
                        Email = user.Email,
                        UserName = user.UserName,
                        Roles = _userManager.GetRolesAsync(user).Result.ToList()
                    };

        return query.ToList();
    }
    public async Task<UserToUpdateResponse> GetUserForUpdateById(int id)
    {
        AppUser user = await _userManager.FindByIdAsync(id.ToString());

        UserToUpdateResponse userToUpdateResponse = _mapper.Map<UserToUpdateResponse>(user);
        userToUpdateResponse.Roles = _userManager.GetRolesAsync(user).Result;

        return userToUpdateResponse;
    }
    public async Task<IdentityResult> EditUserAsync(UserUpdateRequest userUpdateRequest)
    {
        AppUser user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userUpdateRequest.Id && u.EntityStatus == EntityStatus.Active);
        user.FirstName = userUpdateRequest.FirstName;
        user.LastName = userUpdateRequest.LastName;
        user.Email = userUpdateRequest.Email;
        user.UserName = userUpdateRequest.UserName;
        IdentityResult result = await _userManager.UpdateAsync(user);

        return result;
    }
    public async Task<IdentityResult> DeActivateUser(int id)
    {
        AppUser user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == id && u.EntityStatus == EntityStatus.Active);
        user.EntityStatus = EntityStatus.InActive;
        IdentityResult result = await _userManager.UpdateAsync(user);

        return result;
    }
    public async Task<IdentityResult> ChangePasswordAsync(UserChangePasswordRequest userChangePasswordRequest)
    {
        AppUser user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userChangePasswordRequest.Id && u.EntityStatus == EntityStatus.Active);

        IdentityResult result = await _userManager.ChangePasswordAsync(user, userChangePasswordRequest.OldPassword, userChangePasswordRequest.NewPassword);
        return result;

    }
    public async Task<IdentityResult> ResetPasswordAsync(UserResetPasswordRequest userResetPasswordRequest)
    {
        AppUser user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userResetPasswordRequest.Id && u.EntityStatus == EntityStatus.Active);
        string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, userResetPasswordRequest.NewPassword);
        return result;

    }




    public async Task<IdentityResult> CreateRoleAsync(RoleAddRequest roleAddRequest)
    {
        AppRole role = _mapper.Map<AppRole>(roleAddRequest);
        IdentityResult result = await _roleManager.CreateAsync(role);
        return result;
    }
    public List<RoleTableResponse> GetAllRoles()
    {
        var query = from role in _roleManager.Roles.Where(x => x.EntityStatus == EntityStatus.Active)
                    select new RoleTableResponse
                    {
                        Id = role.Id,
                        Name = role.Name,
                    };

        return query.ToList();
    }
    public async Task<IdentityResult> DeActivateRole(int id)
    {
        AppRole role = await _roleManager.Roles.SingleOrDefaultAsync(r => r.Id == id && r.EntityStatus == EntityStatus.Active);
        role.EntityStatus = EntityStatus.InActive;
        IdentityResult result = await _roleManager.UpdateAsync(role);
        return result;
    }   
    public async Task<IdentityResult> AddUserToRoleAsync(int userId, int roleId)
    {
        IdentityResult result;
        AppUser user = _userManager.Users.SingleOrDefault(u => u.Id == userId && u.EntityStatus == EntityStatus.Active);
        AppRole role = _roleManager.Roles.SingleOrDefault(r => r.Id == roleId && r.EntityStatus == EntityStatus.Active);
        result = await _userManager.AddToRoleAsync(user, role.Name);
        return result;
    }
    public async Task<IdentityResult> AddUserToRolesAsync(int userId, List<int> roleIds)
    {
        IdentityResult result;
        AppUser AppUser = _userManager.Users.SingleOrDefault(u => u.Id == userId && u.EntityStatus == EntityStatus.Active);
        IList<string> userRoles = await _userManager.GetRolesAsync(AppUser);
        result = await _userManager.RemoveFromRolesAsync(AppUser, userRoles);
        List<string> rolesByIds = _roleManager.Roles.Where(x => roleIds.Contains(x.Id)).Select(n => n.Name).ToList();
        result = await _userManager.AddToRolesAsync(AppUser, rolesByIds);
        return result;


    }
    public async Task<IdentityResult> RemoveUserFromRoleAsync(int userId, int roleId)
    {
        IdentityResult result;
        AppUser user = _userManager.Users.SingleOrDefault(u => u.Id == userId && u.EntityStatus == EntityStatus.Active);
        AppRole role = _roleManager.Roles.SingleOrDefault(r => r.Id == roleId && r.EntityStatus == EntityStatus.Active);
        result = await _userManager.RemoveFromRoleAsync(user, role.Name);
        return result;
    }
    public async Task<IdentityResult> RemoveUserFromRolesAsync(int userId, List<int> roleIds)
    {
        IdentityResult result;
        AppUser user = _userManager.Users.SingleOrDefault(u => u.Id == userId && u.EntityStatus == EntityStatus.Active);
        IList<string> userRoles = await _userManager.GetRolesAsync(user);
        List<string> rolesByIds = _roleManager.Roles.Where(x => roleIds.Contains(x.Id)).Select(n => n.Name).ToList();
        result = await _userManager.RemoveFromRolesAsync(user, rolesByIds);
        return result;
    }



}
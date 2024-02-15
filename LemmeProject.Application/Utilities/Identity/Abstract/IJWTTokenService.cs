namespace LemmeProject.Application.Utilities.Identity.Abstract
{
    public interface IJWTTokenService
    {
        string GenerateJwt(IUserClaimsOptions userModelForTokenGen, IList<string> roles, IJWTOptions jwtSettings);
    }
}

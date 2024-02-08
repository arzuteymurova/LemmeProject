using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace LemmeProject.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Scan(
                scan =>
                    scan.FromAssemblies(typeof(IInfrastructureAssemblyMarker).Assembly)
                        .AddClasses(
                            @class =>
                                @class.Where(
                                    type => !type.Name.StartsWith('I') && type.Name.EndsWith("Repository")))
                        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
            );


            return services;
        }
    }
}

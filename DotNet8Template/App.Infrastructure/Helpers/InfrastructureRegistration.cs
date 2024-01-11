using App.Infrastructure.Repositories.User;
using App.Infrastructure.RepositoryInterfaces.User;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.Helpers
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            //services.AddScoped<IUserTypeRepository, UserTypeRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}

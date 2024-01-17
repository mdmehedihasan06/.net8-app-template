using App.Infrastructure.Repositories.Menu;
using App.Infrastructure.Repositories.Role;
using App.Infrastructure.Repositories.User;
using App.Infrastructure.RepositoryInterfaces.Menu;
using App.Infrastructure.RepositoryInterfaces.Role;
using App.Infrastructure.RepositoryInterfaces.User;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.Helpers
{
    public static class InfrastructureRegistration
    {        
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            // User
            services.AddScoped<IUserTypeRepository, UserTypeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            // Role
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleMenuPermissionRepository, RoleMenuPermissionRepository>();

            // Menu
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IUserMenuPermissionRepository, UserMenuPermissionRepository>();

            return services;
        }
    }
}

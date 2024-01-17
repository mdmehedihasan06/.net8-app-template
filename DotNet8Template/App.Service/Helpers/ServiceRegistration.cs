using App.Infrastructure.Repositories.Menu;
using App.Infrastructure.Repositories.Role;
using App.Infrastructure.Repositories.User;
using App.Infrastructure.RepositoryInterfaces.Menu;
using App.Infrastructure.RepositoryInterfaces.Role;
using App.Infrastructure.RepositoryInterfaces.User;
using App.Service.ServiceInterfaces.Menu;
using App.Service.ServiceInterfaces.Role;
using App.Service.ServiceInterfaces.User;
using App.Service.Services.Menu;
using App.Service.Services.Role;
using App.Service.Services.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Helpers
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // User
            services.AddScoped<IUserTypeService, UserTypeService>();
            services.AddScoped<IUserService, UserService>();

            // Role
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleMenuPermissionService, RoleMenuPermissionService>();

            // Menu
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IUserMenuPermissionService, UserMenuPermissionService>();

            return services;
        }
    }
}

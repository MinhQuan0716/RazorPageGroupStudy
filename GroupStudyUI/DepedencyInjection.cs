using Application.IService;
using Application.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GroupStudyUI
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddWebService(this IServiceCollection services, IConfiguration config) 
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IPostService, PostService>();    
            services.AddScoped<IUserGroupService , UserGroupService>(); 
            return services;
        
        }
    }
}

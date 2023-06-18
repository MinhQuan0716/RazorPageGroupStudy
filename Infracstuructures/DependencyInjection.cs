using Application.IRepositories;
using Infracstuructures.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstuructures
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfractstructure(this IServiceCollection services, IConfiguration config)
        {
            // Use local DB
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("GroupStudy.Db")));

            services.AddScoped<IAttachFileRepo, AttachFileRepo>();
            services.AddScoped<ICommentRepo, CommentRepo>();
            services.AddScoped<IGroupRepo, GroupRepo>();
            services.AddScoped<IGroupRoleRepo, GroupRoleRepo>();
            services.AddScoped<IPostRepo, PostRepo>();
            services.AddScoped<IUserGroupRepo, UserGroupRepo>();
            services.AddScoped<IUserRepo, UserRepo>();

            return services;
        }
    }
}

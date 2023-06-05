using Application.Commons;
using Application.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstuructures.Repositories
{
    public class GroupRoleRepo : GenericRepo<GroupRole>, IGroupRoleRepo
    {
        public GroupRoleRepo(AppDbContext context) : base(context)
        {
        }
    }
}

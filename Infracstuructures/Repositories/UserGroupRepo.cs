using Application.IRepositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstuructures.Repositories
{
    public class UserGroupRepo : GenericRepo<UserGroup>, IUserGroupRepo
    {
        public UserGroupRepo(AppDbContext context) : base(context)
        {
        }
    }
}

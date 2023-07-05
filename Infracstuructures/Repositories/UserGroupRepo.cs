using Application.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstuructures.Repositories
{
    public class UserGroupRepo : GenericRepo<UserGroup>, IUserGroupRepo
    {
        private AppDbContext _appDbContext;
        public UserGroupRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<UserGroup> GetById(int userId, int groupId)
        {
            return await _appDbContext.UserGroups.AsNoTracking().SingleOrDefaultAsync(ug => ug.GroupId == groupId && ug.UserId == userId);
        }
        public async Task<UserGroup> isUserExisted(int userId)
        {
            return await _appDbContext.UserGroups.FirstOrDefaultAsync(ug=> ug.UserId == userId);   
        }
    }
}

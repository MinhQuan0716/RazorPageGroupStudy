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
    public class GroupRepo : GenericRepo<Group>, IGroupRepo
    { 
        private AppDbContext _appDbContext;
        public GroupRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<List<Group>> GetJoinedGroup(int userId)
        {
            var group = await _appDbContext.Groups.Include(x => x.UserGroups)
                                                .ThenInclude(y => y.User.Id == userId)
                                                .ToListAsync();
            return group;
        }
    }
}

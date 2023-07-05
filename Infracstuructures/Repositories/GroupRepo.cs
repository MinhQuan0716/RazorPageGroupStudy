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
		public async Task UpdateGroup(Group group)
		{
			_appDbContext.Groups.Update(group);
			await _appDbContext.SaveChangesAsync();
		}

		public async Task<List<Group>> GetAllGroupV2()
        {
            var allGroups= await _appDbContext.Groups.OrderBy(g=>g.Name)
                                               .Where(x=>x.Status.Equals("Public"))
                                               .ToListAsync();
            return allGroups;
        }

        /*public async Task<List<Group>> GetJoinedGroup(int userId)
        {
            var group = await _appDbContext.Groups.Include(x => x.UserGroups)
                                                .ThenInclude(y => y.User.Id == userId)
                                                .ToListAsync();
            return group;
        }*/

        public async Task<List<Group>> GetJoinedGroup(int userId)
        {
            var user = await _appDbContext.Users
                .Include(u => u.UserGroups)
                .ThenInclude(ug => ug.Group)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                var groups = user.UserGroups.Select(ug => ug.Group).ToList();
                return groups;
            }

            return null; // or return new List<Group>();
        }


        public async Task<List<Group>> GetSearchGroup(string groupName)
        {
            var searchGroup = await _appDbContext.Groups
                                        .Where(x => x.Name.Contains(groupName)&&x.Status.Equals("Public"))
                                        .ToListAsync();
            return searchGroup;
        }

		public async Task<List<Group>> GetAdminGroup(int userId)
		{
			var adminGroups = await _appDbContext.UserGroups
				.Where(ug => ug.UserId == userId && ug.GroupRoleId == 1)
				.Join(_appDbContext.Groups,
					ug => ug.GroupId,
					g => g.Id,
					(ug, g) => g)
				.ToListAsync();

			return adminGroups;
		}
		public async Task<List<Group>> GetModeratorGroup(int userId)
		{
			var adminGroups = await _appDbContext.UserGroups
				.Where(ug => ug.UserId == userId && ug.GroupRoleId == 2)
				.Join(_appDbContext.Groups,
					ug => ug.GroupId,
					g => g.Id,
					(ug, g) => g)
				.ToListAsync();

			return adminGroups;
		}
        public async Task<int> GetUserRoleIdInGroup(int userId, int groupId)
        {
            var userRoleId = await _appDbContext.UserGroups
                .Where(ug => ug.UserId == userId && ug.GroupId == groupId)
                .Select(ug => ug.GroupRoleId)
                .FirstOrDefaultAsync();

            return (int)userRoleId;
        }

        public async Task<Group> GetLastSavedGroup()
        {
            return  await _appDbContext.Groups.OrderByDescending(x=>x.Id).FirstAsync();
        }

        public async Task<List<Group>> GetAllGroupV3()
        {
            return await _appDbContext.Groups.Where(x=>x.isDeleted==false).ToListAsync();
        }

		public async Task<Group> GetGroupThroughLink(string inviteUrl)
		{
            return await _appDbContext.Groups.SingleOrDefaultAsync(x => x.InviteUrl.Equals(inviteUrl));
		}
	}
}

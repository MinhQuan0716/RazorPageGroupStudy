﻿using Application.IRepositories;
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
                                        .Where(x => x.Name.Contains(groupName))
                                        .ToListAsync();
            return searchGroup;
        }
    }
}

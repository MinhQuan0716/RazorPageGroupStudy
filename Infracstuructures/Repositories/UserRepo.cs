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
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        private readonly AppDbContext _dbContext;

        public UserRepo(AppDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<bool> CheckEmailDuplicateAsync(string email)
        {
           return await _dbSet.AnyAsync(x => x.Email == email);
        }

        public async Task<User> GetUserAsync(string email, string password)
        {
          User user= await _dbSet.FirstOrDefaultAsync(x => x.Email.Equals(email)&&x.Password.Equals(password));
            return user;
        }
        public async Task<List<User>> GetUsersByGroupId(int groupId)
        {
            return await _dbContext.Users
                .Join(_dbContext.UserGroups,
                    u => u.Id,
                    ug => ug.UserId,
                    (u, ug) => new { User = u, UserGroup = ug })
                .Where(j => j.UserGroup.GroupId == groupId)
                .Select(j => j.User)
                .ToListAsync();
        }
    }
}

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
        public UserRepo(AppDbContext context) : base(context)
        {
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
    }
}

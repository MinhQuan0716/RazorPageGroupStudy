using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IUserGroupRepo : IGenericRepo<UserGroup>
    {
        Task<UserGroup> GetById(int userId, int groupId);
        Task<UserGroup> isUserExisted(int userId);

	}
}

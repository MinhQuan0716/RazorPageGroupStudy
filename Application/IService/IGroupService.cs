using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public  interface IGroupService
    {
        Task<IEnumerable<Group>> GetAllGroup();
        Task<bool> CreateGroup(string name,string description,string status,string inviteUrl);
    }
}

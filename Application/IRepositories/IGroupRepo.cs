﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IGroupRepo : IGenericRepo<Group>
    {
        Task<List<Group>> GetJoinedGroup(int userId);
        Task<List<Group>> GetSearchGroup(string groupName);
        Task<List<Group>> GetAllGroupV2();
    }
}

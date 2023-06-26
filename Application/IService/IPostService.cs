﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public  interface IPostService
    {
        Task<List<Post>> SearchPost(string content);
        Task<List<Post>> SortPostByNewestDay(int groupId);
        Task<int> PostAmountInGroup(int groupId);
		Task<List<Post>> GetPostsByGroupId(int groupId);
	}
}

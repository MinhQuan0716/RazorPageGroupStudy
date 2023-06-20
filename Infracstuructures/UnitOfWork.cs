using Application;
using Application.IRepositories;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstuructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAttachFileRepo _attachFileRepo;
        private readonly ICommentRepo _commentRepo;
        private readonly IGroupRepo _groupRepo;
        private readonly IGroupRoleRepo _groupRoleRepo;
        private readonly IPostRepo _postRepo;
        private readonly IUserRepo _userRepo;
        private readonly IUserGroupRepo _userGroupRepo;
        public UnitOfWork(AppDbContext appDbContext, IAttachFileRepo attachFileRepo, ICommentRepo commentRepo, IGroupRepo groupRepo, IGroupRoleRepo groupRoleRepo, IPostRepo postRepo, IUserRepo userRepo, IUserGroupRepo userGroupRepo)
        {
            _appDbContext = appDbContext;
            _attachFileRepo = attachFileRepo;
            _commentRepo = commentRepo;
            _groupRepo = groupRepo;
            _groupRoleRepo = groupRoleRepo;
            _postRepo = postRepo;
            _userRepo = userRepo;
            _userGroupRepo = userGroupRepo;
        }
        public IAttachFileRepo AttachFileRepo => _attachFileRepo;

        public ICommentRepo CommentRepo => _commentRepo;

        public IGroupRepo GroupRepo => _groupRepo;

        public IGroupRoleRepo GroupRoleRepo => _groupRoleRepo;

        public IPostRepo PostRepo => _postRepo;

        public IUserRepo UserRepo =>_userRepo;

        public IUserGroupRepo UserGroupRepo =>_userGroupRepo;

        public async Task<int> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }
    }
}

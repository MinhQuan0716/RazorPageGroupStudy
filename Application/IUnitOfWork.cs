using Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUnitOfWork
    {
        public IAttachFileRepo AttachFileRepo { get; }
        public ICommentRepo CommentRepo { get; }
        public IGroupRepo GroupRepo { get; }
        public IGroupRoleRepo GroupRoleRepo { get; }
        public IPostRepo PostRepo { get; }
        public IUserRepo UserRepo { get; }
        public IUserGroupRepo UserGroupRepo { get; }

        public Task<int> SaveChangesAsync();
    }
}

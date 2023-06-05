using Application.IRepositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstuructures.Repositories
{
    public class CommentRepo : GenericRepo<Comment>, ICommentRepo
    {
        public CommentRepo(AppDbContext context) : base(context)
        {
        }
    }
}

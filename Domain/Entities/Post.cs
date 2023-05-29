using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public int CreateUserId { get; set; }
        public int GroupId { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }

        public Group Group { get; set; }
        public User User { get; set; }
        public ICollection<AttachFile> AttachFiles { get; set; }
        public ICollection<Comment> Comment { get; set; }
    }
}

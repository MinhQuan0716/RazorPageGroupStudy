using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int CreateByUserId { get; set; }
        public int PostId { get; set; }
        public DateTime CreateDate { get; set; }
        public int? ReplyToCommentId { get; set; }

        public Post Post { get; set; }
        public User CommentUser { get; set; }
        public Comment? ReplyComment { get; set; }

    }
}

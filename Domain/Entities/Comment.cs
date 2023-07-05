using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("CommentUser")]
        public int CreateByUserId { get; set; }
        [Required]
        [ForeignKey("Post")]
        public int? PostId { get; set; }
        public string? Content { get; set; }
        public DateTime CreateDate { get; set; }

        public Post Post { get; set; }
        public User CommentUser { get; set; }
        public ICollection<CommentMap> ParentComment { get; set; }
        public ICollection<CommentMap> ChildComment { get; set; }

    }
}

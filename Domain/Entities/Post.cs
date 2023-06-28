using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("User")]
        public int CreateUserId { get; set; }
        [Required]
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        [Required]
        public string Content { get; set; }
        public string? PostTitle { get; set; }
        [ForeignKey("PostStatus")]
        public int? PostStatusId { get; set; }
        public PostStatus PostStatus { get; set; }
        public int? CommentOnPost { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        public bool? isReport { get; set; }  
        public Group Group { get; set; }
        public User User { get; set; }
        public ICollection<AttachFile> AttachFiles { get; set; }
        public ICollection<Comment>? Comment { get; set; }
    }
}

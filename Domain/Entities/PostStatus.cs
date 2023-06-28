using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class PostStatus
    {
        [Key]
        public int PostStatusId { get; set; }
        public string PostStatusName { get; set; }  
        public ICollection<Post> Posts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{ 
    public  class CommentMap
    {
        [ForeignKey("Comment")]
        public int? ParentCommentId { get; set; }
        public int? SubCommentId { get; set; }   
        public Comment ParentComment { get; set; }
        public Comment SubComment { get; set; }

    }
}

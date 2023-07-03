using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [PrimaryKey(nameof(UserId), nameof(GroupId))]
    public class UserGroup
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? BannedDate { get; set;}
        public bool? isBanned { get; set; }  
        [ForeignKey("GroupRole")]
        public int GroupRoleId { get; set; }

        public GroupRole GroupRole { get; set; }
        public Group Group { get; set; }
        public User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? BannedDate { get; set;}
        public int GroupRoleId { get; set; }

        public GroupRole GroupRole { get; set; }
        public Group Group { get; set; }
        public User User { get; set; }
    }
}

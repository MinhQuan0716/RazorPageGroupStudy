using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GroupRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }

        public ICollection<UserGroup> UserGroups { get; set;}
    }
}

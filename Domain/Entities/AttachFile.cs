using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AttachFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirebaseUrl { get; set; }
        public int OrderInPost { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}

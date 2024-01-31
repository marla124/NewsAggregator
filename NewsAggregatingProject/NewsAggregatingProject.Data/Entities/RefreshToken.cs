using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregatingProject.Data.Entities
{
    public class RefreshToken:IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime GeneratedAt { get; set; }
        public DateTime ExpiringAt { get; set; }
        public string AssociateDeviceName { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

    }
}

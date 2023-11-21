using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregatingProject.Data.Entities
{
    internal interface IBaseEntity
    {
        Guid Id { get; set; }
    }
}

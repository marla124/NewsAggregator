using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregatingProject.Data.CQS.Commands
{
    public class SetNewsRateCommand:IRequest
    {
        public Guid Id { get; set; }
        public int? Rate { get; set; }
    }
}

using MediatR;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregatingProject.Data.CQS.Queries
{
    public class GetNewsTextByIdQuery : IRequest<string>
    {
        public Guid Id {  get; set; }
    }
}

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
    public class GetUserByEmailQuery : IRequest<User>
    {
        public string Email {  get; set; }
    }
}

﻿using MediatR;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregatingProject.Data.CQS.Queries
{
    public class GetRefreshtokenQuery : IRequest<RefreshToken>
    {
        public Guid Id {  get; set; }
    }
}
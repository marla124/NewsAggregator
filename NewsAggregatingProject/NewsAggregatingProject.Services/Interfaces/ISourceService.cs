using NewsAggregatingProject.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregatingProject.Services.Interfaces
{
    public interface ISourceService
    {
        public Task<SourceDto> GetSourceById(Guid id);
        public Task<SourceDto> UpdateSourcebyId(Guid id);
        public Task DeleteSource(Guid id);

    }
}

using NewsAggregatingProject.API.Mappers;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Repositories;
using NewsAggregatingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregatingProject.Services
{
    public class SourceService : ISourceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SourceMapper _sourceMapper;


        public SourceService(IUnitOfWork unitOfWork, SourceMapper sourceMapper)
        {
            _unitOfWork = unitOfWork;
            _sourceMapper = sourceMapper;
        }
        public async Task DeleteSource(Guid id)
        {
            await _unitOfWork.SourceRepository.DeleteById(id);
            await _unitOfWork.Commit();
        }

        public async Task<SourceDto> GetSourceById(Guid id)
        {
            var source = (await _unitOfWork.SourceRepository.GetById(id));
            if (source == null)
            {
                return null;
            }
            return _sourceMapper.SourceToSourceDto(source);
        }
    }
}

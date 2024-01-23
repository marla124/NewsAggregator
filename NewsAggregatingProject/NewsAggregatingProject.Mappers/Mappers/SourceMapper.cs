using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data.Entities;
using NewsAggregatingProject.Models;
using Riok.Mapperly.Abstractions;


namespace NewsAggregatingProject.API.Mappers
{
    [Mapper]
    public partial class SourceMapper
    {
        public partial SourceDto SourceToSourceDto(Source source);
        public partial Source SourceDtoToSource(SourceDto dto);
        public partial SourceModel SourceDtoToSourceModel(SourceDto dto);
    }
}

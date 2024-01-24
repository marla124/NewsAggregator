using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data.Entities;
using NewsAggregatingProject.Models;
using Riok.Mapperly.Abstractions;
using System.ServiceModel.Syndication;

namespace NewsAggregatingProject.API.Mappers
{
    [Mapper]
    public partial class NewsMapper
    {
        public partial NewsDto NewsToNewsDto(News news);
        public partial News NewsDtoToNews(NewsDto dto);
        public partial NewsModel NewsDtoToNewsModel(NewsDto dto);
        public partial NewsDto NewsModelToNewsDto(NewsModel model);




    }
}

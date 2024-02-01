using MediatR;
using Microsoft.Extensions.Configuration;
using NewsAggregatingProject.API.Mappers;
using NewsAggregatingProject.Data.CQS.Queries;
using NewsAggregatingProject.Data.Entities;
using NewsAggregatingProject.Data.Migrations;
using NewsAggregatingProject.Repositories;
using NewsAggregatingProject.Services.Interfaces;
using NSubstitute.ReturnsExtensions;

namespace NewsAggregatingProject.Services.Tests
{
    public class NewsServiceTests
    {
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IMediator _mediatorMock;
        private readonly IConfiguration _configurationMock;
        private readonly NewsService _newsService;

        public NewsServiceTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _mediatorMock = Substitute.For<IMediator>();
            _configurationMock = Substitute.For<IConfiguration>();
            _newsService = new NewsService(_unitOfWorkMock, new NewsMapper(), _mediatorMock, _configurationMock);
        }

        [Fact]
        public async void GetNewsById_NonUnexistedId_ReturnNull()
        {
            var id = Guid.Empty;
            _mediatorMock.Send(Arg.Is<Guid>(guid => guid.Equals(id)))
                .ReturnsNull();
            var result = await _newsService.GetNewsById(id);

            Assert.Null(result);
        }
        [Fact]
        public async void GetNewsById_ReturnNewsDto()
        {
            var id = Guid.NewGuid();
            _mediatorMock.Send(Arg.Any<GetNewsByIdQuery>())
                .Returns(new News()
                {
                    Id = id
                });

            var result = await _newsService.GetNewsById(id);

            Assert.Equal(result?.Id, id);
        }
    }
}
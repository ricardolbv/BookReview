using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookReview.Application.Contracts.Persistence;
using BookReview.Domain.Entities;
using AutoMapper;
using BookReview.Application.UnitTests.Mocks;
using BookReview.Application.Profiles;
using Xunit;
using BookReview.Application.Features.Reviews.Queries.GetAllReviews;
using System.Threading;
using Shouldly;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace BookReview.Application.UnitTests.Reviews.Queries
{
    public class GetAllReviewsQueryHandlerTests
    {
        private readonly Mock<IReviewRepository> _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllReviewsQueryHandler> _logger;

        public GetAllReviewsQueryHandlerTests()
        {
            _repo = RepositoryMocks.GetMockReviewRepository();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = config.CreateMapper();
            _logger = new NullLogger<GetAllReviewsQueryHandler>();
        }

        [Fact]
        public async Task GetAll_Reviews_Query()
        {
            var handler = new GetAllReviewsQueryHandler(_repo.Object, _mapper, _logger);

            var resp = await handler.Handle(new GetAllReviewsQuery(), CancellationToken.None);

            //Asserts
            resp.ShouldBeOfType<List<GetAllReviewsDto>>();
            resp.Count.ShouldBe(3);
        }
    }
}

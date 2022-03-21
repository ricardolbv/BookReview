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

namespace BookReview.Application.UnitTests.Reviews.Queries
{
    public class GetAllReviewsQueryTests
    {
        private readonly Mock<IReviewRepository> _repo;
        private readonly IMapper _mapper;

        public GetAllReviewsQueryTests()
        {
            _repo = RepositoryMocks.GetMockReviewRepository();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetAll_Reviews_Query()
        {
            var handler = new GetAllReviewsQueryHandler(_repo.Object, _mapper);

            var resp = await handler.Handle(new GetAllReviewsQuery(), CancellationToken.None);

            //Asserts
            resp.ShouldBeOfType<List<GetAllReviewsDto>>();
            resp.Count.ShouldBe(3);
        }
    }
}

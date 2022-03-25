using AutoMapper;
using BookReview.Application.Contracts.Persistence;
using BookReview.Application.Features.Reviews.Commands.CreateReview;
using BookReview.Application.Profiles;
using BookReview.Application.UnitTests.Mocks;
using BookReview.Domain.Enums;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BookReview.Application.UnitTests.Reviews.Commands
{
    public class CreateReviewCommandHandlerTests
    {
        private readonly Mock<IReviewRepository> _repo;
        private readonly IMapper _mapper;

        public CreateReviewCommandHandlerTests()
        {
            _repo = RepositoryMocks.GetMockReviewRepository();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Can_create_review()
        {
            var handler = new CreateReviewCommandHandler(_mapper, _repo.Object);

            var resp = await handler.Handle(new CreateReviewCommand()
            {
                Text = "Testing ekofjowifoijwrijgio  iorgjio eroi g" +
                "test sett "
            }, CancellationToken.None);

            var reviews = await _repo.Object.ListAllAsync();

            //Asserts
            reviews.Count.ShouldBe(4);
            resp.ShouldBeOfType<CreateReviewDto>();
            resp.State.ShouldBe(ReviewState.Created);
        }
    }
}

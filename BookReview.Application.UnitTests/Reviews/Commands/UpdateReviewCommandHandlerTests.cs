using AutoMapper;
using BookReview.Application.Contracts.Persistence;
using BookReview.Application.Features.Reviews.Commands.UpdateReview;
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
    public class UpdateReviewCommandHandlerTests
    {
        private readonly Mock<IReviewRepository> _repo;
        private readonly IMapper _mapper;

        public UpdateReviewCommandHandlerTests()
        {
            _repo = RepositoryMocks.GetMockReviewRepository();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Can_update_review()
        {
            var handler = new UpdateReviewCommandHandler(_repo.Object, _mapper);
            var result = await handler.Handle(new UpdateReviewCommand { Id = 1, State = ReviewState.Working, Text = "Testing an great update" }, 
                CancellationToken.None);

            //Assert
            result.State.ShouldBe(ReviewState.Working);
            result.Text.ShouldBe("Testing an great update");
            result.ShouldBeOfType<ReviewUpdateDto>();
        }
    }
}

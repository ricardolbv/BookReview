using AutoMapper;
using BookReview.Application.Contracts.Persistence;
using BookReview.Application.Exceptions;
using BookReview.Application.Features.Reviews.Commands.UpdateReview;
using BookReview.Application.Profiles;
using BookReview.Application.UnitTests.Mocks;
using BookReview.Domain.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
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
        private readonly ILogger<UpdateReviewCommandHandler> _logger;

        public UpdateReviewCommandHandlerTests()
        {
            _repo = RepositoryMocks.GetMockReviewRepository();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = config.CreateMapper();
            _logger = new NullLogger<UpdateReviewCommandHandler>();
        }

        [Fact]
        public async Task Can_update_review()
        {
            var handler = new UpdateReviewCommandHandler(_repo.Object, _mapper, _logger);
            var result = await handler.Handle(new UpdateReviewCommand { Id = 1, State = ReviewState.Working, Text = "Testing an great update" }, 
                CancellationToken.None);

            //Assert
            result.Review.State.ShouldBe(ReviewState.Working);
            result.Review.Text.ShouldBe("Testing an great update");
            result.ShouldBeOfType<UpdateReviewReponse>();
            result.Message.ShouldNotBeEmpty();
            result.Message.ShouldNotBeNull();
        }

        [Fact]
        public async Task Can_not_update_review()
        {
            var handler = new UpdateReviewCommandHandler(_repo.Object, _mapper, _logger);
            var resp = handler.Handle(new UpdateReviewCommand { Id = 0, State = ReviewState.Working, Text = "Just a valid test teste test"},
                CancellationToken.None);

            //Asserts
            resp.ShouldThrow<NotFoundException>();
        }

        [Fact]
        public async Task Also_can_not_update_review()
        {
            var handler = new UpdateReviewCommandHandler(_repo.Object, _mapper, _logger);
            var resp = await handler.Handle(new UpdateReviewCommand { Id = 1, State = ReviewState.Working, Text = "To short" },
                CancellationToken.None);

            //Asserts
            resp.Message.ShouldNotBeEmpty();
            resp.Message.ShouldNotBeNull();
            resp.Errors.Count.ShouldBeGreaterThan(0);
        }

    }
}

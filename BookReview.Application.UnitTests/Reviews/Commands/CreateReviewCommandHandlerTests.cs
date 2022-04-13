using AutoMapper;
using BookReview.Application.Contracts.Persistence;
using BookReview.Application.Exceptions;
using BookReview.Application.Features.Reviews.Commands.CreateReview;
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
    public class CreateReviewCommandHandlerTests
    {
        private readonly Mock<IReviewRepository> _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateReviewCommandHandler> _logger;

        public CreateReviewCommandHandlerTests()
        {
            _repo = RepositoryMocks.GetMockReviewRepository();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = config.CreateMapper();
            _logger = new NullLogger<CreateReviewCommandHandler>();
        }

        [Fact]
        public async Task Can_create_review()
        {
            var handler = new CreateReviewCommandHandler(_mapper, _repo.Object, _logger);

            var resp = await handler.Handle(new CreateReviewCommand()
            {
                Text = "Testing ekofjowifoijwrijgio  iorgjio eroi g" +
                "test sett "
            }, CancellationToken.None);

            var reviews = await _repo.Object.ListAllAsync();

            //Asserts
            reviews.Count.ShouldBe(4);
            resp.ShouldBeOfType<CreateReviewCommandResponse>();
            resp.Review.State.ShouldBe(ReviewState.Created);
            resp.Success.ShouldBeTrue();
            resp.Message.ShouldNotBeEmpty();
            resp.Message.ShouldNotBeNull();
        }

       [Fact]
       public async Task Can_not_create_review()
        {
            var handler = new CreateReviewCommandHandler(_mapper, _repo.Object, _logger);

            var resp = await handler.Handle(new CreateReviewCommand
            {
                Text = "To Short"
            }, CancellationToken.None);

            //Asserts
            resp.Success.ShouldBeFalse();
            resp.Message.ShouldNotBeEmpty();
            resp.Message.ShouldNotBeNull();
        }

        [Fact]
        public async Task Also_can_not_create_review()
        {
            var handler = new CreateReviewCommandHandler(_mapper, _repo.Object, _logger);

            var resp = await handler.Handle(new CreateReviewCommand
            {
                Text = ""
            }, CancellationToken.None);

            //Asserts
            resp.Success.ShouldBeFalse();
            resp.Message.ShouldNotBeEmpty();
            resp.Message.ShouldNotBeNull();
        }
    }
}

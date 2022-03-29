using AutoMapper;
using BookReview.Application.Contracts.Persistence;
using BookReview.Application.Exceptions;
using BookReview.Application.Features.Reviews.Commands.DeleteReviewById;
using BookReview.Application.Profiles;
using BookReview.Application.UnitTests.Mocks;
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
    public class DeleteReviewByIdCommandHandlerTests
    {
        private readonly Mock<IReviewRepository> _repo;
        private readonly IMapper _mapper;

        public DeleteReviewByIdCommandHandlerTests()
        {
            _repo = RepositoryMocks.GetMockReviewRepository();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper =  config.CreateMapper();
        }

        [Fact]
        public async Task Can_delete_review_by_id()
        {
            var handler = new DeleteReviewByIdCommandHandler(_mapper, _repo.Object);
            var resp = await handler.Handle(new DeleteReviewByIdCommand { Id = 1}, CancellationToken.None);

            var reviews = await _repo.Object.ListAllAsync();

            //Asserts
            reviews.Count.ShouldBe(2);
            resp.ShouldBeOfType<DeleteReviewByIdResponse>();
            resp.Message.ShouldNotBeNull();
            resp.Message.ShouldNotBeEmpty();
            resp.Success.ShouldBeTrue();
        }

        [Fact]
        public async Task Can_not_delete_review_by_id()
        {
            var handler = new DeleteReviewByIdCommandHandler(_mapper, _repo.Object);
            var resp = handler.Handle(new DeleteReviewByIdCommand { Id = 0}, CancellationToken.None);

            //Asserts
            resp.ShouldThrow<NotFoundException>();
        }
    }
}

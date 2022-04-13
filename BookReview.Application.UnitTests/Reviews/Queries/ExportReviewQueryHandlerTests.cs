using AutoMapper;
using BookReview.Application.Contracts.Infraestructure;
using BookReview.Application.Contracts.Persistence;
using BookReview.Application.Features.Reviews.Queries.ExportAllReviews;
using BookReview.Application.Profiles;
using BookReview.Application.UnitTests.Mocks;
using BookReview.Infraestructure.FileExporter;
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

namespace BookReview.Application.UnitTests.Reviews.Queries
{
    public class ExportReviewQueryHandlerTests
    {
        private readonly ICsvExporter _csvExporter;
        private readonly Mock<IReviewRepository> _reviewRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<ExportReviewQueryHandler> _logger;

        public ExportReviewQueryHandlerTests()
        {
            _reviewRepo = RepositoryMocks.GetMockReviewRepository();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = config.CreateMapper();
            _csvExporter = new CsvExporter();
            _logger = new NullLogger<ExportReviewQueryHandler>();
        }

        [Fact]
        public async Task Should_generate_a_valid_report()
        {
            var handler = new ExportReviewQueryHandler(_mapper, _csvExporter, _reviewRepo.Object, _logger);
            var resp = await handler.Handle(new ExportReviewQuery(), CancellationToken.None);

            //Asserts
            resp.FileName.ShouldBeOfType<string>();
            resp.FileName.ShouldContain(".csv");
            resp.Data.ShouldNotBeNull();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using BookReview.Domain.Entities;
using BookReview.Application.Features.Reviews.Commands.CreateReview;
using BookReview.Application.Features.Reviews.Commands.DeleteReviewById;
using BookReview.Application.Features.Reviews.Queries.GetAllReviews;
using BookReview.Application.Features.Reviews.Commands.UpdateReview;
using BookReview.Application.Features.Reviews.Queries.ExportAllReviews;
using Microsoft.Extensions.Logging;
using BookReview.Domain.Common;
using BookReview.Application.Contracts.Infraestructure;
using BookReview.Application.Features.Rating.Commands.CreateRating;

namespace BookReview.Api.Controllers
{
    [ApiController]
    [Route("api/review")]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ReviewController> _logger;
        private readonly IMessageBus _bus;

        public ReviewController(IMediator mediator, ILogger<ReviewController> logger, IMessageBus bus)
        {
            _mediator = mediator;
            _logger = logger;
            _bus = bus;
        }

        [HttpPost("addReview")]
        public async Task<ActionResult<CreateReviewCommandResponse>> AddReview([FromBody] CreateReviewCommand createReviewCommand)
        {
            var response = await _mediator.Send(createReviewCommand);
            return Ok(response);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<GetAllReviewsDto>>> GetAll()
        {
                var response = await _mediator.Send(new GetAllReviewsQuery());
                return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<DeleteReviewByIdResponse>> DeleteReview([FromBody] DeleteReviewByIdCommand deleteReviewByIdCommand)
        {
            var response = await _mediator.Send(deleteReviewByIdCommand);
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<ActionResult<UpdateReviewReponse>> UpdateReview([FromBody] UpdateReviewCommand updateReviewCommand)
        {
            var response = await _mediator.Send(updateReviewCommand);
            return Ok(response);
        }

        [HttpGet("export")]
        public async Task<FileResult> ExportReviews()
        {
            var fileDto = await _mediator.Send(new ExportReviewQuery());

            return File(fileDto.Data, fileDto.ContentType, fileDto.FileName);
        }

        [HttpPost("bus-message")]
        public async Task<ActionResult<CreateRatingCommandResponse>> BusMessage([FromBody] CreateRatingCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}

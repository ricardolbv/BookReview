using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

namespace BookReview.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
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
        public async Task<ActionResult<ReviewUpdateDto>> UpdateReview([FromBody] UpdateReviewCommand updateReviewCommand)
        {
            var response = await _mediator.Send(updateReviewCommand);
            return Ok(response);
        }
    }
}

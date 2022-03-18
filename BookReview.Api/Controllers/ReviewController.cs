using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using BookReview.Domain.Entities;
using BookReview.Application.Features.Reviews.Commands.CreateReview;

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
        public async Task<ActionResult<Review>> AddReview([FromBody] CreateReviewCommand createReviewCommand)
        {
            var response = await _mediator.Send(createReviewCommand);
            return Ok(response);
        }
    }
}

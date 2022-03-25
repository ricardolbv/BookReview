using AutoMapper;
using BookReview.Application.Features.Reviews.Commands.CreateReview;
using BookReview.Application.Features.Reviews.Commands.DeleteReviewById;
using BookReview.Application.Features.Reviews.Commands.UpdateReview;
using BookReview.Application.Features.Reviews.Queries.GetAllReviews;
using BookReview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Application.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, ReviewUpdateDto>().ReverseMap();
            CreateMap<Review, UpdateReviewCommand>().ReverseMap();
            CreateMap<Review, DeleteReviewByIdCommand>().ReverseMap();
            CreateMap<Review, CreateReviewCommand>().ReverseMap();
            CreateMap<Review, GetAllReviewsDto>().ReverseMap();
        }
    }
}

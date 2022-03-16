using AutoMapper;
using BookReview.Application.Features.Reviews.Commands.CreateReview;
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
            CreateMap<Review, CreateReviewCommand>().ReverseMap();
        }
    }
}

﻿using BookReview.Application.Contracts.Persistence;
using BookReview.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Application.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<IAsyncRepository<Review>> GetMockReviewRepository()
        {
            var reviews = new List<Review>()
            {
                new Review()
                {
                    Id = 1,
                    Text = "Text it's gonna Be greaternfe ok jekejfkefmkefkef"
                },

                new Review()
                {
                    Id = 2,
                    Text = "Texttsgrgçrekglwekgpo lekflekl fefmemklefm mmmmm"
                },

                new Review()
                {
                    Id = 3,
                    Text = "fefwegpkj efjowejiorjir wpoefkwepokfpoew k"
                }
            };

            var mockReviewRepository = new Mock<IAsyncRepository<Review>>();

            mockReviewRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(reviews);

            mockReviewRepository.Setup(repo => repo.AddAsync(It.IsAny<Review>())).ReturnsAsync(
                    (Review review) =>
                    {
                        reviews.Add(review);
                        return review;
                    }
                );

            return mockReviewRepository;
        }
    }
}

﻿using FluentValidation;

namespace WebApi.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryValidator : AbstractValidator<GetMovieDetailQuery>
    {
        public GetMovieDetailQueryValidator()
        {
            RuleFor(query => query.MovieId).NotEmpty().GreaterThan(0);
        }
    }
}

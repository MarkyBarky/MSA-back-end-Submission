using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MSA.backend.Api.Model;

namespace UnitTest
{
    class myValidator : AbstractValidator<Move>
    {
        public myValidator()
        {
            RuleFor(x => x.move)
                .NotEmpty()
                .WithMessage("Move is required.");
            RuleFor(x => x.name)
                .NotEmpty()
                .WithMessage("Name is required.");
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleOfFive.Application.UseCase.CheckMultipleOfFive
{
    public class CheckMultipleOfFiveValidation: AbstractValidator<CheckMultipleOfFiveCommand>
    {
        public CheckMultipleOfFiveValidation()
        {
            RuleFor(x => x.Number.Number).Cascade(CascadeMode.Stop).Custom((number, context) =>
            {
                if(number < 0)
                     context.AddFailure("Number must be grater than 0");
            });
        }
    }
}

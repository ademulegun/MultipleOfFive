using MediatR;
using Microsoft.Extensions.Logging;
using MultipleOfFive.Application.UseCase.CheckMultipleOfFive.Dto;
using MultipleOfFive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultipleOfFive.Application.UseCase.CheckMultipleOfFive
{
    public class CheckMultipleOfFiveCommand: IRequest<Result<List<string>>>
    {
        public Model Number { get; set; }
        public class CheckMultipleOfFiveCommandHandler : IRequestHandler<CheckMultipleOfFiveCommand, Result<List<string>>>
        {
            public async Task<Result<List<string>>> Handle(CheckMultipleOfFiveCommand request, CancellationToken cancellationToken)
            {
                await Task.Delay(1);
                List<string> list = new List<string>();
                for (int i = 0; i <= request.Number.Number; i++)
                {
                    if(i % 3 == 0) 
                        list.Add("Fiss");
                    if(i % 5 == 0)
                        list.Add("Buzz");
                    else
                        list.Add(request.Number.Number.ToString());
                }
                return Result.Ok(list);
            }
        }
    }
}

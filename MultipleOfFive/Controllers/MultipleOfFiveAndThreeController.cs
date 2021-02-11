using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MultipleOfFive.Application.UseCase.CheckMultipleOfFive;
using MultipleOfFive.Application.UseCase.CheckMultipleOfFive.Dto;
using MultipleOfFive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultipleOfFive.Controllers
{
    public class MultipleOfFiveAndThreeController : ControllerBase
    {
        private readonly IMediator _Mediator;
        public MultipleOfFiveAndThreeController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        /// <summary>
        /// This endpoint is used to check if a number is a multiple of 5 and 3
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ///<remarks>
        ///Sample request:
        ///
        ///  POST /verifyNumber 
        ///  {
        ///     "Number": 100
        ///  }
        ///</remarks>
        [HttpPost("/verifyNumber")]
        [ProducesResponseType(typeof(Result<List<string>>), 200)]
        [ProducesResponseType(typeof(Result<string>), 400)]
        [ProducesResponseType(typeof(Result<string>), 500)]
        public async Task<IActionResult> VerifyNumber([FromBody]Model model)
        {
            var result = await this._Mediator.Send(new CheckMultipleOfFiveCommand()
            {
                Number = model
            });
            return Ok(result);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MultipleOfFive.Application.UseCase.CheckMultipleOfFive;
using MultipleOfFive.Application.UseCase.CheckMultipleOfFive.Dto;
using MultipleOfFive.Common;
using MultipleOfFive.Controllers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static MultipleOfFive.Application.UseCase.CheckMultipleOfFive.CheckMultipleOfFiveCommand;

namespace MultipleOfFive.Test
{
    public class MultipleOfFiveShouldTestThat
    {
        [Fact]
        public async Task Return_Number_If_Not_A_Multiple_Of_Five_And_Three()
        {
            //Arrange
            Mock<IMediator> mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(It.IsAny<CheckMultipleOfFiveCommand>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(() => Result.Ok(new List<string>()));

            MultipleOfFiveAndThreeController sut = new MultipleOfFiveAndThreeController(mediator.Object);
            Model model = new Model();
            model.Number = 1;
            //Act
            var ret = await sut.VerifyNumber(model);
            //Assert
            Assert.IsType<ViewResult>(await sut.VerifyNumber(model));
        }
    }
}

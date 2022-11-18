using AutoFixture.Xunit2;
using AutoMapper;
using Exercises.Domain.Commands.UpdateExercise;
using Exercises.Domain.Dtos;
using Exercises.Domain.Events;
using Exercises.Domain.Extensions;
using Exercises.Infrastructure.Repositories;
using Exercises.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using System.Net;

namespace Exercises.UnitTests.Commands
{
    public class UpdateExerciseCommandTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IExerciseRepository> _exerciseRepositoryMock;
        private readonly Mock<IExerciseUpdateEventPublisher> _exerciseUpdateEventPublisher;
        private readonly UpdateExerciseHandler _handler;

        public UpdateExerciseCommandTests()
        {
            _mapper = MockMapper.GetMapper();
            _exerciseRepositoryMock = MockExerciseRepository.GetExerciseRepository();
            _exerciseUpdateEventPublisher = new Mock<IExerciseUpdateEventPublisher>();
            _handler = new UpdateExerciseHandler(_mapper, _exerciseRepositoryMock.Object, _exerciseUpdateEventPublisher.Object);
        }

        [Theory, AutoData]
        public async Task UpdateExerciseShouldBeSuccess(UpdateExerciseDto updateExerciseDto)
        {
            //Arrange
            updateExerciseDto.Id = 1;

            //Act
            var response = await _handler.Handle(new UpdateExerciseCommand(updateExerciseDto), CancellationToken.None);

            //Assert
            response.IsSuccess.Should().BeTrue();
            response.ErrorMessage.Should().BeNullOrWhiteSpace();
        }

        [Theory, AutoData]
        public async Task Update_Unexisting_Exercise_Should_Be_Return_NotFound(UpdateExerciseDto updateExerciseDto)
        {
            //Arrange
            updateExerciseDto.Id = 99;

            //Act
            var response = await _handler.Handle(new UpdateExerciseCommand(updateExerciseDto), CancellationToken.None);

            //Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorStatusCode.Should().Be(HttpStatusCode.NotFound.ToInt());
        }

        [Theory, AutoData]
        public async Task Update_Exercise_Error_Saving_Should_Return_BadRequest(UpdateExerciseDto updateExerciseDto)
        {
            //Arrange
            updateExerciseDto.Id = 1;
            _exerciseRepositoryMock.Setup(x => x.SaveAsync())
                 .ReturnsAsync(() => false);

            //Act
            var response = await _handler.Handle(new UpdateExerciseCommand(updateExerciseDto), CancellationToken.None);

            //Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorStatusCode.Should().Be(HttpStatusCode.BadRequest.ToInt());
        }
    }
}
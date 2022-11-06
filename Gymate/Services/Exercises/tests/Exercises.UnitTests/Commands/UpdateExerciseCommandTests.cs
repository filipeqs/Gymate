using AutoFixture.Xunit2;
using AutoMapper;
using Exercises.Domain.Commands.UpdateExercise;
using Exercises.Domain.Extensions;
using Exercises.Domain.Models.Exercise;
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

        public UpdateExerciseCommandTests()
        {
            _exerciseRepositoryMock = MockExerciseRepository.GetExerciseRepository();
            _mapper = MockMapper.GetMapper();
        }

        [Theory, AutoData]
        public async Task Update_Exercise_Should_Be_Success(UpdateExerciseDto updateExerciseDto)
        {
            //Arrange
            updateExerciseDto.Id = 1;
            var handler = new UpdateExerciseHandler(_mapper, _exerciseRepositoryMock.Object);

            //Act
            var response = await handler.Handle(new UpdateExerciseCommand(updateExerciseDto), CancellationToken.None);

            //Assert
            response.IsSuccess.Should().BeTrue();
            response.ErrorMessage.Should().BeNullOrWhiteSpace();
        }

        [Theory, AutoData]
        public async Task Update_Unexisting_Exercise_Should_Be_Return_NotFound(UpdateExerciseDto updateExerciseDto)
        {
            //Arrange
            updateExerciseDto.Id = 99;
            var handler = new UpdateExerciseHandler(_mapper, _exerciseRepositoryMock.Object);

            //Act
            var response = await handler.Handle(new UpdateExerciseCommand(updateExerciseDto), CancellationToken.None);

            //Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorStatusCode.Should().Be(HttpStatusCode.NotFound.ToInt());
        }

        [Theory, AutoData]
        public async Task Update_Exercise_Error_Saving_Should_Return_BadRequest(UpdateExerciseDto updateExerciseDto)
        {
            //Arrange
            updateExerciseDto.Id = 1;
            var handler = new UpdateExerciseHandler(_mapper, _exerciseRepositoryMock.Object);
            _exerciseRepositoryMock.Setup(x => x.SaveAsync())
                 .ReturnsAsync(() => false);

            //Act
            var response = await handler.Handle(new UpdateExerciseCommand(updateExerciseDto), CancellationToken.None);

            //Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorStatusCode.Should().Be(HttpStatusCode.BadRequest.ToInt());
        }
    }
}
using AutoFixture.Xunit2;
using Exercises.Domain.Commands.DeleteExercise;
using Exercises.Domain.Dtos;
using Exercises.Domain.Extensions;
using Exercises.Infrastructure.Repositories;
using Exercises.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using System.Net;

namespace Exercises.UnitTests.Commands
{
    public class DeleteExerciseCommandTests
    {
        private readonly Mock<IExerciseRepository> _exerciseRepositoryMock;

        public DeleteExerciseCommandTests()
        {
            _exerciseRepositoryMock = MockExerciseRepository.GetExerciseRepository();
        }

        [Theory, AutoData]
        public async void Delete_Exercise_Should_Remove_From_List(DeleteExerciseDto deleteExerciseDto)
        {
            //Arrange
            deleteExerciseDto.Id = 1;
            var handler = new DeleteExerciseHandler(_exerciseRepositoryMock.Object);

            //Act
            var response = await handler.Handle(new DeleteExerciseCommand(deleteExerciseDto), CancellationToken.None);
            var exercises = await _exerciseRepositoryMock.Object.GetAllExercisesAsync();

            //Assert
            response.IsSuccess.Should().BeTrue();
            exercises.Count().Should().Be(1);
        }

        [Theory, AutoData]
        public async void Delete_UnExercise_Should_Return_NotFound(DeleteExerciseDto deleteExerciseDto)
        {
            //Arrange
            deleteExerciseDto.Id = 99;
            var handler = new DeleteExerciseHandler(_exerciseRepositoryMock.Object);

            //Act
            var response = await handler.Handle(new DeleteExerciseCommand(deleteExerciseDto), CancellationToken.None);

            //Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorStatusCode.Should().Be(HttpStatusCode.NotFound.ToInt());
        }

        [Theory, AutoData]
        public async void Delete_Exercise_Error_Saving_Should_Return_BadRequest(DeleteExerciseDto deleteExerciseDto)
        {
            //Arrange
            deleteExerciseDto.Id = 1;
            var handler = new DeleteExerciseHandler(_exerciseRepositoryMock.Object);
            _exerciseRepositoryMock.Setup(x => x.SaveAsync())
                .ReturnsAsync(() => false);

            //Act
            var response = await handler.Handle(new DeleteExerciseCommand(deleteExerciseDto), CancellationToken.None);

            //Assert
            response.IsSuccess.Should().BeFalse();
            response.ErrorStatusCode.Should().Be(HttpStatusCode.BadRequest.ToInt());
        }
    }
}

using AutoFixture.Xunit2;
using AutoMapper;
using Exercises.Domain.Commands.CreateExercise;
using Exercises.Domain.Dtos;
using Exercises.Domain.Extensions;
using Exercises.Infrastructure.Repositories;
using Exercises.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using System.Net;

namespace Exercises.UnitTests.Commands
{
    public class CreateExerciseCommandTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IExerciseRepository> _exerciseRepositoryMock;

        public CreateExerciseCommandTests()
        {
            _exerciseRepositoryMock = MockExerciseRepository.GetExerciseRepository();
            _mapper = MockMapper.GetMapper();
        }

        [Theory, AutoData]
        public async Task Create_Exercise_Should_Add_To_List(CreateExerciseDto exerciseDto)
        {
            //Arrange
            var handler = new CreateExerciseHandler(_mapper, _exerciseRepositoryMock.Object);

            //Act
            var response = await handler.Handle(new CreateExerciseCommand(exerciseDto), CancellationToken.None);
            var exercises = await _exerciseRepositoryMock.Object.GetAllExercisesAsync();

            //Assert
            response.IsSuccess.Should().BeTrue();
            response.ExerciseDetailsDto.Should().NotBeNull();
            exercises.Count().Should().Be(3);
        }

        [Theory, AutoData]
        public async Task Create_Exercise_Error_Saving_Should_Not_Add_To_List(CreateExerciseDto exerciseDto)
        {
            //Arrange
            var handler = new CreateExerciseHandler(_mapper, _exerciseRepositoryMock.Object);
            _exerciseRepositoryMock.Setup(x => x.SaveAsync())
                .ReturnsAsync(() => false);

            //Act
            var response = await handler.Handle(new CreateExerciseCommand(exerciseDto), CancellationToken.None);

            //Assert
            response.IsSuccess.Should().BeFalse();
            response.ExerciseDetailsDto.Should().BeNull();
            response.ErrorStatusCode.Should().Be(HttpStatusCode.BadRequest.ToInt());
        }
    }
}
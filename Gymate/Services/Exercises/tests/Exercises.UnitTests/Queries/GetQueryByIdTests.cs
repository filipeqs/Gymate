using AutoMapper;
using Exercises.Domain.Models.Exercise;
using Exercises.Domain.Queries.ExerciseById;
using Exercises.Infrastructure.Repositories;
using Exercises.UnitTests.Mocks;
using FluentAssertions;
using Moq;

namespace Exercises.UnitTests.Queries
{
    public class GetQueryByIdTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IExerciseRepository> _exerciseRepositoryMock;

        public GetQueryByIdTests()
        {
            _exerciseRepositoryMock = MockExerciseRepository.GetExerciseRepository();
            _mapper = MockMapper.GetMapper();
        }

        [Fact]
        public async Task Get_Available_Exercise_Should_Return_Exercise()
        {
            //Arrange
            var handler = new GetExerciseByIdHandler(_mapper, _exerciseRepositoryMock.Object);

            //Act
            var result = await handler.Handle(new GetExerciseByIdQuery(1), CancellationToken.None);

            //Assert
            result.Should().BeOfType<ExerciseDetailsDto>();
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_Unavailable_Exercise_Should_Return_Null()
        {
            //Arrange
            var handler = new GetExerciseByIdHandler(_mapper, _exerciseRepositoryMock.Object);

            //Act
            var result = await handler.Handle(new GetExerciseByIdQuery(99), CancellationToken.None);

            //Assert
            result.Should().BeNull();
        }
    }
}

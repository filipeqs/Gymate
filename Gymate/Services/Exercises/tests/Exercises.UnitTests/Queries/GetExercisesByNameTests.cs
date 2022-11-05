using AutoMapper;
using Exercises.Domain.Queries.GetExercisesByName;
using Exercises.Infrastructure.Repositories;
using Exercises.UnitTests.Mocks;
using FluentAssertions;
using Moq;

namespace Exercises.UnitTests.Queries
{
    public class GetExercisesByNameTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IExerciseRepository> _exerciseRepositoryMock;

        public GetExercisesByNameTests()
        {
            _exerciseRepositoryMock = MockExerciseRepository.GetExerciseRepository();
            _mapper = MockMapper.GetMapper();
        }

        [Fact]
        public async Task Get_Exercises_By_Name_Should_Return_Exercises()
        {
            //Arrange
            var handler = new GetExercisesByNameHandler(_mapper, _exerciseRepositoryMock.Object);

            //Act
            var result = await handler.Handle(new GetExercisesByNameQuery("Deadlift"), CancellationToken.None);

            //Assert
            result.Count().Should().Be(1);
        }

        [Fact]
        public async Task Get_Exercises_By_Name_Should_Return_Null()
        {
            //Arrange
            var handler = new GetExercisesByNameHandler(_mapper, _exerciseRepositoryMock.Object);

            //Act
            var result = await handler.Handle(new GetExercisesByNameQuery("NotAvailable"), CancellationToken.None);

            //Assert
            result.Count().Should().Be(0);
        }
    }
}

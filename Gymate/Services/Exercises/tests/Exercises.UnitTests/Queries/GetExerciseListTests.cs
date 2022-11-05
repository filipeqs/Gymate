using AutoMapper;
using Exercises.Domain.Models.Exercise;
using Exercises.Domain.Queries.GetExerciseList;
using Exercises.Infrastructure.Repositories;
using Exercises.UnitTests.Mocks;
using FluentAssertions;
using Moq;

namespace Exercises.UnitTests.Queries
{
    public class GetExerciseListTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IExerciseRepository> _exerciseRepositoryMock;

        public GetExerciseListTests()
        {
            _exerciseRepositoryMock = MockExerciseRepository.GetExerciseRepository();
            _mapper = MockMapper.GetMapper();
        }

        [Fact]
        public async Task Get_Exercise_List_Should_Return_List()
        {
            //Arrange
            var handler = new GetExerciseListHandler(_mapper, _exerciseRepositoryMock.Object);

            //Act
            var result = await handler.Handle(new GetExerciseListQuery(), CancellationToken.None);

            //Assert
            result.Should().BeOfType<List<ExerciseDetailsDto>>();
            result.Count().Should().Be(2);
        }
    }
}

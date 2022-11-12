using AutoFixture;
using AutoFixture.Xunit2;
using Exercises.Core.Entities;
using Exercises.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Exercises.FunctionalTests.Repository
{
    public class ExerciseRepositoryTests : ExercisesTestBase
    {
        private readonly IFixture _fixture;

        public ExerciseRepositoryTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Get_All_Exercises_Should_Return_List()
        {
            //Arrange
            using var server = CreateServer();
            var exerciseRepository = (IExerciseRepository)server.Services.GetService(typeof(IExerciseRepository));

            //Act
            var exercises = await exerciseRepository.GetAllExercisesAsync();

            //Assert
            exercises.Should().NotBeNull();
            exercises.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Get_Exercise_By_Id_Should_Return_Exercise()
        {
            //Arrange
            using var server = CreateServer();
            var exerciseRepository = (IExerciseRepository)server.Services.GetService(typeof(IExerciseRepository));

            //Act
            var exercise = await exerciseRepository.GetExerciseByIdAsync(1);

            //Assert
            exercise.Should().NotBeNull();
            exercise.Name.Should().Be("Squat");
        }

        [Fact]
        public async Task Get_Non_Existing_Exercise_By_Id_Should_Return_Null()
        {
            //Arrange
            using var server = CreateServer();
            var exerciseRepository = (IExerciseRepository)server.Services.GetService(typeof(IExerciseRepository));

            //Act
            var exercise = await exerciseRepository.GetExerciseByIdAsync(99);

            //Assert
            exercise.Should().BeNull();
        }

        [Fact]
        public async Task Get_Exercise_By_Name_Should_Return_Exercise()
        {
            //Arrange
            using var server = CreateServer();
            var exerciseRepository = (IExerciseRepository)server.Services.GetService(typeof(IExerciseRepository));

            //Act
            var exercises = await exerciseRepository.GetExercisesByNameAsync("Squat");

            //Assert
            exercises.Should().NotBeNull();
            exercises.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Get_Non_Existing_Exercise_By_Name_Should_Return_Null()
        {
            //Arrange
            using var server = CreateServer();
            var exerciseRepository = (IExerciseRepository)server.Services.GetService(typeof(IExerciseRepository));

            //Act
            var exercises = await exerciseRepository.GetExercisesByNameAsync("Not Available");

            //Assert
            exercises.Count().Should().Be(0);
        }

        [Fact]
        public async Task Create_Exercise_Should_Add_To_Exercises()
        {
            //Arrange
            using var server = CreateServer();
            var exerciseRepository = (IExerciseRepository)server.Services.GetService(typeof(IExerciseRepository));
            var currentExercises = await exerciseRepository.GetAllExercisesAsync();
            var exercise = _fixture.Build<Exercise>().Without(q => q.Id).Create();

            //Act
            await exerciseRepository.CreateExerciseAsync(exercise);
            var result = await exerciseRepository.SaveAsync();
            var updatedExercises = await exerciseRepository.GetAllExercisesAsync();

            //Assert
            result.Should().BeTrue();
            updatedExercises.Count().Should().Be(currentExercises.Count() + 1);
        }

        [Fact]
        public async Task Upadate_Exercise_Should_Success()
        {
            //Arrange
            using var server = CreateServer();
            var exerciseRepository = (IExerciseRepository)server.Services.GetService(typeof(IExerciseRepository));
            var exercise = new Exercise { Id = 1, Name = "Updated" };

            //Act
            exerciseRepository.UpdateExercise(exercise);
            var result = await exerciseRepository.SaveAsync();

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Upadate_Non_Existing_Exercise_Should_Throw_Exception()
        {
            //Arrange
            using var server = CreateServer();
            var exerciseRepository = (IExerciseRepository)server.Services.GetService(typeof(IExerciseRepository));
            var exercise = new Exercise{ Id = 99, Name = "Not Available" };

            //Act
            exerciseRepository.UpdateExercise(exercise);
            Func<Task> result = () => exerciseRepository.SaveAsync();

            //Assert
            await result.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

        [Fact]
        public async Task Remove_Exercise_Should_Remove_Exercise_From_List()
        {
            //Arrange
            using var server = CreateServer();
            var exerciseRepository = (IExerciseRepository)server.Services.GetService(typeof(IExerciseRepository));
            var currentExercises = await exerciseRepository.GetAllExercisesAsync();
            var exerciseToDelte = currentExercises.ToList()[0];

            //Act
            exerciseRepository.DeleteExercise(exerciseToDelte);
            var result = await exerciseRepository.SaveAsync();
            var updatedExercises = await exerciseRepository.GetAllExercisesAsync();

            //Assert
            result.Should().BeTrue();
            updatedExercises.Count().Should().Be(currentExercises.Count() - 1);
            updatedExercises.FirstOrDefault(q => q.Name == exerciseToDelte.Name).Should().BeNull();
        }

        [Fact]
        public async Task Remove_Unexisting_Exercise_Should_Throw_Exception()
        {
            //Arrange
            using var server = CreateServer();
            var exerciseRepository = (IExerciseRepository)server.Services.GetService(typeof(IExerciseRepository));
            var exerciseToDelte = new Exercise { Id = 99, Name = "Not Available"};

            //Act
            exerciseRepository.DeleteExercise(exerciseToDelte);
            Func<Task> result = () => exerciseRepository.SaveAsync();

            //Assert
            await result.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }
    }
}

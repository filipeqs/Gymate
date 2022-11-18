using Exercises.Core.Entities;
using Exercises.Domain.Events;

namespace Exercises.FunctionalTests.Events
{
    public class FakeExerciseUpdateEventPublisher : IExerciseUpdateEventPublisher
    {
        public Task Publish(Exercise exercise)
        {
            return Task.CompletedTask;
        }
    }
}

using Exercises.Core.Entities;

namespace Exercises.Domain.Events
{
    public interface IExerciseUpdateEventPublisher
    {
        Task Publish(Exercise exercise);
    }
}

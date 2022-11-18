using EventBus.Messages.Events;
using Exercises.Core.Entities;
using MassTransit;

namespace Exercises.Domain.Events
{
    public class ExerciseUpdateEventPublisher : IExerciseUpdateEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ExerciseUpdateEventPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Publish(Exercise exercise)
        {
            var eventMessage = new ExerciseUpdateEvent
            {
                ExerciseId = exercise.Id,
                ExerciseName = exercise.Name,
            };
            await _publishEndpoint.Publish(eventMessage);
        }
    }
}

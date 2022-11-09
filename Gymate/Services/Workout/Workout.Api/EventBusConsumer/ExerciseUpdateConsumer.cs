using EventBus.Messages.Events;
using MassTransit;

namespace Workout.Api.EventBusConsumer
{
    public class ExerciseUpdateConsumer : IConsumer<ExerciseUpdateEvent>
    {
        public Task Consume(ConsumeContext<ExerciseUpdateEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}

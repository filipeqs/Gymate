using EventBus.Messages.Events;
using MassTransit;

namespace Workouts.Api.EventBusConsumer
{
    public class ExerciseUpdateConsumer : IConsumer<ExerciseUpdateEvent>
    {
        public Task Consume(ConsumeContext<ExerciseUpdateEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}

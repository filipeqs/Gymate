namespace EventBus.Messages.Events;

public class ExerciseUpdateEvent : IntegrationBaseEvent
{
    public int ExerciseId { get; set; }
    public string ExerciseName { get; set; }
}

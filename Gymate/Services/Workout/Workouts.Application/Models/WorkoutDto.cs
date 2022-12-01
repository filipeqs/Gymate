namespace Workouts.Application.Models;

public sealed class WorkoutDto
{
    public int Id { get; set; }
    public string Title { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

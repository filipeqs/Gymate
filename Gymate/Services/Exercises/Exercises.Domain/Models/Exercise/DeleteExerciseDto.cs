namespace Exercises.Domain.Models.Exercise
{
    public class DeleteExerciseDto
    {
        public DeleteExerciseDto(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}

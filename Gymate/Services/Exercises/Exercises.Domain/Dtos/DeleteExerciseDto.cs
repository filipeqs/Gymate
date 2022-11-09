namespace Exercises.Domain.Dtos
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

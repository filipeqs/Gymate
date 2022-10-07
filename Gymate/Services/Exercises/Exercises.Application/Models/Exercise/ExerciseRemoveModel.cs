namespace Exercises.Application.Models.Exercise
{
    public class ExerciseRemoveModel : ExerciseBaseModel
    {
        public ExerciseRemoveModel(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}

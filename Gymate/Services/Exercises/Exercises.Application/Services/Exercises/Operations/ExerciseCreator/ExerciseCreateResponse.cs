using Exercises.Application.Models.Exercise;

namespace Exercises.Application.Services.Exercises.Operations.ExerciseCreator
{
    public class ExerciseCreateResponse : BaseExerciseResponseModel
    {
        public ExerciseCreateResponse()
        {
            IsSuccess = false;
        }

        public ExerciseDetailsModel ExerciseDetailsModel { get; set; }
    }
}

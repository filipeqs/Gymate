using Exercises.Application.Models.Exercise;

namespace Exercises.Application.Services.Exercises.Operations.ExerciseUpdator
{
    public class ExerciseUpdateResponse : BaseExerciseResponseModel
    {
        public ExerciseUpdateResponse()
        {
            IsSuccess = false;
        }

        public int ErrorStatusCode  { get; set; }
    }
}

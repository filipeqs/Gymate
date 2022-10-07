using Exercises.Application.Models.Exercise;

namespace Exercises.Application.Services.Exercises.Operations.ExerciseRemover
{
    public class ExerciseRemoveResponse : BaseExerciseResponseModel
    {
        public ExerciseRemoveResponse()
        {
            IsSuccess = false;
        }

        public int ErrorStatusCode { get; set; }
    }
}

using Exercises.Application.Models.Exercise;
using Exercises.Application.Services.Exercises.Operations;

namespace Exercises.Application.Services.Exercises.Operations.ExerciseCreator
{
    public interface IExerciseCreator : IExerciseOperator<ExerciseCreateModel, ExerciseCreateResponse>
    {
    }
}

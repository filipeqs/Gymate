using Exercises.Domain.Models.Exercise;
using MediatR;

namespace Exercises.Domain.Queries.GetExerciseList
{
    public class GetExerciseListQuery : IRequest<IEnumerable<ExerciseDetailsDto>>
    {
    }
}

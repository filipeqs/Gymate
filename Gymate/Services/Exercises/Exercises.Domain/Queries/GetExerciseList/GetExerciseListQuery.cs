using Exercises.Domain.Dtos;
using MediatR;

namespace Exercises.Domain.Queries.GetExerciseList
{
    public class GetExerciseListQuery : IRequest<IEnumerable<ExerciseDetailsDto>>
    {
    }
}

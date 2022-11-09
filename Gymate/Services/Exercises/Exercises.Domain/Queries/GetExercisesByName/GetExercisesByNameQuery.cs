using Exercises.Domain.Dtos;
using MediatR;

namespace Exercises.Domain.Queries.GetExercisesByName
{
    public class GetExercisesByNameQuery : IRequest<IEnumerable<ExerciseDetailsDto>>
    {
        public GetExercisesByNameQuery(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}

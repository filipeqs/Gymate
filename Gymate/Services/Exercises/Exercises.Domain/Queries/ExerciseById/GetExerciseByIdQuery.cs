using Exercises.Domain.Models.Exercise;
using MediatR;

namespace Exercises.Domain.Queries.ExerciseById
{
    public class GetExerciseByIdQuery : IRequest<ExerciseDetailsDto>
    {
        public GetExerciseByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}

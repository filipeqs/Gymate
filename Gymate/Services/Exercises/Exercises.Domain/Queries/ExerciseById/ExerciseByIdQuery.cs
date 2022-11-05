using Exercises.Domain.Models.Exercise;
using MediatR;

namespace Exercises.Domain.Queries.ExerciseById
{
    public class ExerciseByIdQuery : IRequest<ExerciseDetailsDto>
    {
        public ExerciseByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}

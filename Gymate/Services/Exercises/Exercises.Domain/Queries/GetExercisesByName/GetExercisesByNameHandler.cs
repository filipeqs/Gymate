using AutoMapper;
using Exercises.Domain.Models.Exercise;
using Exercises.Infrastructure.Repositories;
using MediatR;

namespace Exercises.Domain.Queries.GetExercisesByName
{
    public class GetExercisesByNameHandler : IRequestHandler<GetExercisesByNameQuery, IEnumerable<ExerciseDetailsDto>>
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _repository;

        public GetExercisesByNameHandler(IMapper mapper, IExerciseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<ExerciseDetailsDto>> Handle(GetExercisesByNameQuery request, CancellationToken cancellationToken)
        {
            var exercises = await _repository.GetExercisesByNameAsync(request.Name);
            return _mapper.Map<IEnumerable<ExerciseDetailsDto>>(exercises);
        }
    }
}

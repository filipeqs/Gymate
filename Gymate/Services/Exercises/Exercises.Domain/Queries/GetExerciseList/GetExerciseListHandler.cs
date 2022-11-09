using AutoMapper;
using Exercises.Domain.Dtos;
using Exercises.Infrastructure.Repositories;
using MediatR;

namespace Exercises.Domain.Queries.GetExerciseList
{
    public class GetExerciseListHandler : IRequestHandler<GetExerciseListQuery, IEnumerable<ExerciseDetailsDto>>
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _repository;

        public GetExerciseListHandler(IMapper mapper, IExerciseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<ExerciseDetailsDto>> Handle(GetExerciseListQuery request, CancellationToken cancellationToken)
        {
            var exercises = await _repository.GetAllExercisesAsync();
            return _mapper.Map<IEnumerable<ExerciseDetailsDto>>(exercises);
        }
    }
}

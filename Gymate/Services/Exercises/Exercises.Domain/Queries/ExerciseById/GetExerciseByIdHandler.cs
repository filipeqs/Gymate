using AutoMapper;
using Exercises.Domain.Dtos;
using Exercises.Infrastructure.Repositories;
using MediatR;

namespace Exercises.Domain.Queries.ExerciseById
{
    public class GetExerciseByIdHandler : IRequestHandler<GetExerciseByIdQuery, ExerciseDetailsDto>
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _repository;

        public GetExerciseByIdHandler(IMapper mapper, IExerciseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ExerciseDetailsDto> Handle(GetExerciseByIdQuery request, CancellationToken cancellationToken)
        {
            var exercise = await _repository.GetExerciseByIdAsync(request.Id);
            return _mapper.Map<ExerciseDetailsDto>(exercise);
        }
    }
}

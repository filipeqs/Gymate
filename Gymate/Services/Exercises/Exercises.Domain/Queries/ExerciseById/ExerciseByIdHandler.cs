using AutoMapper;
using Exercises.Domain.Models.Exercise;
using Exercises.Infrastructure.Repositories;
using MediatR;

namespace Exercises.Domain.Queries.ExerciseById
{
    public class ExerciseByIdHandler : IRequestHandler<ExerciseByIdQuery, ExerciseDetailsDto>
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _repository;

        public ExerciseByIdHandler(IMapper mapper, IExerciseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ExerciseDetailsDto> Handle(ExerciseByIdQuery request, CancellationToken cancellationToken)
        {
            var exercise = await _repository.GetExerciseByIdAsync(request.Id);
            return _mapper.Map<ExerciseDetailsDto>(exercise);
        }
    }
}

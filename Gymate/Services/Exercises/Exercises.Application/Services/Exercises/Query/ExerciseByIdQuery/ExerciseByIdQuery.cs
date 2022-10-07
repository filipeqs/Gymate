using AutoMapper;
using Exercises.Application.Models.Exercise;
using Exercises.Infrastructure.Repositories;

namespace Exercises.Application.Services.Exercises.Query.ExerciseByIdQuery
{
    public class ExerciseByIdQuery : IExerciseByIdQuery
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _repository;

        public ExerciseByIdQuery(IMapper mapper, IExerciseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }


        public async Task<ExerciseDetailsModel?> RunAsync(int id)
        {
            var exercise = await _repository.GetExerciseByIdAsync(id);
            return _mapper.Map<ExerciseDetailsModel?>(exercise);
        }
    }
}

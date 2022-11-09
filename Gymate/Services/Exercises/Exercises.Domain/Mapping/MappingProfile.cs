using AutoMapper;
using EventBus.Messages.Events;
using Exercises.Core.Entities;
using Exercises.Domain.Dtos;

namespace Exercises.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Exercise, ExerciseDetailsDto>().ReverseMap();
            CreateMap<Exercise, CreateExerciseDto>().ReverseMap();
            CreateMap<Exercise, UpdateExerciseDto>().ReverseMap();
        }
    }
}

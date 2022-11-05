using AutoMapper;
using Exercises.Core.Entities;
using Exercises.Domain.Commands.CreateExercise;
using Exercises.Domain.Models.Exercise;

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

using AutoMapper;
using Exercises.Application.Models.Exercise;
using Exercises.Domain.Entities;

namespace Exercises.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Exercise, ExerciseDetailsModel>().ReverseMap();
            CreateMap<Exercise, ExerciseCreateModel>().ReverseMap();
            CreateMap<Exercise, ExerciseUpdateModel>().ReverseMap();
        }
    }
}

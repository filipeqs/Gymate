using AutoMapper;
using Exercises.Domain.Mapping;

namespace Exercises.UnitTests.Mocks
{
    public static class MockMapper
    {
        public static IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            return mapperConfig.CreateMapper();
        }
    }
}

using Gymate.Aggregator.Config;
using Gymate.Aggregator.Extensions;
using Gymate.Aggregator.Interfaces;
using Gymate.Aggregator.Models;

namespace Gymate.Aggregator.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly HttpClient _client;

        public ExerciseService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<ExerciseModel>> GetExercises()
        {
            var response = await _client.GetAsync(ExerciseOperations.GetExercises());
            return await response.ReadContentAs<List<ExerciseModel>>();
        }
    }
}

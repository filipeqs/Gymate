using Exercises.Domain.Extensions;
using System.Net;

namespace Exercises.Domain.Models
{
    public abstract class BaseExerciseCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorStatusCode { get; set; }

        public void BuildNotFoundErrorResponse(int exerciseId)
        {
            ErrorMessage = $"Exercise with id {exerciseId} not found.";
            ErrorStatusCode = HttpStatusCode.NotFound.ToInt();
        }

        public void BuildBadRequestErrorResponse(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ErrorStatusCode = HttpStatusCode.BadRequest.ToInt();
        }
    }
}

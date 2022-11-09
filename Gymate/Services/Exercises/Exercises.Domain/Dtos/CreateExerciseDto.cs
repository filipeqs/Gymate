using System.ComponentModel.DataAnnotations;

namespace Exercises.Domain.Dtos
{
    public class CreateExerciseDto
    {
        [Required]
        public string Name { get; set; }
    }
}

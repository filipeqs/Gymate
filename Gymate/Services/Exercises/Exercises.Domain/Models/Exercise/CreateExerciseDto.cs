using System.ComponentModel.DataAnnotations;

namespace Exercises.Domain.Models.Exercise
{
    public class CreateExerciseDto
    {
        [Required]
        public string Name { get; set; }
    }
}

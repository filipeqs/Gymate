using System.ComponentModel.DataAnnotations;

namespace Exercises.Domain.Dtos
{
    public class UpdateExerciseDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

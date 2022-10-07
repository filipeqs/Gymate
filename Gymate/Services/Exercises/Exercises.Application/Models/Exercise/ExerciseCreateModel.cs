using System.ComponentModel.DataAnnotations;

namespace Exercises.Application.Models.Exercise
{
    public class ExerciseCreateModel : ExerciseBaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}

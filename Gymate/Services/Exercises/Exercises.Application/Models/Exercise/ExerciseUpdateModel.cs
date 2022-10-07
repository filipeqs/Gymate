using System.ComponentModel.DataAnnotations;

namespace Exercises.Application.Models.Exercise
{
    public class ExerciseUpdateModel : ExerciseBaseModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

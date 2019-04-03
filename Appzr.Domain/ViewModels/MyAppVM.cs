using System.ComponentModel.DataAnnotations;

namespace Appzr.Domain.ViewModels
{
    public sealed class MyAppVM
    {
        [Required]
        [MinLength(3), MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(10), MaxLength(1000)]
        public string Link { get; set; }
    }
}

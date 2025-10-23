using System.ComponentModel.DataAnnotations;

namespace _57361.Models
{
    public class Speaker
    {
        [Key]
        public int SpeakerId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Tiêu đề không được vượt quá 100 ký tự")]
        public string Title { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Tiểu sử không được vượt quá 1000 ký tự")]
        public string Bio { get; set; }

        public List<Presentation>? Presentations { get; set; }
    }
}

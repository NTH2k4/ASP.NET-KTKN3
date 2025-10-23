using System.ComponentModel.DataAnnotations;

namespace _57361.Models
{
    public class Presentation
    {
        [Key]
        public int PresentationId { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Chủ đề không được vượt quá 200 ký tự")]
        public string Topic { get; set; }

        [Required]
        [Range(1, 480, ErrorMessage = "Thời lượng phải từ 1 đến 480 phút")]
        public int Duration { get; set; } 

        [StringLength(500, ErrorMessage = "Liên kết slide không được vượt quá 500 ký tự")]
        public string Slides { get; set; }

        public int SpeakerId { get; set; }
        public Speaker? Speaker { get; set; }
    }
}

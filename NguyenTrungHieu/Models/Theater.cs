using System.ComponentModel.DataAnnotations;

namespace NguyenTrungHieu.Models
{
    public class Theater
    {
        [Key]
        public int TheaterId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Tên không được vượt qua 50 ký tự")]
        public string Name { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Thể loại không được vượt quá 50 ký tự")]
        public string Style { get; set; }

        public List<Performance>? performances { get; set; } 
    }
}

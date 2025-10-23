using System.ComponentModel.DataAnnotations;

namespace NguyenTrungHieu.Models
{
    public class Performance
    {
        [Key]
        public int PerformanceId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Tiêu đề không được vượt quá 100 ký tự")]
        public string Title { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double TicketPrice { get; set; }

        public int TheaterId { get; set; }
        public Theater? theater { get; set; }
    }
}

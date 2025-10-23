using System.ComponentModel.DataAnnotations;

namespace _57362.Models
{
    public class Exhibition
    {
        //Exhibition(ExhibitionId, Title, StartDate, EndDate)
        public int ExhibitionId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Tiêu đề không được vượt quá 100 ký tự")]
        public string Title { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int CuratorId { get; set; }
        public Curator? Curator { get; set; }
    }
}

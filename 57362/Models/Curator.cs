using System.ComponentModel.DataAnnotations;

namespace _57362.Models
{
    public class Curator
    {
        // Curator (CuratorId, Name, Museum, Specialization)
        [Key]
        public int CuratorId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Tên khồn được vượt quá 100 ký tự")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Tên bảo tàng không được vượt quá 100 ký tự")]
        public string Museum { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Chuyên môn không được vượt quá 100 ký tự")]
        public string Specialization { get; set; }

        public List<Exhibition>? Exhibitions { get; set; }
    }
}

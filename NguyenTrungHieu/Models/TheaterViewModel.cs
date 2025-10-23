using System.ComponentModel.DataAnnotations;

namespace NguyenTrungHieu.Models
{
    public class TheaterViewModel
    {
        public int TheaterId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Style { get; set; }
    }
}

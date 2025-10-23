namespace NguyenTrungHieu.Models
{
    public class PerformanceViewModel
    {
        public int PerformanceId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public double TicketPrice { get; set; }
        public Theater? theater { get; set; }
    }
}

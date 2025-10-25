namespace _57361.Models
{
    public class PresentationViewModel
    {
        public int PresentationId { get; set; }
        public string Topic { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string? Slides { get; set; }
        public string? SpeakerName { get; set; }
    }
}

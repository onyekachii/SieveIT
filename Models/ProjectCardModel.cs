
namespace SeiveIT.Models
{
    public class ProjectCardModel
    {
        public long Id { get; set; }
        public string? Title { get; set; } = "Project Title";
        public DateTime? Date { get; set; }
        public int OutcropNumber { get; set; }
    }
}

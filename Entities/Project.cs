using SQLite;

namespace SeiveIT.Entities
{
    [Table("Project")]
    public class Project : BaseEntity
    {

        [Unique]
        public string? Title { get; set; }

        [Column("Number of Outcrop")]
        public int OutcropOfNumber { get; set; }
        public DateTime? Date { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Altitude { get; set; }

        [MaxLength(250)]
        public string? Notes { get; set; }
    }

}

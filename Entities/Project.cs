using SQLite;

namespace SeiveIT.Entities
{
    [Table("Project")]
    public class Project : BaseEntity
    {

        [Unique]
        [NotNull]
        public string? Title { get; set; }
        public DateTime? Date { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        [MaxLength(250)]
        public string? Notes { get; set; }
    }

}

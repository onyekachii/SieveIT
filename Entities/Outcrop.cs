
using SQLite;

namespace SeiveIT.Entities
{
    [Table("Outcrop")]
    public class Outcrop : BaseEntity
    {        
        public DateTime? Date { get; set; }
        public string? Basin { get; set; }
        public string? Formation { get; set; }
        public long ProjectId { get; set; }

        [Ignore] // SQLite-Net does not support navigation properties directly
        public Project? Project { get; set; }
    }
}

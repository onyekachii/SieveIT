
using SQLite;

namespace SeiveIT.Entities
{
    [Table("Seive")]
    public class SeiveData : BaseEntity
    {
        [Column("Phi Size")]
        public float PhiScale { get; set; }
        public float Weight { get; set; }
        public long OutcropId { get; set; }
        public long ProjectId { get; set; }
        [Ignore] // SQLite-Net does not support navigation properties directly
        public Outcrop? Outcrop { get; set; }
    }
}

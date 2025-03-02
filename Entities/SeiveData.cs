
using SQLite;

namespace SeiveIT.Entities
{
    [Table("SeiveData")]
    public class SeiveData : BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        [Column("Phi Size")]
        public byte PhiScale { get; set; }
        public byte Weight { get; set; }
        public long OutcropId { get; set; }

        [Ignore] // SQLite-Net does not support navigation properties directly
        public Outcrop? Outcrop { get; set; }
    }
}

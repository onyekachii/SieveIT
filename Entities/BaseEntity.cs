using SQLite;

namespace SeiveIT.Entities
{
    public class BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        [Column("Created On")]
        public DateTime CreatedOn { get; set; }

        [Column("Updated On")]
        public DateTime UpdatedOn { get; set; }

        [Column("Deleted On")]
        public DateTime DeletedOn { get; set; }

        public bool Deleted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeiveIT.Entities
{
    public class BaseEntity
    {
        [Column("Created On")]
        public DateTime CreatedOn { get; set; }

        [Column("Updated On")]
        public DateTime UpdatedOn { get; set; }

        [Column("Deleted On")]
        public DateTime DeletedOn { get; set; }

        public bool Deleted { get; set; }
    }
}

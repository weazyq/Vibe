using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vibe.EF.Entities
{
    public abstract class BaseEntity
    {
        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("modifiedat")]
        public DateTime ModifiedAt { get; set; }

        [Column("isremoved")]
        public Boolean IsRemoved { get; set; }
    }
}

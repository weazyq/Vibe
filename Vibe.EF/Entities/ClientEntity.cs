using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vibe.EF.Entities
{
    public class ClientEntity : BaseEntity
    {
        [Column("id")]
        public Guid Id { get; set; }
        
        [Column("name")]
        public String Name { get; set; }

        [Column("phone")]
        public String Phone { get; set; }
    }
}

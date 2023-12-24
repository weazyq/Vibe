using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vibe.EF.Entities
{
    public class ScooterEntity
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        public String Name { get; set; }
        [Column("ip")]
        public String Ip { get; set; }
        [Column("port")]
        public String Port { get; set; }
    }
}

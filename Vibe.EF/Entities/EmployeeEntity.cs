using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vibe.EF.Entities
{
    public class EmployeeEntity : BaseEntity
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        public String Name { get; set; }
        [Column("phone")]
        public String Phone { get; set; }
        [Column("email")]
        public String Email { get; set; }
        [Column("role")]
        public Int32 Role { get; set; }
    }
}

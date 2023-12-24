using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vibe.EF.Entities
{
    public class UserEntity : BaseEntity
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("login")]
        public String Login { get; set; }
        [Column("password")]
        public String Password { get; set; }
        [Column("employeeid")]
        public Guid? EmployeeId { get; set; }
        [Column("clientid")]
        public Guid? ClientId { get; set; }
    }
}

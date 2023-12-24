using System.ComponentModel.DataAnnotations.Schema;

namespace Vibe.EF.Entities
{
    public class WorkzoneEntity : BaseEntity
    {
        [Column(TypeName = "id")]
        public Guid Id { get; set; }
        
        [Column(TypeName = "employeeid")]
        public Guid EmployeeId { get; set; }
        
        [Column(TypeName = "name")]
        public String Name { get; set; }
    }
}

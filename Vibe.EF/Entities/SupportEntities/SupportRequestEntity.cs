using System.ComponentModel.DataAnnotations.Schema;

namespace Vibe.EF.Entities.SupportEntities
{
    [Table("sup_supportrequests")]
    public class SupportRequestEntity
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public Guid ClientId { get; set; }
        public Guid? EmployeeId { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Boolean IsClosed { get; set; }
        public Boolean IsRemoved { get; set; }
    }
}

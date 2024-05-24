using System.ComponentModel.DataAnnotations.Schema;

namespace Vibe.EF.Entities.SupportEntities
{
    [Table("sup_supportmessages")]
    public class SupportMessageEntity
    {
        public Guid Id { get; set; }
        public String Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid SupportRequestId { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Boolean IsRemoved { get; set; }
    }
}

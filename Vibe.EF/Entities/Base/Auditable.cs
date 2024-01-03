
namespace Vibe.EF.Entities.Base
{
    public abstract class Auditable : IAuditable
    {
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Guid UpatedBy { get; set; }
    }
}

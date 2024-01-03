namespace Vibe.EF.Entities
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public Boolean IsRemoved { get; set; }
    }
}

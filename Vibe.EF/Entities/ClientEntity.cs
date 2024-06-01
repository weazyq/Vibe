namespace Vibe.EF.Entities
{
    public class ClientEntity
    {
        public Guid Id { get; set; }
        public required String Name { get; set; }
        public required String Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Boolean IsRemoved { get; set; }
    }
}

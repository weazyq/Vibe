namespace Vibe.EF.Entities
{
    public class ClientEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
    }
}

namespace Vibe.Domain.Clients
{
    public class Client
    {
        public Guid Id { get; }
        public String Name { get; }
        public String Phone { get; }
        public DateTime CreatedAt { get; }
        public Guid? CreatedBy { get; }
        public DateTime? ModifiedAt { get; }
        public Guid? UpdatedBy { get; }
        public Boolean IsRemoved { get; }

        public Client(Guid id, String name, String phone, DateTime createdAt, Guid? createdBy, DateTime? modifiedAt, Guid? updatedBy, Boolean isRemoved)
        {
            Id = id;
            Name = name;
            Phone = phone;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            ModifiedAt = modifiedAt;
            UpdatedBy = updatedBy;
            IsRemoved = isRemoved;
        }
    }
}

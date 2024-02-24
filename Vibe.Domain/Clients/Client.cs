namespace Vibe.Domain.Clients
{
    public class Client
    {
        public Guid Id;
        public String Name;
        public String Phone;
        public DateTime CreatedAt;
        public Guid? CreatedBy;
        public DateTime? ModifiedAt;
        public Guid? UpdatedBy;
        public Boolean IsRemoved;

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

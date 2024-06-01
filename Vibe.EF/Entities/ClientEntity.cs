using Vibe.Domain.Clients;
using Vibe.Domain.Employees;

namespace Vibe.EF.Entities
{
    public class ClientEntity
    {
        public Guid Id { get; set; }
        public required String Name { get; set; }
        public required String Phone { get; set; }
        public String? RefreshToken { get; set; } = String.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Boolean IsRemoved { get; set; }

        public void UpdateFromUser(Client client)
        {
            Id = client.Id;
            RefreshToken = client.RefreshToken;
            TokenCreated = client.TokenCreated;
            TokenExpires = client.TokenExpires;
        }
    }
}

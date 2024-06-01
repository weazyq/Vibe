using Vibe.Tools.Token;

namespace Vibe.Domain.Clients
{
    public class Client
    {
        public Guid Id { get; }
        public String Name { get; }
        public String Phone { get; }
        public String? RefreshToken { get; set; } = String.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
        public DateTime CreatedAt { get; }
        public DateTime? ModifiedAt { get; }
        public Boolean IsRemoved { get; }

        public Client(Guid id, String name, String phone, String? refreshToken, DateTime tokenCreated, DateTime tokenExpires, 
            DateTime createdAt, DateTime? modifiedAt, Boolean isRemoved)
        {
            Id = id;
            Name = name;
            Phone = phone;
            RefreshToken = refreshToken;
            TokenCreated = tokenCreated;
            TokenExpires = tokenExpires;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            IsRemoved = isRemoved;
        }

        public void SetRefreshToken(RefreshToken refreshToken)
        {
            RefreshToken = refreshToken.Token;
            TokenCreated = refreshToken.Created;
            TokenExpires = refreshToken.Expires;
        }
    }
}

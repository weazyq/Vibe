using Vibe.Domain.Users;

namespace Vibe.EF.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? ClientId { get; set; }
        public String? RefreshToken { get; set; } = String.Empty;
        public DateTime TokenCreated {  get; set; }
        public DateTime TokenExpires { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Boolean IsRemoved { get; set; }

        public void UpdateFromUser(User user)
        {
            Id = user.Id;
            EmployeeId = user.EmployeeId;
            ClientId = user.ClientId;
            RefreshToken = user.RefreshToken;
            TokenCreated = user.TokenCreated;
            TokenExpires = user.TokenExpires;
        }
    }
}

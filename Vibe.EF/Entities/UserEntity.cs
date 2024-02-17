using Vibe.Domain.Users;
using Vibe.EF.Entities.Base;

namespace Vibe.EF.Entities
{
    public class UserEntity : Auditable, IHaveId, IRemovable
    {
        public Guid Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? ClientId { get; set; }
        public String? RefreshToken { get; set; } = String.Empty;
        public DateTime TokenCreated {  get; set; }
        public DateTime TokenExpires { get; set; }
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

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
    }
}

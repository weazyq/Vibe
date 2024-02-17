using Vibe.Tools.Token;

namespace Vibe.Domain.Users
{
    public class User(Guid id, Guid? employeeId, Guid? clientId, String? refreshToken, DateTime tokenCreated, DateTime tokenExpires)
    {
        public Guid Id = id;
        public Guid? EmployeeId = employeeId;
        public Guid? ClientId = clientId;
        public String? RefreshToken = refreshToken;
        public DateTime TokenCreated = tokenCreated;
        public DateTime TokenExpires = tokenExpires;

        public void SetRefreshToken(RefreshToken refreshToken)
        {
            RefreshToken = refreshToken.Token;
            TokenCreated = refreshToken.Created;
            TokenExpires = refreshToken.Expires;
        }
    }
}

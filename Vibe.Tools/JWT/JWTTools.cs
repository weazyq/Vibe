using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Vibe.Tools.JWT
{
    public static class JWTTools
    {
        public static SymmetricSecurityKey FormSingingKey(String secretKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)); 
        }
    }
}

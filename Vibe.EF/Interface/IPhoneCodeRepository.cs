using Vibe.EF.Entities;
using Vibe.Tools.Result;

namespace Vibe.EF.Interface
{
    public interface IPhoneCodeRepository
    {
        Result SaveSms(PhoneCodeEntity entity);
        PhoneCodeEntity? GetSms(String phoneNumber);
    }
}

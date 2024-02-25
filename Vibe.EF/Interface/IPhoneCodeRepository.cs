using Vibe.EF.Entities;
using Vibe.Tools.Result;

namespace Vibe.EF.Interface
{
    public interface IPhoneCodeRepository
    {
        Result SaveSms(String phoneNumber, String code);
        PhoneCodeEntity? GetSms(String phoneNumber);
    }
}

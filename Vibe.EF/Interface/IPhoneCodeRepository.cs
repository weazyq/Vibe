using Vibe.Domain.Infrastructure;
using Vibe.Tools.Result;

namespace Vibe.EF.Interface
{
    public interface IPhoneCodeRepository
    {
        Result SaveSms(String phoneNumber, String code);
        PhoneCode? GetSms(String phoneNumber);
    }
}

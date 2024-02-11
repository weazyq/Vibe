using Vibe.Domain.Clients;
using Vibe.Tools.Result;

namespace Vibe.Services.Clients.Interface
{
    public interface IClientService
    {
        Result SendSms(String phoneNumber);
        Result CheckSms(ClientBlank blank, String code);
    }
}

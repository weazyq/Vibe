using Vibe.Domain.Clients;
using Vibe.Tools.Result;

namespace Vibe.Services.Clients.Interface
{
    public interface IClientService
    {
        Result<Guid> SaveClient(ClientBlank clientBlank);
        Boolean CheckIsPhoneNumberExist(String phoneNumber);
        Client? GetClient(Guid clientId);
        Client? GetClientByPhoneNumber(String phoneNumber);
        Client GetClientByUser(Guid userId);
        Result SendSms(String phoneNumber);
        Result CheckSms(ClientBlank blank, String code);
    }
}

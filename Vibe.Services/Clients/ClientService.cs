using Vibe.Domain.Clients;
using Vibe.EF.Entities;
using Vibe.EF.Interface;
using Vibe.Services.Clients.Interface;
using Vibe.Tools.Result;

namespace Vibe.Services.Clients
{
    public class ClientService : IClientService
    {
        private readonly IPhoneCodeRepository _phoneCodeRepository;
        private readonly IClientRepository _clientRepository;

        public ClientService(IPhoneCodeRepository phoneCodeRepository, IClientRepository clientRepository)
        {
            _phoneCodeRepository = phoneCodeRepository;
            _clientRepository = clientRepository;
        }
        public Result<Guid> SaveClient(ClientBlank clientBlank)
        {
            if (clientBlank.Name == null) return Result.Fail("Укажите имя");
            if (clientBlank.Phone == null) return Result.Fail("Укажите номер телефона");

            return _clientRepository.SaveClient(clientBlank);
        }

        public Client? GetClient(Guid clientId)
        {
            return _clientRepository.GetClient(clientId);
        }

        public Result SendSms(String phoneNumber)
        {
            PhoneCodeEntity entity = new PhoneCodeEntity
            {
                Code = "0000",
                Phone = phoneNumber
            };

            return _phoneCodeRepository.SaveSms(entity);
        }

        public Result CheckSms(ClientBlank clientBlank, String code)
        {
            if (clientBlank.Name == null) return Result.Fail("Укажите имя");
            if (clientBlank.Phone == null) return Result.Fail("Укажите номер телефона");

            PhoneCodeEntity? phoneCode = _phoneCodeRepository.GetSms(clientBlank.Phone);
            if (phoneCode is null) return Result.Fail("Проверьте ввод номера телефона");
            if (!code.Equals(phoneCode.Code)) return Result.Fail("Введённый тобой код не совпадает с отправленным");

            return Result.Success;
        }

    }
}

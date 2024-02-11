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
        private readonly IUserRepository _userRepository;

        public ClientService(IPhoneCodeRepository phoneCodeRepository, IUserRepository userRepository, IClientRepository clientRepository)
        {
            _phoneCodeRepository = phoneCodeRepository;
            _userRepository = userRepository;
            _clientRepository = clientRepository;
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
            PhoneCodeEntity? phoneCode = _phoneCodeRepository.GetSms(clientBlank.Phone);
            if (phoneCode is null) return Result.Fail("Проверьте ввод номера телефона");
            if (!code.Equals(phoneCode.Code)) return Result.Fail("Введённый тобой код не совпадает с отправленным");

            Result clientRegisterResult = ClientRegister(clientBlank);
            if (!clientRegisterResult.IsSuccess) return Result.Fail("К сожалению не удалось создать твой аккаунт. Разработчики скоро всё починят.");

            return Result.Success;
        }

        private Result ClientRegister(ClientBlank clientBlank)
        {
            if (clientBlank.Name == null) return Result.Fail("Укажите имя");
            if (clientBlank.Phone == null) return Result.Fail("Укажите номер телефона");

            DataResult<Guid> saveClientResult = _clientRepository.SaveClient(clientBlank);
            if (!saveClientResult.IsSuccess) return Result.Fail("Не удалось сохранить клиента");
            
            Guid clientId = saveClientResult.Data;
            _userRepository.SaveUserByClient(clientId);

            return Result.Success;
        }
    }
}

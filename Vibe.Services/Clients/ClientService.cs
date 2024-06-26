﻿using Vibe.Domain.Clients;
using Vibe.Domain.Infrastructure;
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

        public Boolean CheckIsPhoneNumberExist(String phoneNumber)
        {
            Client? client = _clientRepository.GetClientByPhoneNumber(phoneNumber);

            return client is not null;
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

        public Client? GetClientByPhoneNumber(String phoneNumber) 
        {
            return _clientRepository.GetClientByPhoneNumber(phoneNumber);
        }

        public Client? GetClientByRefreshToken(String refreshToken)
        {
            return _clientRepository.GetClientByRefreshToken(refreshToken);
        }

        public Result SendSms(String phoneNumber)
        {
            PhoneCode? phoneCode = _phoneCodeRepository.GetSms(phoneNumber);
            if(phoneCode is not null)
            {
                Boolean isPhoneCodeExpired = DateTime.UtcNow > phoneCode?.CreatedAt.AddMinutes(phoneCode.ValidityMinutes);
                if(!isPhoneCodeExpired) return Result.Fail("На данный номер телефона уже отправлен код", "phoneCodeAlreadyExist");
            }

            String code = GenerateCode();
            return _phoneCodeRepository.SaveSms(phoneNumber, code);
        }

        public Result CheckSms(ClientBlank clientBlank, String code)
        {
            if (clientBlank.Phone == null) return Result.Fail("Укажите номер телефона");

            PhoneCode? phoneCode = _phoneCodeRepository.GetSms(clientBlank.Phone);
            if (phoneCode is null) return Result.Fail("Проверьте ввод номера телефона");
            if (!code.Equals(phoneCode.Code)) return Result.Fail("Введённый тобой код не совпадает с отправленным");

            return Result.Success;
        }

        public String GenerateCode()
        {
            Random rnd = new();

            String code = "";
            Int32 codeLength = 4;
            for (int i = 0; i < codeLength; i++)
            {
                Int32 number = rnd.Next(0, 9);
                code += number.ToString();
            }

            return code;
        }

        public Result UpdateClient(Client client)
        {
            return _clientRepository.UpdateClient(client);
        }
    }
}

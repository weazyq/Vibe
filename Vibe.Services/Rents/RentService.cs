using Vibe.Domain.Clients;
using Vibe.Domain.Rents;
using Vibe.Domain.Scooter;
using Vibe.EF.Interface;
using Vibe.Services.Clients.Interface;
using Vibe.Services.Rents.Interface;
using Vibe.Services.Scooters.Interface;
using Vibe.Tools.Result;

namespace Vibe.Services.Rents
{
    public class RentService : IRentService
    {
        private readonly IScootersService _scootersService;
        private readonly IClientService _clientService;
        private readonly IRentRepository _rentRepository;

        public RentService(IScootersService scootersService, IClientService clientService, IRentRepository rentRepository)
        { 
            _scootersService = scootersService;
            _clientService = clientService;
            _rentRepository = rentRepository;
        }

        public async Task<Result<Rent>> Initialize(Guid scooterId, Guid clientId)
        {
            Client? client = _clientService.GetClient(clientId);
            if (client is null) return Result.Fail("Клиент не найден в системе");

            Result scooterAvailabilityResult = await _scootersService.CheckScooterAvailability(scooterId);
            if (scooterAvailabilityResult.IsFail) return Result.Fail("Самокат не найден в системе");

            Guid rentId = _rentRepository.Save(client.Id, scooterId);
            Rent? rent = _rentRepository.GetRent(rentId);
            if (rent is null) return Result.Fail("Произошла ошибка при инициализации аренды");

            return rent;
        }

        public async Task<Result> EndRent(Guid rentId)
        {
            Rent? rent = GetRent(rentId);
            if (rent is null) return Result.Fail("Указанной аренды не существует");
            rent.EndRent();
            
            Result endRentResult = await _scootersService.EndRent(rent.ScooterId);
            if (endRentResult.IsFail) throw new Exception("Самокат отказал в завершении аренды");

            return _rentRepository.UpdateRent(rent);
        } 

        public Rent? GetRent(Guid rentId)
        {
            return _rentRepository.GetRent(rentId);
        }

        public Rent? GetRentByClient(Guid clientId)
        {
            return _rentRepository.GetRentByClient(clientId);
        }

        public Rent? GetActiveUserRent(Guid userId)
        {
            Client? client = _clientService.GetClientByUser(userId);
            if (client == null) return null;

            return _rentRepository.GetActiveRent(client.Id);
        }

        public Rent[] GetRentHistory(Guid userId)
        {
            Client? client = _clientService.GetClientByUser(userId);
            if (client == null) return [];

            return _rentRepository.GetRentHistory(client.Id);
        }
    }
}

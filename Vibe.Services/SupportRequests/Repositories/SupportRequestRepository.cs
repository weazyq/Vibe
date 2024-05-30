using Vibe.Domain.SupportRequests;
using Vibe.Services.SupportRequests.Converters;
using Vibe.EF;
using Vibe.EF.Entities.SupportEntities;
using Vibe.EF.Interface;
using Vibe.Tools.Result;
using Vibe.Domain.SupportRequests.SupportMessages;
using Vibe.Domain.Employees;
using Vibe.Services.Employees.Converters;
using Vibe.Services.Clients.Converters;
using Vibe.Domain.Clients;

namespace Vibe.Services.SupportRequests.Repositories
{
    public class SupportRequestRepository : ISupportRequestRepository
    {
        private DataContext _context { get; init; }

        public SupportRequestRepository(DataContext context)
        {
            _context = context;
        }

        public Result SaveSupportRequest(SupportRequestBlank blank)
        {
            SupportRequestEntity entity = new()
            {
                Id = blank.Id,
                Title = blank.Title,
                Description = blank.Description,
                ClientId = blank.ClientId,
                EmployeeId = blank.EmployeeId,
                OpenedAt = blank.OpenedAt,
                CreatedAt = DateTime.UtcNow,
                IsClosed = false,
            };

            try
            {
                _context.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return Result.Fail("Не удалось сохранить обращение в тех.поддержку");
            }

            return Result.Success;
        }

        public Result<Guid> SaveSupportMessage(SupportMessageBlank blank)
        {
            SupportMessageEntity message = new()
            {
                Id = blank.Id,
                SupportRequestId = blank.SupportRequestId,
                Text = blank.Text,
                CreatedBy = blank.CreatedBy,
                CreatedAt = DateTime.UtcNow,
            };

            try
            {
                _context.SupportMessages.Add(message);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return Result.Fail("Не удалось сохранить сообщение");
            }

            return message.Id;
        }

        public SupportRequestDetail? GetSupportRequestDetail(Guid id)
        {
            SupportRequestEntity? entity = _context.SupportRequests.FirstOrDefault(r => r.Id == id);
            if (entity == null) return null;

            Employee? employee = _context.Employees.FirstOrDefault(e => e.Id == entity.EmployeeId)?.ToDomain();
            Client client = _context.Clients.First(cl => cl.Id == entity.ClientId).ToDomain();

            SupportMessage[] messages = _context.SupportMessages.Where(m => m.SupportRequestId == id).ToArray().ToDomain();
            return new SupportRequestDetail(entity.Id, entity.Title, entity.Description, client, employee, entity.OpenedAt, entity.IsClosed, messages);
        }

        public SupportMessage? GetSupportMessage(Guid id)
        {
            SupportMessage? entity = _context.SupportMessages.FirstOrDefault(m => m.Id == id)?.ToDomain();
            return entity;
        }

        public SupportRequest[] GetSupportRequests(Guid clientId)
        {
            return _context.SupportRequests.Where(sr => sr.ClientId == clientId).ToDomain();
        }

        public SupportRequest[] ListSupportRequestsForEmployee(Guid employeeId)
        {
            return _context.SupportRequests.Where(r => r.EmployeeId == employeeId || r.EmployeeId == null).ToArray().ToDomain();
        }
    }
}

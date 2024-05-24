using Vibe.Domain.SupportRequests;
using Vibe.Services.SupportRequests.Converters;
using Vibe.EF;
using Vibe.EF.Entities.SupportEntities;
using Vibe.EF.Interface;
using Vibe.Tools.Result;
using Vibe.Domain.SupportRequests.SupportMessages;

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

        public Result SaveSupportMessage(SupportMessageBlank blank)
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

            return Result.Success;
        }

        public SupportRequestDetail? GetSupportRequestDetail(Guid id)
        {
            SupportRequest? supportRequest = _context.SupportRequests.FirstOrDefault(r => r.Id == id)?.ToDomain();
            if (supportRequest == null) return null;

            SupportMessage[] messages = _context.SupportMessages.Where(m => m.SupportRequestId == id).ToArray().ToDomain();
            return new SupportRequestDetail(supportRequest, messages);
        }

        public SupportRequest[] GetSupportRequests(Guid clientId)
        {
            return _context.SupportRequests.Where(sr => sr.ClientId == clientId).ToDomain();
        }
    }
}

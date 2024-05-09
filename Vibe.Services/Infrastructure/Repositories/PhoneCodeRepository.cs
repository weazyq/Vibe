using Vibe.Domain.Infrastructure;
using Vibe.Domain.Infrastructure.Converters;
using Vibe.EF;
using Vibe.EF.Entities;
using Vibe.EF.Interface;
using Vibe.Tools.Result;

namespace Vibe.Services
{
    public class PhoneCodeRepository : IPhoneCodeRepository
    {
        private DataContext _context { get; init; }

        public PhoneCodeRepository(DataContext context) {  _context = context; }

        public Result SaveSms(String phone, String code)
        {
            try
            {
                PhoneCodeEntity? phoneCode = _context.PhoneCodes.FirstOrDefault(pc => pc.Phone == phone);
                if (phoneCode != null) _context.PhoneCodes.Remove(phoneCode);
                _context.PhoneCodes.Add(new PhoneCodeEntity { 
                    Code = code, 
                    Phone = phone, 
                    CreatedAt = DateTime.UtcNow, 
                    ValidityMinutes = 1 
                });
            
                _context.SaveChanges();
                return Result.Success;
            }
            catch (Exception e)
            {
                return Result.Fail("Не удалось отправить код");
            }
        }

        public PhoneCode? GetSms(String phoneNumber)
        {
            return _context.PhoneCodes.FirstOrDefault(p => p.Phone == phoneNumber)?.ToDomain();
        }
    }
}

using Vibe.EF.Entities;
using Vibe.EF.Interface;
using Vibe.Tools.Result;

namespace Vibe.EF
{
    public class PhoneCodeRepository : IPhoneCodeRepository
    {
        private DataContext _context { get; init; }

        public PhoneCodeRepository(DataContext context) {  _context = context; }

        public Result SaveSms(PhoneCodeEntity entity)
        {
            try
            {
                PhoneCodeEntity? phoneCode = _context.PhoneCodes.FirstOrDefault(pc => pc.Phone == entity.Phone);
                if (phoneCode != null)
                {
                    phoneCode.Code = entity.Code;
                    _context.PhoneCodes.Update(phoneCode);
                }
                else _context.PhoneCodes.Add(entity);
            
                _context.SaveChanges();
                return Result.Success;
            }
            catch (Exception e)
            {
                return Result.Fail("Не удалось отправить код");
            }
        }

        public PhoneCodeEntity? GetSms(String phoneNumber)
        {
            return _context.PhoneCodes.FirstOrDefault(p => p.Phone == phoneNumber);
        }
    }
}

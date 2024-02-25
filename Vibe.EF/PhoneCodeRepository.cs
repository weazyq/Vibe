using Vibe.EF.Entities;
using Vibe.EF.Interface;
using Vibe.Tools.Result;

namespace Vibe.EF
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
                if (phoneCode != null)
                {
                    phoneCode.Code = code;
                    _context.PhoneCodes.Update(phoneCode);
                }
                else _context.PhoneCodes.Add(new PhoneCodeEntity { Code = code, Phone = phone });
            
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

namespace Vibe.Domain.Infrastructure
{
    public class PhoneCode
    {
        public String Phone { get; }
        public String Code { get; }
        public Int32 ValidityMinutes { get; }
        public DateTime CreatedAt { get; }

        public PhoneCode(String phone, String code, Int32 validityMinutes, DateTime createdAt)
        {
            Phone = phone;
            Code = code;
            ValidityMinutes = validityMinutes;
            CreatedAt = createdAt;
        }
    }
}
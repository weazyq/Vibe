using Microsoft.EntityFrameworkCore;

namespace Vibe.EF.Entities
{
    [PrimaryKey("Phone")]
    public class PhoneCodeEntity
    {
        public String Phone { get; set; } = String.Empty;
        public String Code { get; set; } = String.Empty;
        public Int32 ValidityMinutes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}

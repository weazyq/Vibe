using Microsoft.EntityFrameworkCore;

namespace Vibe.EF.Entities
{
    [PrimaryKey("Phone")]
    public class PhoneCodeEntity
    {
        public required String Phone { get; set; }
        public required String Code { get; set; }
        public Int32 ValidityMinutes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}

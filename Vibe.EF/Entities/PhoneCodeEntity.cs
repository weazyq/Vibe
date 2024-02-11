using Microsoft.EntityFrameworkCore;

namespace Vibe.EF.Entities
{
    [PrimaryKey("Phone")]
    public class PhoneCodeEntity
    {
        public String Phone { get; set; } = String.Empty;
        public String Code { get; set; } = String.Empty;
    }
}

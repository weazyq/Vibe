namespace Vibe.EF.Entities.Base
{
    public interface IAuditable
    {
        DateTime CreatedAt { get; set; }
        Guid? CreatedBy { get; set; }
        DateTime? ModifiedAt { get; set; }
        Guid? UpdatedBy { get; set; }
    }
}

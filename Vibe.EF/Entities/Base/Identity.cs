namespace Vibe.EF.Entities.Base
{
    public class Identity : IHaveId
    {
        public Guid Id { get; set; }
    }
}

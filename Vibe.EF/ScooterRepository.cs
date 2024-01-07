using Vibe.EF.Entities;

namespace Vibe.EF
{
    public class ScooterRepository : IDataRepository<ScooterEntity>
    {
        private DataContext _context { get; init; }

        public ScooterRepository(DataContext context)
        {
            _context = context;
        }

        public void Remove(Guid id)
        {
            ScooterEntity? scooter = Get(id);
            if (scooter == null) return;

            Update(scooter);
        }

        public ScooterEntity? Get(Guid id)
        {
            return _context.Scooters.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<ScooterEntity> List()
        {
            return _context.Scooters.ToList();
        }

        public void Save(ScooterEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Update(ScooterEntity entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}

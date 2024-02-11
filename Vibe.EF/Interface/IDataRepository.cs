namespace Vibe.EF.Interface
{
    public interface IDataRepository<TEntity>
    {
        void Save(TEntity entity);
        IEnumerable<TEntity> List();
        TEntity? Get(Guid id);
        void Remove(Guid id);
        void Update(TEntity entity);
    }
}

namespace UsersAPI.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity, TKey> : IDisposable
        where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        
        List<TEntity> FindAll();
        List<TEntity> FindAll(Func<TEntity, bool> where);
        TEntity? FindById(TKey id);
        TEntity? Find(Func<TEntity,bool> where);
    }
}

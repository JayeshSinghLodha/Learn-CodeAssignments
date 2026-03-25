namespace BankingSystem.Repositories.Interfaces;

public interface IRepository<TEntity>
{
    IEnumerable<TEntity> GetAll();
    void Add(TEntity entity);
    void Update(TEntity entity);
}

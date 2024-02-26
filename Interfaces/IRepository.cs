namespace XPTechnicalInterview.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // Retrieves all entities of the specified type
        IEnumerable<TEntity> ListAll();

        // Retrieves an entity by its identifier
        TEntity GetById(long id);

        // Creates a new entity
        TEntity Create(TEntity entity);

        // Updates an existing entity
        TEntity Update(TEntity entity);

        // Deletes an entity by its identifier
        void Delete(long id);
    }
}

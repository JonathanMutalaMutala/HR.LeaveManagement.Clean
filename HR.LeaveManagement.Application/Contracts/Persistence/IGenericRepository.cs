namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    /// <summary>
    /// Interface public qui Accepte T ou Type est une classe 
    /// </summary>
    /// <typeparam name="T">Represente un Type </typeparam>
    public interface IGenericRepository<T> where T : class
    {
       
        /// <summary>
        /// Permet de récupérer toutes les entités
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyList<T>> GetAsync();

        /// <summary>
        /// Permet de récupérer une entité par son Id
        /// </summary>
        /// <param name="id">Représente l'ID de l'entité</param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);
        /// <summary>
        ///¨Créer un entité 
        /// </summary>
        /// <param name="entity">Represente L'objet de l'entité à créer</param>
        /// <returns></returns>
        Task CreateAsync(T entity);

        /// <summary>
        /// Permet de mettre à jour une entité 
        /// </summary>
        /// <param name="entity">Represente l'objet de l'entité à mettre à jour</param>
        /// <returns></returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Supprimer une entité 
        /// </summary>
        /// <param name="entity">Represente l'objet à supprimer</param>
        /// <returns></returns>
        Task DeleteAsync(T entity);
       
    }

}

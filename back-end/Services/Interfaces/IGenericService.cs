namespace RateForProfessor.Services.Interfaces
{
    public interface IGenericService<T, TEntity> where T : class where TEntity : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        Task<T> AddWithPhotoAsync(T entity, string photoPath);

        Task UploadPhotoAsync(int id, string photoPath);

    }

}

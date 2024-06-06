namespace RateForProfessor.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
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

using Microsoft.EntityFrameworkCore;
using RateForProfessor.Context;
using RateForProfessor.Repositories.Interfaces;

namespace RateForProfessor.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
            _dbContext = dbContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _dbSet.Find(id);

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> AddWithPhotoAsync(T entity, string photoPath)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (string.IsNullOrWhiteSpace(photoPath))
                throw new ArgumentException("Photo path cannot be empty.", nameof(photoPath));

            var entityWithImage = entity;

            var propertyInfo = entityWithImage.GetType().GetProperty("ProfilePhotoPath");
            if (propertyInfo != null)
                propertyInfo.SetValue(entityWithImage, photoPath);

            _dbContext.Set<T>().Add(entityWithImage);
            await _dbContext.SaveChangesAsync();

            return entityWithImage;
        }

        public async Task UploadPhotoAsync(int id, string photoPath)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var propertyInfo = entity.GetType().GetProperty("ProfilePhotoPath");
            if (propertyInfo != null)
                propertyInfo.SetValue(entity, photoPath);

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }


    }
}

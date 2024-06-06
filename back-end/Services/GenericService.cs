using AutoMapper;
using RateForProfessor.Repositories.Interfaces;
using RateForProfessor.Services.Interfaces;

public class GenericService<T, TEntity> : IGenericService<T, TEntity> where T : class where TEntity : class
{
    private readonly IGenericRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<T>>(entities);
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<T>(entity);
    }

    public async Task<T> AddAsync(T entity)
    {
        var entityToCreate = _mapper.Map<TEntity>(entity);
        var createdEntity = await _repository.AddAsync(entityToCreate);
        return _mapper.Map<T>(createdEntity);
    }

    public async Task UpdateAsync(T entity)
    {
        var entityToUpdate = _mapper.Map<TEntity>(entity);
        await _repository.UpdateAsync(entityToUpdate);
    }

    public async Task DeleteAsync(int id)
    {
        //var entityToDelete = _mapper.Map<TEntity>(entity);
        await _repository.DeleteAsync(id);
    }


    public async Task<T> AddWithPhotoAsync(T entity, string photoPath)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        if (string.IsNullOrWhiteSpace(photoPath))
            throw new ArgumentException("Photo path cannot be empty.", nameof(photoPath));

        var entityWithImage = _mapper.Map<TEntity>(entity);
        var propertyInfo = entityWithImage.GetType().GetProperty("ProfilePhotoPath");
        if (propertyInfo != null)
            propertyInfo.SetValue(entityWithImage, photoPath);

        var addedEntity = await _repository.AddAsync(entityWithImage);
        return _mapper.Map<T>(addedEntity);
    }



    public async Task UploadPhotoAsync(int id, string photoPath)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        var propertyInfo = entity.GetType().GetProperty("ProfilePhotoPath");
        if (propertyInfo != null)
            propertyInfo.SetValue(entity, photoPath);

        await _repository.UpdateAsync(entity);
    }

}


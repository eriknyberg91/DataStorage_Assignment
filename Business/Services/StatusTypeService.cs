using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class StatusTypeService(IStatusTypeRepository statusTypeRepository) : IStatusTypeService
{
    private readonly IStatusTypeRepository _statusTypeRepository = statusTypeRepository;


    public async Task<StatusType> CreateStatusTypeAsync(StatusTypeRegistrationForm form)
    {
        var newStatusTypeEntity = await _statusTypeRepository.GetAsync(x => x.StatusName == form.StatusName);
        if (newStatusTypeEntity == null)
        {
            newStatusTypeEntity = await _statusTypeRepository.CreateAsync(StatusTypeFactory.Create(form));
        }

        return StatusTypeFactory.Create(newStatusTypeEntity);
    }

    public async Task<IEnumerable<StatusTypeEntity>> GetAllStatusTypesAsync()
    {
        return await _statusTypeRepository.GetAllAsync();
    }

    public async Task<StatusType> GetStatusTypeAsync(Expression<Func<StatusTypeEntity, bool>> expression)
    {
        var entity = await _statusTypeRepository.GetAsync(expression);
        var status = StatusTypeFactory.Create(entity);
        return status ?? null!;
    }

    public async Task<bool> DeleteStatusTypeAsync(int id)
    {
        var status = await _statusTypeRepository.GetAsync(x => x.Id == id);
        var result = await _statusTypeRepository.DeleteAsync(x => x.Id == id, status);

        return result;
    }
    public async Task<bool> CheckIfStatusTypeExistsAsync(Expression<Func<StatusTypeEntity, bool>> expression)
    {
        return await _statusTypeRepository.ExistsAsync(expression);
    }
}

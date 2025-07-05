using JobBoard.Core.Entities;
using JobBoard.Core.Interfaces;

namespace JobBoard.Application.Services;

public class EmployerService : IEmployerService
{
    private readonly IEmployerRepository _employerRepository;

    public EmployerService(IEmployerRepository employerRepository)
    {
        _employerRepository = employerRepository;
    }

    public async Task<Employer> CreateEmployerAsync(Employer employer)
    {
        if (await _employerRepository.GetByEmailAsync(employer.Email) != null)
            throw new Exception("Email already exists.");

        employer.Id = Guid.NewGuid();
        await _employerRepository.AddAsync(employer);
        return employer;
    }

    public async Task<Employer?> GetEmployerByIdAsync(Guid id)
    {
        return await _employerRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Employer>> GetAllEmployersAsync()
    {
        return await _employerRepository.GetAllAsync();
    }

    public async Task UpdateEmployerAsync(Employer employer)
    {
        if (await _employerRepository.GetByIdAsync(employer.Id) == null)
            throw new Exception("Employer not found.");

        await _employerRepository.UpdateAsync(employer);
    }

    public async Task DeleteEmployerAsync(Guid id)
    {
        if (await _employerRepository.GetByIdAsync(id) == null)
            throw new Exception("Employer not found.");

        await _employerRepository.DeleteAsync(id);
    }
}
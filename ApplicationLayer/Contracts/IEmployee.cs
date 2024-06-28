using ApplicationLayer.Commons;
using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.Employees;

namespace ApplicationLayer.Contracts
{
    public interface IEmployee
    {
        Task<EmployeeDto> AddAsync(CreateEmployeeDto createEmployeeDto);
        Task<EmployeeDto> UpdateAsync(CreateEmployeeDto createEmployeeDto);
        Task DeleteAsync(int id);
        Task<List<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> GetByIdAsync(int id);
        Task<PaginatedList<EmployeeDto>> GetAllEmployeesWithPaging(int page, int pageSize, string searchValue);
    }
}

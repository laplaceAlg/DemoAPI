using ApplicationLayer.Commons;
using ApplicationLayer.Dtos.Departments;

namespace ApplicationLayer.Contracts
{
    public interface IDepartment
    {
        Task<DepartmentDto> AddAsync(CreateDepartmentDto departmentDto);
        Task<DepartmentDto> UpdateAsync(CreateDepartmentDto departmentDto);
        Task DeleteAsync(int id);
        Task<List<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto> GetByIdAsync(int id);
        Task<PaginatedList<DepartmentDto>> GetAllDepartmentsWithPaging(int page, int pageSize, string searchValue);
    }
}

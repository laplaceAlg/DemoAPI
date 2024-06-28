using ApplicationLayer.Commons;
using ApplicationLayer.Contracts;
using ApplicationLayer.Dtos.Employees;
using AutoMapper;
using DomainLayer.Entities;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Implementations
{
    public class EmployeeRepo : IEmployee
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployeeRepo(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }   
        public async Task<EmployeeDto> AddAsync(CreateEmployeeDto createEmployeeDto)
        {
            var check = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Name.ToLower() == createEmployeeDto.Name.ToLower());
            if (check != null)
                /* return new ServiceResponse(true, "Employee already exist");*/
                throw new Exception($"Employee already exist");
           /* return null;*/
            /* var employee = new Employee
             {
                 Id = CreateEmployeeDto.Id,
                 Name = CreateEmployeeDto.Name,
                 Address = CreateEmployeeDto.Address
             };
             _dbContext.Employees.Add(employee);
             await SaveChangesAsync();
             return new ServiceResponse(true, "Added");*/
            var employee = new Employee
            {
                Id = createEmployeeDto.Id,
                Name = createEmployeeDto.Name,
                Address = createEmployeeDto.Address,
                Dept_Emps = new List<Dept_Emp>()
            };

            if (createEmployeeDto.DepartmentIds != null && createEmployeeDto.DepartmentIds.Any())
            {
                foreach (var departmentId in createEmployeeDto.DepartmentIds)
                {
                    var department = await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id == departmentId);
                    if (department == null) 
                        throw new Exception($"Department with ID {departmentId} not found");

                   /* return null;*/ /*new ServiceResponse(false, $"Department with ID {departmentId} not found");*/

                    var deptEmp = new Dept_Emp
                    {
                        DepartmentId = department.Id,
                        Department = department,
                        EmployeeId = employee.Id,
                        Employee = employee
                    };
                    _dbContext.Dept_Emps.Add(deptEmp);
                }
            }

            _dbContext.Employees.Add(employee);
            await SaveChangesAsync();

            /* return new ServiceResponse(true, "Employee added");*/
           /* var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Address = employee.Address,
                Departments = employee.Dept_Emps.Select(p => p.Department.Name).ToList() 
            };*/
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee == null)
                throw new Exception($"Employee not found with id {id}");
               /* return new ServiceResponse(false, $"Employee not found with id {id}");*/
            _dbContext.Employees.Remove(employee);
            await SaveChangesAsync();
            /*return new ServiceResponse(true, "Deleted");*/
        }

        public async Task<PaginatedList<EmployeeDto>> GetAllEmployeesWithPaging(int page, int pageSize, string searchValue)
        {
            IQueryable<Employee> query = _dbContext.Employees.
                Include(e => e.Dept_Emps)
                .ThenInclude(e => e.Department)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(e => e.Name.Contains(searchValue));
            var result = await PaginatedList<Employee>.ToPagedList(query, page, pageSize);

            /*  var employeeDtos = result.Select(e => new EmployeeDto
              {
                  Id = e.Id,
                  Name = e.Name,
                  Address = e.Address,
                  Departments = e.Dept_Emps.Select(de => de.Department.Name).ToList()
              }).ToList();*/
            var employeeDtos = _mapper.Map<List<EmployeeDto>>(result); ;
            return new PaginatedList<EmployeeDto>(employeeDtos, result.TotalCount, page, pageSize);
        }

        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            var employees = await _dbContext.Employees
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Address = e.Address,
                    Departments = e.Dept_Emps.Select(de => de.Department.Name).ToList(),
                }).ToListAsync();
            return employees;
        }

        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            /* var employee = await _dbContext.Employees
                 .Where(e => e.Id == id)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Address = e.Address,
                    Departments = e.Dept_Emps.Select(de => de.Department.Name).ToList(),
                }).FirstOrDefaultAsync();
             return employee;*/
            var employee = await _dbContext.Employees
                .Include(e => e.Dept_Emps)
                .ThenInclude(de => de.Department)
                .FirstOrDefaultAsync(e => e.Id == id);

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public async Task<EmployeeDto> UpdateAsync(CreateEmployeeDto createEmployeeDto)
        {
            var employeeUpdate = await _dbContext.Employees
                                        .Include(e => e.Dept_Emps)
                                        .ThenInclude(d => d.Department)
                                        .FirstOrDefaultAsync(e => e.Id == createEmployeeDto.Id);
            if (employeeUpdate == null)
                throw new Exception($"Employee with Id {createEmployeeDto.Id} not found");
            /* return new ServiceResponse(false, "Employee not found");*/
            employeeUpdate.Name = createEmployeeDto.Name;
            employeeUpdate.Address = createEmployeeDto.Address;
            if (createEmployeeDto.DepartmentIds != null)
            {
                _dbContext.Dept_Emps.RemoveRange(employeeUpdate.Dept_Emps);

                foreach (var departmentId in createEmployeeDto.DepartmentIds)
                {
                    var department = await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id == departmentId);
                    if (department == null)
                        throw new Exception($"Department with ID {departmentId} not found");
                    /* return new ServiceResponse(false, $"Department with ID {departmentId} not found");*/

                    var deptEmp = new Dept_Emp
                    {
                        DepartmentId = department.Id,
                        Department = department,
                        EmployeeId = employeeUpdate.Id,
                        Employee = employeeUpdate
                    };

                    employeeUpdate.Dept_Emps.Add(deptEmp);
                    _dbContext.Dept_Emps.Add(deptEmp);
                }
            }
                _dbContext.Employees.Update(employeeUpdate);
            await SaveChangesAsync();
        /*    var employeeDto = new EmployeeDto
            {
                Id = employeeUpdate.Id,
                Name = employeeUpdate.Name,
                Address = employeeUpdate.Address,
                Departments = employeeUpdate.Dept_Emps.Select(p => p.Department.Name).ToList()
            };*/
        var employeeDto = _mapper.Map<EmployeeDto>(employeeUpdate);
            return employeeDto;
          /*  return new ServiceResponse(true, "Updated");*/
        }
        private async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}


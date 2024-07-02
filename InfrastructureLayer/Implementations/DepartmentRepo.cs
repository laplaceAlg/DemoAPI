using ApplicationLayer.Commons;
using ApplicationLayer.Contracts;
using ApplicationLayer.Dtos.Departments;
using AutoMapper;
using DomainLayer.Entities;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace InfrastructureLayer.Implementations
{
    public class DepartmentRepo : IDepartment
    {
        private readonly AppDbContext _dbContext;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        private static readonly string CacheKeyAllDepartments = "AllDepartments";
        private static readonly string CacheKeyDepartmentByIdPrefix = "DepartmentById_";
        private static readonly string CacheKeyDepartmentsPagingPrefix = "DepartmentsPaging_";
        public DepartmentRepo(AppDbContext dbContext, IMemoryCache cache, IMapper mapper)
        {
            _dbContext = dbContext;
            _cache = cache;
            _mapper = mapper;
        }
        public async Task<DepartmentDto> AddAsync(CreateDepartmentDto departmentDto)
        {
            var check = await _dbContext.Departments.FirstOrDefaultAsync(x => x.Name.ToLower() == departmentDto.Name.ToLower());
            if (check != null)
                throw new AppException(400,"Department already exist");
            /*return new ServiceResponse(true, "Department already exist");*/
            var department = new Department
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name
            };
            _dbContext.Departments.Add(department);
            await SaveChangesAsync();
            var data = _mapper.Map<DepartmentDto>(department);
            _cache.Remove(CacheKeyAllDepartments);
            _cache.Remove(CacheKeyDepartmentByIdPrefix + departmentDto.Id);
            RemovePagingCache();
            return data;
            /*return new ServiceResponse(true, "Added");*/
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _dbContext.Departments.FindAsync(id);
            if (department == null)
                throw new AppException(404,$"Department not found with id {id}");
               /* return new ServiceResponse(false, $"Department not found with id {id}");*/
            _dbContext.Departments.Remove(department);
            await SaveChangesAsync();
            _cache.Remove(CacheKeyAllDepartments);
            _cache.Remove(CacheKeyDepartmentByIdPrefix + id);
            RemovePagingCache();
            /*  return new ServiceResponse(true, "Deleted");*/
        }

        public async Task<List<DepartmentDto>> GetAllAsync()
        {
            if (!_cache.TryGetValue(CacheKeyAllDepartments, out List<DepartmentDto> departments))
            {
                var departmentEntities = await _dbContext.Departments
                    .Include(d => d.Dept_Emps)
                       .ThenInclude(e => e.Employee)
                    .ToListAsync();
                departments = _mapper.Map<List<DepartmentDto>>(departmentEntities);
                _cache.Set(CacheKeyAllDepartments, departments, TimeSpan.FromMinutes(5));
            }
            if (departments == null)
                throw new AppException(404, "Department not availble");
            return departments;
        }

        public async Task<PaginatedList<DepartmentDto>> GetAllDepartmentsWithPaging(int page, int pageSize, string searchValue)
        {
            if (page <= 0 || pageSize <= 0)
                throw new AppException(0, "Page and PageSize must be greater than zero.");
            string cacheKey = $"{CacheKeyDepartmentsPagingPrefix}_{page}_{pageSize}_{searchValue}";

            if (!_cache.TryGetValue(cacheKey, out PaginatedList<DepartmentDto> cachedPagedDepartments))
            {
                IQueryable<Department> query = _dbContext.Departments
                     .Include(e => e.Dept_Emps)
                     .ThenInclude(e => e.Employee)
                     .AsQueryable();

                if (!string.IsNullOrEmpty(searchValue))
                    query = query.Where(e => e.Name.Contains(searchValue));

                var result = await PaginatedList<Department>.ToPagedList(query, page, pageSize);

                var departmentDtos = _mapper.Map<List<DepartmentDto>>(result);

                cachedPagedDepartments = new PaginatedList<DepartmentDto>(departmentDtos, result.TotalCount, page, pageSize);

                _cache.Set(cacheKey, cachedPagedDepartments, TimeSpan.FromMinutes(5));

                var cacheKeys = _cache.GetOrCreate("PagingCacheKeys", entry => new List<string>());
                cacheKeys.Add(cacheKey);
                _cache.Set("PagingCacheKeys", cacheKeys);
            }

            return cachedPagedDepartments;
        }
             
        public async Task<DepartmentDto> GetByIdAsync(int id)
        {
            string cacheKey = CacheKeyDepartmentByIdPrefix + id;
            var department = new DepartmentDto();
            if (!_cache.TryGetValue(cacheKey, out  department))
            {
                var departmentEntity = await _dbContext.Departments
                   .Include(d => d.Dept_Emps)
                   .ThenInclude(de => de.Employee)
                   .FirstOrDefaultAsync(d => d.Id == id);

                if (departmentEntity != null)
                {
                    department = _mapper.Map<DepartmentDto>(departmentEntity);
                    _cache.Set(cacheKey, department, TimeSpan.FromMinutes(5));
                }
            }
            if (department == null)
                throw new AppException(404,$"Department not found with id {id}");
            return department;
        }

        public async Task<DepartmentDto> UpdateAsync(CreateDepartmentDto departmentDto)
        {
            var departmentUpdate = await _dbContext.Departments
                 .Include(d => d.Dept_Emps).ThenInclude(e => e.Employee)
                 .FirstOrDefaultAsync(e => e.Id == departmentDto.Id);
            if (departmentUpdate == null)
                throw new AppException(404, $"Department not found with id {departmentDto.Id}");
            /* return new ServiceResponse(false, "Department not found");*/
            var check = await _dbContext.Departments.FirstOrDefaultAsync(x => x.Name.ToLower() == departmentDto.Name.ToLower() && x.Id != departmentDto.Id);
            if (check != null)
                throw new AppException(400,"Department already exist");
            departmentUpdate.Name = departmentDto.Name;
            _dbContext.Departments.Update(departmentUpdate);
            await SaveChangesAsync();
            var data = _mapper.Map<DepartmentDto>(departmentUpdate);
        
            _cache.Remove(CacheKeyAllDepartments);
            _cache.Remove(CacheKeyDepartmentByIdPrefix + departmentDto.Id);
            RemovePagingCache();
            return data;
           /* return new ServiceResponse(true, "Updated");*/
        }
        private async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
        private void RemovePagingCache()
        {
            if (_cache.TryGetValue("PagingCacheKeys", out List<string> cacheKeys))
            {
                foreach (var cacheKey in cacheKeys)
                {
                    _cache.Remove(cacheKey);
                }
                _cache.Remove("PagingCacheKeys");
            }
        }
    }
}

using SampleEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SampleEntityFramework.Helpers
{
    public interface IEmployeesRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(int id);

        Task<bool> AddEmployeeAsync(Employee Employee);

        Task<bool> AddEmployeeAsync(IEnumerable<Employee> Employees);

        Task<bool> UpdateEmployeeAsync(Employee Employee);

        Task<bool> RemoveEmployeeAsync(int id);

        Task<IEnumerable<Employee>> QueryEmployeesAsync(Func<Employee, bool> predicate);
    }
}

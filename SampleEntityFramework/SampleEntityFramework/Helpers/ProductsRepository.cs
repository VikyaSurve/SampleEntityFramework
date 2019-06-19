

using Microsoft.EntityFrameworkCore;
using SampleEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleEntityFramework.Helpers
{
    public class EmployeesRepository : IEmployeesRepository
    {

        private readonly DatabaseContext _databaseContext;

        public EmployeesRepository(string dbPath)
        {
            _databaseContext = new DatabaseContext(dbPath);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            try
            {
                var Employees = await _databaseContext.Employees.ToListAsync();

                return Employees;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var Employee = await _databaseContext.Employees.FindAsync(id);

                return Employee;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> AddEmployeeAsync(Employee Employee)
        {
            try
            {
                var tracking = await _databaseContext.Employees.AddAsync(Employee);

                await _databaseContext.SaveChangesAsync();

                var isAdded = tracking.State == EntityState.Added;

                return isAdded;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> AddEmployeeAsync(IEnumerable<Employee> Employee)
        {
            try
            {
                await _databaseContext.Employees.AddRangeAsync(Employee);

                await _databaseContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateEmployeeAsync(Employee Employee)
        {
            try
            {
                var tracking = _databaseContext.Update(Employee);

                await _databaseContext.SaveChangesAsync();

                var isModified = tracking.State == EntityState.Modified;

                return isModified;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> RemoveEmployeeAsync(int id)
        {
            try
            {
                var Employee = await _databaseContext.Employees.FindAsync(id);

                var tracking = _databaseContext.Remove(Employee);

                await _databaseContext.SaveChangesAsync();

                var isDeleted = tracking.State == EntityState.Deleted;

                return isDeleted;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Employee>> QueryEmployeesAsync(Func<Employee, bool> predicate)
        {
            try
            {
                var Employees = _databaseContext.Employees.Where(predicate);

                return Employees.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}

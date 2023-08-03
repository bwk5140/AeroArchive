using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using AeroArchive.Models;

namespace AeroArchive.AppDatabase
{
    public class EmployeeDatabase
    {
        readonly SQLiteAsyncConnection database;

        public EmployeeDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Employee>().Wait();
        }

        public Task<List<Employee>> GetEmployeeDetsAsync()
        {
            //Get all employee entries.
            return database.Table<Employee>().ToListAsync();
        }

        public Task<Employee> GetEmployeeDetsAsync(int id)
        {
            // Get a specific employee.
            return database.Table<Employee>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveEmployeeDetsAsync(Employee employee)
        {
            if (employee.ID != 0)
            {
                // Update an employee entry.
                return database.UpdateAsync(employee);
            }
            else
            {
                // Save a new employee.
                return database.InsertAsync(employee);
            }
        }

        public Task<int> DeleteEmployeeDetsAsync(Employee employee)
        {
            // Delete an employee from DB.
            return database.DeleteAsync(employee);
        }

        public Task<int> ClearEmployeeDBAsync()
        {
            // Clear Employee DB.
            return database.DeleteAllAsync<Employee>();
        }
    }
}
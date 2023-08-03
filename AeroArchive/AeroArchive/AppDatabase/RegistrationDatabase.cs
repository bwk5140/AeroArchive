using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using AeroArchive.Models;

namespace AeroArchive.AppDatabase
{
    public class RegistrationDatabase
    {
        readonly SQLiteAsyncConnection database;

        public RegistrationDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Registration>().Wait();
        }

        public Task<List<Registration>> GetRegistrationDetsAsync()
        {
            //Get all accounts.
            return database.Table<Registration>().ToListAsync();
        }

        public Task<Registration> GetRegistrationDetsAsync(int id)
        {
            // Get a specific user account.
            return database.Table<Registration>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveRegistrationDetsAsync(Registration registration)
        {
            if (registration.ID != 0)
            {
                // Update an existing account.
                return database.UpdateAsync(registration);
            }
            else
            {
                // Save a new user account.
                return database.InsertAsync(registration);
            }
        }

        public Task<int> DeleteRegistrationDetsAsync(Registration registration)
        {
            // Delete a user account.
            return database.DeleteAsync(registration);
        }

        public Task<int> ClearAccountsDBAsync()
        {
            // Delete a product.
            return database.DeleteAllAsync<Registration>();
        }
    }
}
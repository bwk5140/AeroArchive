using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using AeroArchive.Models;

namespace AeroArchive.RegistrationDB
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
            //Get all notes.
            return database.Table<Registration>().ToListAsync();
        }

        public Task<Registration> GetRegistrationDetsAsync(int id)
        {
            // Get a specific note.
            return database.Table<Registration>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveRegistrationDetsAsync(Registration registration)
        {
            if (registration.ID != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(registration);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(registration);
            }
        }

        public Task<int> DeleteRegistrationDetsAsync(Registration registration)
        {
            // Delete a note.
            return database.DeleteAsync(registration);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using AeroArchive.Models;

namespace AeroArchive.AppDatabase
{
    public class ProductDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ProductDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Item>().Wait();
        }

        public Task<List<Item>> GetProductDetsAsync()
        {
            //Get all products.
            return database.Table<Item>().ToListAsync();
        }

        public Task<Item> GetProductDetsAsync(int id)
        {
            // Get a specific product.
            return database.Table<Item>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveProductDetsAsync(Item item)
        {
            if (item.ID != 0)
            {
                // Update an product.
                return database.UpdateAsync(item);
            }
            else
            {
                // Save a new product.
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteProductDetsAsync(Item item)
        {
            // Delete a product.
            return database.DeleteAsync(item);
        }
    }
}
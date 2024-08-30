using System;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace LibraryOlutoyosi
{
	public class Database
	{
        // 1. Define a property that represents the connection to the database
        // - C# property modifier
        // - only the constructor can modify the value of this property
        // (no other methods in the class can modify it)
        readonly SQLiteAsyncConnection dbConnection;

        public Database()
        {
            // Configure the connection

            // - specifying the name of the database file
            string databasePath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "BookDatabase.db");
            Console.WriteLine($"++++++ Database path: {databasePath}");

            // - specify "where" on the device the file will be saved
            dbConnection = new SQLiteAsyncConnection(databasePath);

            // Create the table (based on the TodoItem)
            dbConnection.CreateTableAsync<Book>().Wait();

            // Done!
            Console.WriteLine($"++++++ Database table created!");
        }

        // CRUD operations
        public async Task<int> AddBook(Book itemToAdd)
        {
            // INSERT INTO TodoItems (...,...,..,)
            int numRowsAdded = await dbConnection.InsertAsync(itemToAdd);
            return numRowsAdded;
        }

        // get everything from the table
        public async Task<List<Book>> GetAllBooks()
        {
            List<Book> resultsList = await dbConnection.Table<Book>().ToListAsync(); // SELECT *
            return resultsList;

        }

        // get a single item by its primary key
        public async Task<Book> GetBookById(int id)
        {
            Book result = await dbConnection.GetAsync<Book>(id); // SELECT * FROM WHERE id = id
            return result;

        }

        //update an existing item
        public async Task<int> UpdateItem(Book item)
        {
            return await dbConnection.UpdateAsync(item);
        }

        //Delete
        public async Task<int> DeleteItemById(int id)
        {
            return await dbConnection.DeleteAsync<Book>(id);

        }

    }
}



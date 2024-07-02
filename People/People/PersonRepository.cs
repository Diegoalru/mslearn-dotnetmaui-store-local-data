using SQLite;
using People.Models;

namespace People;

public class PersonRepository(string dbPath)
{
    public string StatusMessage { get; private set; }

    private SQLiteAsyncConnection _conn;

    private async Task InitAsync()
    {
        if (_conn == null)
        {
            _conn = new SQLiteAsyncConnection(dbPath);
            await _conn.CreateTableAsync<Person>();
        }
    }

    public async Task AddNewPersonAsync(string name)
    {
        try
        {
            await InitAsync();

            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            var result = await _conn.InsertAsync(new Person { Name = name });
            StatusMessage = $"{result} record(s) added (Name: {name})";
        }
        catch (SQLiteException ex)
        {
            StatusMessage = ex.Message.Contains("UNIQUE") ? $"Unable to register {name}. Already exists." : $"Failed to add {name}.";
        }
        catch (Exception)
        {
            StatusMessage = $"An unexpected error occurred while adding {name}.";
        }
    }

    public async Task<List<Person>> GetAllPeopleAsync()
    {
        try
        {
            await InitAsync();
            return await _conn.Table<Person>().ToListAsync();
        }
        catch (SQLiteException)
        {
            StatusMessage = "Failed to retrieve data.";
        }
        catch (Exception)
        {
            StatusMessage = "An unexpected error occurred while retrieving data.";
        }

        return new List<Person>();
    }
}

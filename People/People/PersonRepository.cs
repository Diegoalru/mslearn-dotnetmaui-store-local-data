using SQLite;
using People.Models;

namespace People;

public class PersonRepository(string dbPath)
{
    public string StatusMessage { get; private set; }

    private SQLiteConnection _conn;

    private void Init()
    {
        if (_conn != null)
            return;


        _conn = new SQLiteConnection(dbPath);

        if (string.IsNullOrEmpty(_conn.DatabasePath))
            throw new Exception("[Repository] Database path not found!");


        _conn.CreateTable<Person>();
    }

    public void AddNewPerson(string name)
    {
        try
        {
            Init();

            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            var result = _conn.Insert(new Person { Name = name });

            StatusMessage = $"{result} record(s) added (Name: {name})";
        }
        catch (SQLiteException ex)
        {
            if (ex.Message.Contains("UNIQUE"))
            {
                StatusMessage = $"Unable to register {name}. Already exists.";
                return;
            }

            StatusMessage = $"Failed to add {name}.";
        }
        catch (Exception)
        {
            StatusMessage = $"An unexpected error occurred while adding {name}.";
        }

    }

    public List<Person> GetAllPeople()
    {
        try
        {
            Init();

            return [.. _conn.Table<Person>()];
        }
        catch (SQLiteException)
        {
            StatusMessage = "Failed to retrieve data.";
        }
        catch (Exception)
        {
            StatusMessage = "An unexpected error occured while retrieve data.";
        }

        return [];
    }
}

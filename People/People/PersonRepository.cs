using SQLite;
using People.Models;

namespace People;

public class PersonRepository(string dbPath)
{
    public string StatusMessage { get; set; }

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
        catch (Exception ex)
        {
            StatusMessage = $"Failed to add {name}. Error: {ex.Message}";
        }
    }

    public List<Person> GetAllPeople()
    {
        // TODO: Init then retrieve a list of Person objects from the database into a list
        try
        {

        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<Person>();
    }
}

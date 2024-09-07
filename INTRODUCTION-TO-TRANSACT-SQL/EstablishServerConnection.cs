// you're going to be using the system
using System;
// you're going to be using the sql client
using System.Data.SqlClient;

// you're going to use the database connection singleton
// you're going to use a class that can be inherited (sealed)
public sealed class ServerConnectionSingleton
{

    // your're going to use a private constructor
    private ServerConnectionSingleton() {}

    // you're going to use a static reference
    private static ServerConnectionSingleton _instance = null;
    // you're going to use an object lock to prevent race conditions
    private static readonly object _lock = new object();

    // you're going to create the connection string
    private string _connectionString;

    // you're going to create a "getter declaration"
    public static ServerConnectionSingleton Instance
    {
        // gets if null
        get
        {
            if (_instance == null)
            {
                // acquire lock
                lock (_lock)
                {
                    // if its still null
                    if (_instance == null)
                    {
                        // create instance
                        _instance = new ServerConnectionSingleton();
                    }
                }
            }
            // return whats got
            return _instance;
        }
    }

    // what it says
    public void SetConnectionString(string server)
    {
        _connectionString = string.Format("Server={0};Database=master;Integrated Security=True;", server);
    
    }

     // return the open connection
    public SqlConnection GetOpenConnection()
    {
        // is legit connection string
        if (string.IsNullOrEmpty(_connectionString))
        {
            throw new InvalidOperationException("Connection string has not been set.");
        }

        // open connection
        var connection = new SqlConnection(_connectionString);
        try
        {
            connection.Open();
            return connection;
        }
        // ditch effort
        catch
        {
            connection.Dispose(); 
            throw;
        }
    }

    // test
    static void Main(string[] args)
    {
        // get the environment variable 
        string server = Environment.GetEnvironmentVariable("DB_SERVER");
        // set the connection string based off server environment variable
        ServerConnectionSingleton.Instance.SetConnectionString(server);

        // retrieve connection instance
        using (var connection = ServerConnectionSingleton.Instance.GetOpenConnection())
        {
            // yippy kay yay
            if (connection.State == System.Data.ConnectionState.Open)
            {
                Console.WriteLine("SUCCESSFUL CONNECTION TO DATABASE!");
            }
            else
            {
                Console.WriteLine("UNSUCCESSFUL CONNECTION TO DATABASE!");
            }
        }
    }
}
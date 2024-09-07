using System;
using System.Data.SqlClient;

namespace MicrosoftSQLServer
{

    // use DatabaseBuilder to build the databse for you
    public class DatabaseBuilder
    {
        // use serverConnection to build the database
        private SqlConnection _serverConnection;
        // use databaseName to name the database
        private string _databaseName;

        // use WithConnection to set the connection
        public DatabaseBuilder WithConnection(SqlConnection connection)
        {
            _serverConnection = connection;
            return this;
        }

        // use WithDatabaseName to set the database name
        public DatabaseBuilder WithDatabaseName(string databaseName)
        {
            _databaseName = databaseName;
            return this;
        }

        // build the database if it doesn't exist
        public void Build()
        {
            // set the server connection
            _serverConnection = MicrosoftSQLServer.ServerConnection.Instance.GetOpenConnection();
            var exists = false;
            // Use the command to assign value of exists
            using (var command = new SqlCommand("SELECT COUNT(*) FROM sys.databases WHERE name = @databaseName", _serverConnection))
            {
                // Provide missing argument for command
                command.Parameters.AddWithValue("@databaseName", _databaseName);
                // Execute command as a boolean of first row first column
                exists = (int)command.ExecuteScalar() > 0;
            }

            // prints correct message
            if (exists)
            {
                Console.WriteLine(string.Format("DATABASE {0} HAS BEEN CREATED ALREADY!", _databaseName));
            }
            else
            {
                // Create database
                Console.WriteLine(string.Format("DATABASE {0} CREATED!", _databaseName));
            }
        }

        static void Main(string[] args)
        {
            // get the environment variable 
            string server = Environment.GetEnvironmentVariable("DB_SERVER");
            // set the connection string based off server environment variable
            MicrosoftSQLServer.ServerConnection.Instance.SetConnectionString(server);

            // retrieve connection instance
            using (var connection = MicrosoftSQLServer.ServerConnection.Instance.GetOpenConnection())
            {
                // yippy kay yay
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    var databaseBuilder = new MicrosoftSQLServer.DatabaseBuilder();
                    databaseBuilder.WithConnection(connection).WithDatabaseName("HospitalEmergencyRoomDB").Build();
                    connection.Close();
                }
                else
                {
                    Console.WriteLine("UNSUCCESSFUL CONNECTION TO SERVER!");
                }
            }
        }
    }
}

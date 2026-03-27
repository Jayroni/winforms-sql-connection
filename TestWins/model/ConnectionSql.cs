using MySql.Data.MySqlClient;
using System;

namespace TestWins.Model
{
    public class ConnectionSql
    {
        private readonly string _connectionString = "server=localhost;database=student;uid=root;pwd=root";
        private MySqlConnection _conn;

        public MySqlConnection ConnectSql()
        {
            try
            {
                Console.WriteLine("Connecting to DB...");
                
                if (_conn == null || _conn.State == System.Data.ConnectionState.Closed)
                {
                    _conn = new MySqlConnection(_connectionString);
                    _conn.Open(); 
                }

                if (_conn.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine(" Database connection successful");
                    return _conn;
                }
                else
                {
                    Console.WriteLine(" Database connection failed");
                    return null;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($" Database connection error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Unexpected error: {ex.Message}");
                return null;
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (_conn != null && _conn.State == System.Data.ConnectionState.Open)
                {
                    _conn.Close();
                    Console.WriteLine("Connection closed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing connection: {ex.Message}");
            }
        }

        public MySqlConnection GetConnection()
        {
            return ConnectSql();
        }
    }
}

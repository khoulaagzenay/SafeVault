using Microsoft.Data.Sqlite;
using SecurityAndAuthenticationProject.Models;

namespace SecurityAndAuthenticationProject.Services
{
    public class AuthService
    {
            private readonly string _connectionString;

            public AuthService(string connectionString)
            {
                _connectionString = connectionString;
            }

            public bool StoreUser(UserInput input, string role = "User")
            {
                const string query = "INSERT INTO Users (Username, Email, PasswordHash, Role) VALUES (@Username, @Email, @PasswordHash, @Role)";

                var passwordHash = PasswordHelper.HashPassword(input.Password);

                using (var connection = new SqliteConnection(_connectionString))
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", input.Username);
                    command.Parameters.AddWithValue("@Email", input.Email);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    command.Parameters.AddWithValue("@Role", role);

                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }

                /* Using EF Core
                var user = new User
                {
                    Username = input.Username,
                    Email = input.Email,
                    PasswordHash = passwordHash
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
                */
            }

            public List<User> GetAllUsers()
            {
                var users = new List<User>();
                const string query = "SELECT UserID, Username, Email FROM Users";

                using var connection = new SqliteConnection(_connectionString);
                using var command = new SqliteCommand(query, connection);

                connection.Open();
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        UserID = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Email = reader.GetString(2)
                    });
                }

                return users;
            }

            public bool AuthenticateUser(string username, string password)
            {
                const string query = "SELECT PasswordHash FROM Users WHERE Username = @Username";

                using var connection = new SqliteConnection(_connectionString);
                using var command = new SqliteCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                var result = command.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    return false;

                var storedHash = result.ToString();
                return PasswordHelper.VerifyPassword(password, storedHash);
            }

            public bool IsUserAdmin(string username)
            {
                const string query = "SELECT Role FROM Users WHERE Username = @Username";

                using var connection = new SqliteConnection(_connectionString);
                using var command = new SqliteCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                connection.Open();
                var result = command.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    return false;

                var role = result.ToString();
                return role == "Admin";
            }
    }
}

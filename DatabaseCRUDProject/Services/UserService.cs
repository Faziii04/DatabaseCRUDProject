using DatabaseCRUDProject.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;

namespace DatabaseCRUDProject.Services
{
    static class UserService
    {
        public static User? Login(int ci, string password)
        {
            using var connection = new SqlConnection(DatabaseService.Database_Connection);

            string query = "SELECT * FROM users WHERE ci = @ci AND password = @password";

            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ci", ci);
            cmd.Parameters.AddWithValue("@password", password);

            try
            {
                connection.Open();
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new User(
                        (int)reader["ci"],
                        reader["first_name"].ToString()!,
                        reader["last_name"].ToString()!,
                        (byte)reader["age"],
                        reader["email"].ToString()!,
                        reader["phone_number"].ToString()!,
                        reader["password"].ToString()!,
                        reader["role"].ToString()!
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Error: {ex.Message}");
            }
            return null;
        }

        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(DatabaseService.Database_Connection))
            {
                string query = "select * from users";

                using SqlCommand cmd = new SqlCommand(query, connection);
                try
                {
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ci = (int)reader["ci"];
                            string first_name = (string)reader["first_name"];
                            string last_name = (string)reader["last_name"];
                            byte age = (byte)reader["age"];
                            string email = (string)reader["email"];
                            string phone_number = (string)reader["phone_number"];
                            string password = (string)reader["password"];
                            string role = (string)reader["role"];

                            users.Add(new User(ci, first_name, last_name, age, email, phone_number, password, role));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Database Error: {ex.Message}");
                }
            }

            return users;
        }

        public static User? AddUser(User user)
        {
            string query = @"INSERT INTO users (ci, first_name, last_name, age, email, phone_number, password, role) 
                    OUTPUT INSERTED.*
                    VALUES (@ci, @first_name, @last_name, @age, @email, @phone_number, @password, @role)";

            using (SqlConnection connection = new SqlConnection(DatabaseService.Database_Connection))
            {
                using SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ci", user.Ci);
                cmd.Parameters.AddWithValue("@first_name", user.FirstName);
                cmd.Parameters.AddWithValue("@last_name", user.LastName);
                cmd.Parameters.AddWithValue("@age", user.Age);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@phone_number", (object)user.PhoneNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@role", user.Role);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Map the database ID back to the object
                            user.Ci = Convert.ToInt32(reader["ci"]);
                            return user;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Database Error: {ex.Message}");
                }
            }
            return null; // Return null if the insert failed
        }

        public static User? DeleteUser(int ci)
        {
            string query = "DELETE FROM users OUTPUT DELETED.* WHERE ci = @ci";

            using (SqlConnection connection = new SqlConnection(DatabaseService.Database_Connection))
            {
                using SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ci", ci);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User(
                                Convert.ToInt32(reader["ci"]),
                                reader["first_name"].ToString()!,
                                reader["last_name"].ToString()!,
                                Convert.ToByte(reader["age"]),
                                reader["email"].ToString()!,
                                reader["phone_number"]?.ToString()!,
                                reader["password"].ToString()!,
                                reader["role"].ToString()!
                            );
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Database Error: {ex.Message}");
                }
            }
            return null; // Returns null if no user was found with that CI
        }


        public static User? EditUser(User user)
        {
            string query = @"UPDATE users 
                    SET first_name = @first_name, 
                        last_name = @last_name, 
                        age = @age, 
                        email = @email, 
                        phone_number = @phone_number, 
                        password = @password, 
                        role = @role
                    OUTPUT INSERTED.*
                    WHERE ci = @ci";

            using (SqlConnection connection = new SqlConnection(DatabaseService.Database_Connection))
            {
                using SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ci", user.Ci);
                cmd.Parameters.AddWithValue("@first_name", user.FirstName);
                cmd.Parameters.AddWithValue("@last_name", user.LastName);
                cmd.Parameters.AddWithValue("@age", user.Age);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@phone_number", (object)user.PhoneNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@role", user.Role);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return user;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Database Error: {ex.Message}");
                }
            }
            return null; 
        }


        


    }
}

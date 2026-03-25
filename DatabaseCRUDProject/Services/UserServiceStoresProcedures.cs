using DatabaseCRUDProject.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace DatabaseCRUDProject.Services
{
    static class UserServiceStoresProcedures
    {
        public static User? Login(int ci, string password)
        {
            using var connection = new SqlConnection(DatabaseService.Database_Connection);
            using var cmd = new SqlCommand("sp_UserLogin", connection);
            cmd.CommandType = CommandType.StoredProcedure;

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
                        reader["phone_number"]?.ToString()!,
                        reader["password"].ToString()!,
                        reader["role"].ToString()!
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Error (Login): {ex.Message}");
            }
            return null;
        }

        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            using var connection = new SqlConnection(DatabaseService.Database_Connection);

            // Now calling the stored procedure instead of a raw string
            using var cmd = new SqlCommand("sp_GetAllUsers", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new User(
                        (int)reader["ci"],
                        reader["first_name"].ToString()!,
                        reader["last_name"].ToString()!,
                        (byte)reader["age"],
                        reader["email"].ToString()!,
                        reader["phone_number"]?.ToString()!,
                        reader["password"].ToString()!,
                        reader["role"].ToString()!
                    ));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Error (GetAll): {ex.Message}");
            }
            return users;
        }

        public static User? AddUser(User user)
        {
            using var connection = new SqlConnection(DatabaseService.Database_Connection);
            using var cmd = new SqlCommand("sp_CreateUser", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ci", user.Ci);
            cmd.Parameters.AddWithValue("@first_name", user.FirstName);
            cmd.Parameters.AddWithValue("@last_name", user.LastName);
            cmd.Parameters.AddWithValue("@age", user.Age);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@phone_number", (object?)user.PhoneNumber ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@role", user.Role);

            try
            {
                connection.Open();
                using var reader = cmd.ExecuteReader();
                if (reader.Read()) return user;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Error (Add): {ex.Message}");
            }
            return null;
        }

        public static User? EditUser(User user)
        {
            using var connection = new SqlConnection(DatabaseService.Database_Connection);
            using var cmd = new SqlCommand("sp_UpdateUser", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ci", user.Ci);
            cmd.Parameters.AddWithValue("@first_name", user.FirstName);
            cmd.Parameters.AddWithValue("@last_name", user.LastName);
            cmd.Parameters.AddWithValue("@age", user.Age);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@phone_number", (object?)user.PhoneNumber ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@role", user.Role);

            try
            {
                connection.Open();
                using var reader = cmd.ExecuteReader();
                if (reader.Read()) return user;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Error (Edit): {ex.Message}");
            }
            return null;
        }

        public static User? DeleteUser(int ci)
        {
            using var connection = new SqlConnection(DatabaseService.Database_Connection);
            using var cmd = new SqlCommand("sp_DeleteUser", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ci", ci);

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
                        reader["phone_number"]?.ToString()!,
                        reader["password"].ToString()!,
                        reader["role"].ToString()!
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Error (Delete): {ex.Message}");
            }
            return null;
        }
    }
}
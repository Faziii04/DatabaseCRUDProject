using DatabaseCRUDProject.Models;
using DatabaseCRUDProject.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DatabaseCRUDProject.UserControls
{
    /// <summary>
    /// Lógica de interacción para LoginSimulation.xaml
    /// </summary>
    public partial class LoginSimulation : UserControl
    {
        public LoginSimulation()
        {
            InitializeComponent();
        }

        private void SimulateLoginButton_Click(object sender, RoutedEventArgs e)
        {
            string ciText = CiTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(ciText) || string.IsNullOrWhiteSpace(password))
            {
                ResultTextBlock.Text = "Enter CI and password.";
                UserDetailsTextBlock.Text = "No user data yet.";
                return;
            }

            if (!int.TryParse(ciText, out int ci))
            {
                ResultTextBlock.Text = "CI must be a valid integer.";
                UserDetailsTextBlock.Text = "No user data yet.";
                return;
            }

            User? user = UserServiceStoresProcedures.Login(ci, password);
            if (user == null)
            {
                ResultTextBlock.Text = "Login failed.";
                UserDetailsTextBlock.Text = "No matching user found.";
                return;
            }

            ResultTextBlock.Text = "Login success.";
            UserDetailsTextBlock.Text =
                $"CI: {user.Ci}\n" +
                $"Name: {user.FirstName} {user.LastName}\n" +
                $"Age: {user.Age}\n" +
                $"Email: {user.Email}\n" +
                $"Phone: {user.PhoneNumber}\n" +
                $"Role: {user.Role}";
        }
    }
}

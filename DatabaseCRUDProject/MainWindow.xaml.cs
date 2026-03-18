using DatabaseCRUDProject.Windows;
using System.Windows;
using DatabaseCRUDProject.Services;
using DatabaseCRUDProject.Models;

namespace DatabaseCRUDProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            string ciText = CiTextBox.Text.Trim();  
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(ciText) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both CI and password.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(ciText, out int ci))
            {
                MessageBox.Show("CI must be a valid integer.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            User? user = UserService.Login(ci, password);

            if (user == null)
            {
                MessageBox.Show("Invalid CI or password. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            GlobalVars.SetCurrentUser(user);
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();




            /*
            var ci = CiTextBox.Text?.Trim();
            var password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both CI and password.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dashboard = new Dashboard();
            dashboard.Show();
            Close();
            */
        }
    }
}
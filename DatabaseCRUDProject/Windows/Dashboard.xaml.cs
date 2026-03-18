using DatabaseCRUDProject.Services;
using DatabaseCRUDProject.UserControls;
using System.Windows;

namespace DatabaseCRUDProject.Windows
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
            ShowLoggedInUser();
            DashboardContent.Content = new HomeUC();
        }

        private void ShowLoggedInUser()
        {
            if (GlobalVars.CurrentUser == null)
            {
                LoggedInUserTextBlock.Text = "User: Not available";
                return;
            }

            LoggedInUserTextBlock.Text = $"User: {GlobalVars.CurrentUser.FirstName} {GlobalVars.CurrentUser.LastName}\nRole: {GlobalVars.CurrentUser.Role}";
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            DashboardContent.Content = new HomeUC();
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            DashboardContent.Content = new UsersManagementUC();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            DashboardContent.Content = new SettingsUC();
        }
    }
}

using DatabaseCRUDProject.Models;
using DatabaseCRUDProject.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace DatabaseCRUDProject.UserControls
{
    /// <summary>
    /// Interaction logic for UsersManagementUC.xaml
    /// </summary>
    public partial class UsersManagementUC : UserControl
    {
        private readonly ObservableCollection<User> _users = new ObservableCollection<User>();
        private readonly HashSet<User> _newUsers = new HashSet<User>();

        public UsersManagementUC()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            _users.Clear();
            foreach (var user in UserService.GetAllUsers())
            {
                _users.Add(user);
            }

            UsersDataGrid.ItemsSource = _users;
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            var newUser = new User(0, string.Empty, string.Empty, 0, string.Empty, string.Empty, string.Empty, string.Empty);
            _newUsers.Add(newUser);
            _users.Add(newUser);

            UsersDataGrid.SelectedItem = newUser; 
            UsersDataGrid.ScrollIntoView(newUser);
            UsersDataGrid.CurrentCell = new DataGridCellInfo(newUser, UsersDataGrid.Columns[0]);    
            UsersDataGrid.BeginEdit();
        }

        private void UsersDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Row.Item is not User user || _newUsers.Contains(user))
            {
                return;
            }

            Dispatcher.BeginInvoke(new System.Action(() =>
            {
                UserService.EditUser(user);
            }), DispatcherPriority.Background);
        }

        private void UsersDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.Row.Item is not User user || !_newUsers.Contains(user))
            {
                return;
            }

            Dispatcher.BeginInvoke(new System.Action(() =>
            {
                var created = UserService.AddUser(user);
                if (created != null)
                {
                    _newUsers.Remove(user);
                    return;
                }

                _users.Remove(user);
                _newUsers.Remove(user);
            }), DispatcherPriority.Background);
        }

        private void UsersDataGrid_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var depObj = e.OriginalSource as DependencyObject;
            while (depObj != null && depObj is not DataGridRow)
            {
                depObj = VisualTreeHelper.GetParent(depObj);
            }

            if (depObj is DataGridRow row)
            {
                row.IsSelected = true;
            }
        }

        private void DeleteSelectedUserMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is not User user)
            {
                return;
            }

            var deleted = UserService.DeleteUser(user.Ci);
            if (deleted != null)
            {
                _users.Remove(user);
                _newUsers.Remove(user);
            }
        }
    }
}

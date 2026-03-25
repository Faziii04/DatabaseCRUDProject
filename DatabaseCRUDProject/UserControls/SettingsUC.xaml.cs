using DatabaseCRUDProject.Services;
using System.Windows;
using System.Windows.Controls;

namespace DatabaseCRUDProject.UserControls
{
    /// <summary>
    /// Interaction logic for SettingsUC.xaml
    /// </summary>
    public partial class SettingsUC : UserControl
    {
        public SettingsUC()
        {
            InitializeComponent();
            LoadCurrentThemeState();
        }

        private void LoadCurrentThemeState()
        {
            if (GlobalVars.CurrentTheme == AppTheme.Dark)
            {
                DarkThemeRadioButton.IsChecked = true;
            }
            else
            {
                LightThemeRadioButton.IsChecked = true;
            }
        }

        private void LightThemeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            GlobalVars.SetCurrentTheme(AppTheme.Light);
        }

        private void DarkThemeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            GlobalVars.SetCurrentTheme(AppTheme.Dark);
        }
    }
}

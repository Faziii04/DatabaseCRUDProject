using DatabaseCRUDProject.Services;
using System;
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
            var current = GlobalVars.currentTheme ?? Application.Current.Resources.MergedDictionaries[0];
            string source = current.Source?.ToString() ?? string.Empty;

            if (source.Contains("DarkTheme.xaml", StringComparison.OrdinalIgnoreCase) ||
                source.Contains("DatkTheme.xaml", StringComparison.OrdinalIgnoreCase))
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
            ApplyTheme("Themes/LightTheme.xaml");
        }

        private void DarkThemeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ApplyTheme("Themes/DarkTheme.xaml");
        }

        private void ApplyTheme(string themePath)
        {
            ResourceDictionary theme = new ResourceDictionary
            {
                Source = new Uri(themePath, UriKind.Relative)
            };

            GlobalVars.SetCurrentTheme(theme);
        }
    }
}

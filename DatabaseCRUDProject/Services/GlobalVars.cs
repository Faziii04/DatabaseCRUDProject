using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using DatabaseCRUDProject.Models;
using DatabaseCRUDProject.Services;

namespace DatabaseCRUDProject.Services
{
    internal static class GlobalVars
    {
        internal static User? CurrentUser;
        internal static ResourceDictionary? currentTheme { get; private set; }

        internal static void SetCurrentUser(User user)
        {
            CurrentUser = user;
        }

        internal static void SetCurrentTheme(ResourceDictionary theme)
        {
            currentTheme = theme;

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(theme);
        }
    }
}

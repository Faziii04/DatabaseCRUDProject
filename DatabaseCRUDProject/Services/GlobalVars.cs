using System;
using System.Windows;
using System.Windows.Media;
using DatabaseCRUDProject.Models;

namespace DatabaseCRUDProject.Services
{
    internal enum AppTheme
    {
        Light,
        Dark
    }

    internal static class GlobalVars
    {
        internal static User? CurrentUser;
        internal static AppTheme CurrentTheme { get; private set; } = AppTheme.Light;

        internal static void SetCurrentUser(User user)
        {
            CurrentUser = user;
        }

        internal static void SetCurrentTheme(AppTheme theme)
        {
            CurrentTheme = theme;

            if (theme == AppTheme.Dark)
            {
                SetBrush("MainBackgroundBrush", "#0B1220");
                SetBrush("SidebarBackgroundBrush", "#030712");
                SetBrush("SidebarForegroundBrush", "#F9FAFB");
                SetBrush("ContentBackgroundBrush", "#111827");
                SetBrush("CardBackgroundBrush", "#1F2937");
                SetBrush("ControlBackgroundBrush", "#111827");
                SetBrush("PrimaryTextBrush", "#F9FAFB");
                SetBrush("SecondaryTextBrush", "#9CA3AF");
                SetBrush("BorderBrush", "#374151");
                SetBrush("AccentBrush", "#3B82F6");
                SetBrush("AccentHoverBrush", "#2563EB");
                return;
            }

            SetBrush("MainBackgroundBrush", "#F3F4F6");
            SetBrush("SidebarBackgroundBrush", "#F8FAFC");
            SetBrush("SidebarForegroundBrush", "#111827");
            SetBrush("ContentBackgroundBrush", "#FFFFFF");
            SetBrush("CardBackgroundBrush", "#FFFFFF");
            SetBrush("ControlBackgroundBrush", "#F9FAFB");
            SetBrush("PrimaryTextBrush", "#111827");
            SetBrush("SecondaryTextBrush", "#6B7280");
            SetBrush("BorderBrush", "#E5E7EB");
            SetBrush("AccentBrush", "#2563EB");
            SetBrush("AccentHoverBrush", "#1D4ED8");
        }

        private static void SetBrush(string key, string colorHex)
        {
            var color = (Color)ColorConverter.ConvertFromString(colorHex);

            Application.Current.Resources[key] = new SolidColorBrush(color);
        }
    }
}

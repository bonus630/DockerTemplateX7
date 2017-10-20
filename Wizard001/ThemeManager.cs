using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace Wizard001
{
    public enum VsTheme
    {
        Unknown = 0,
        Light,
        Dark,
        Blue
    }

    public class ThemeManager
    {
        private EnvDTE80.DTE2 dte;

        public ThemeManager(EnvDTE80.DTE2 dte)
        {
            this.dte = dte;
        }
        private static readonly IDictionary<string, VsTheme> Themes = new Dictionary<string, VsTheme>()
        {
            { "de3dbbcd-f642-433c-8353-8f1df4370aba", VsTheme.Light },
            { "1ded0138-47ce-435e-84ef-9ec1f439b749", VsTheme.Dark },
            { "a4d6a176-b948-4b29-8c66-53c97a1ed7d0", VsTheme.Blue }
         };

        public  VsTheme GetCurrentTheme()
        {
            string themeId = GetThemeId();
            if (string.IsNullOrWhiteSpace(themeId) == false)
            {
                VsTheme theme;
                if (Themes.TryGetValue(themeId, out theme))
                {
                    return theme;
                }
            }

            return VsTheme.Unknown;
        }

        public  string GetThemeId()
        {
            const string CategoryName = "General";
            const string ThemePropertyName = "CurrentTheme";
            string keyName = string.Format(@"Software\Microsoft\VisualStudio\{0}\{1}",dte.Version, CategoryName);

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName))
            {
                if (key != null)
                {
                    return (string)key.GetValue(ThemePropertyName, string.Empty);
                }
            }

            return null;
        }
    }
}

using System.Windows;

namespace $safeprojectname$.Styles
{
    public class StylesController
    {
        string currentTheme = "";
        ResourceDictionary Resources;
        Corel.Interop.VGCore.Application corelApp;
        /// <summary>
        /// Controller Styles of control, for more colors includes here in StyleKeys and Colors.xaml
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="app"></param>
        public StylesController(ResourceDictionary resource, Corel.Interop.VGCore.Application app)
        {
            Resources = resource;
            corelApp = app;
            corelApp.OnApplicationEvent += CorelApp_OnApplicationEvent;
        }
        #region controle style
        //Keys resources name follow the resource order to add a new value, order to works you need add 5 resources colors and Resources/Colors.xaml
        //1º is default, is the same name of StyleKeys string array
        //2º add LightestGrey. in start name of 1º for LightestGrey style in corel
        //3º MediumGrey
        //4º DarkGrey
        //5º Black
        private readonly string[] StyleKeys = new string[] {
         "TabControl.Static.Border",
         "TabItem.Static.Border" ,
         "TabItem.Disabled.Background",
         "TabItem.Selected.Background",
         "TabItem.Static.Background",
         "TabItem.Selected.MouseOver.Background" ,
         "TabItem.Static.MouseOver.Background",
         "Button.MouseOver.Background" ,
         "Button.MouseOver.Border",
         "Button.Static.Border" ,
         "Button.Static.Background" ,
         "Button.Pressed.Background" ,
         "Button.Pressed.Border" ,
         "Button.Disabled.Foreground",
         "Button.Disabled.Background",
         "Default.Static.Foreground" ,
         "Default.Static.Background",
         "Container.Text.Static.Background" ,
         "Container.Text.Static.Foreground" ,
         "Container.Static.Background" ,
         "Default.Static.Inverted.Foreground" ,
         "ComboBox.Border.Popup.Item.MouseOver"
        };

        private void LoadStyle(string name)
        {

            string style = name.Substring(name.LastIndexOf("_") + 1);
            for (int i = 0; i < StyleKeys.Length; i++)
            {
                this.Resources[StyleKeys[i]] = this.Resources[string.Format("{0}.{1}", style, StyleKeys[i])];
            }
        }
        private void CorelApp_OnApplicationEvent(string EventName, ref object[] Parameters)
        {
            if (EventName.Equals("WorkspaceChanged") || EventName.Equals("OnColorSchemeChanged"))
            {
                LoadThemeFromPreference();
            }
        }
        public void LoadThemeFromPreference()
        {
            try
            {
                string result = string.Empty;
#if X8
                 result = corelApp.GetApplicationPreferenceValue("WindowScheme", "Colors").ToString();
#endif
#if X9
                  result = corelApp.GetApplicationPreferenceValue("WindowScheme", "Colors").ToString();
#endif
#if X10
                  result = corelApp.GetApplicationPreferenceValue("WindowScheme", "Colors").ToString();
#endif
#if X11
                  result = corelApp.GetApplicationPreferenceValue("WindowScheme", "Colors").ToString();
#endif
                if (!result.Equals(currentTheme))
                {
                    if (!result.Equals(string.Empty))
                    {
                        currentTheme = result;
                        LoadStyle(currentTheme);
                    }
                }
            }
            catch { }
        }
        #endregion
    }
}




using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using corel = Corel.Interop.VGCore;

namespace $safeprojectname$
{
  
    public partial class ControlUI : UserControl
    {
        private corel.Application corelApp;
        private string currentTheme;
        public ControlUI(object app)
        {
            InitializeComponent();
            try
            {
                this.corelApp = app as corel.Application;
                this.corelApp.OnApplicationEvent += CorelApp_OnApplicationEvent;
            }
            catch (Exception)
            {
                global::System.Windows.MessageBox.Show("VGCore Erro");
            }
            btn_Command.Click += (s, e) => { corelApp.MsgShow("Working"); };
        }
    #region theme select
    //Keys resources name follow the resource order to add a new value, order to works you need add 5 resources colors and Resources/Colors.xaml
    //1º is default, is the same name of StyleKeys string array
    //2º add LightestGrey. in start name of 1º for LightestGrey style in corel
    //3º MediumGrey
    //4º DarkGrey
    //5º Black
    private readonly string[] StyleKeys = new string[] {
         "Button.MouseOver.Background" ,
         "Button.MouseOver.Border",
         "Button.Static.Border" ,
         "Button.Static.Background" ,
         "Button.Pressed.Background" ,
         "Button.Pressed.Border" ,
         "Button.Disabled.Foreground",
         "Button.Disabled.Background",
         "Default.Static.Foreground" ,
         "Container.Text.Static.Background" ,
         "Container.Text.Static.Foreground" ,
         "Container.Static.Background" ,
         "Default.Static.Inverted.Foreground" ,
         "ComboBox.Border.Popup.Item.MouseOver" 
  };
    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        LoadThemeFromPreference();
    }
    public void LoadStyle(string name)
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

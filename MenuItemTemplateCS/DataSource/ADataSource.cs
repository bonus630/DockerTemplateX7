using System;
using System.Collections.Generic;
using Corel.Interop.VGCore;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Interop;
using System.Diagnostics;

namespace $safeprojectname$.DataSource
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class $safeprojectname$DataSource : BaseDataSource
    {
  
        private string caption = "$Caption$";
        private string icon = "guid://$GuidA$";

        public $safeprojectname$DataSource(DataSourceProxy proxy) : base(proxy)
        {
            
        }

        // You can change caption/icon dynamically setting a new value here 
        //or loading the value from resource specifying the id of the caption/icon 
        public string Caption
        {
            get { return caption; }
            set { caption = value; NotifyPropertyChanged(); }
        }
        public string Icon
        {
            get { return icon; }
            set { icon = value; NotifyPropertyChanged(); }
        }

        public void MenuItemCommand()
        {
            ControlUI.corelApp.MsgShow("Working");
        }
    }

}

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

        public $safeprojectname$DataSource(DataSourceProxy proxy) : base(proxy)
        {
            
        }
        public string Caption
        {
            get { return caption; }
            set { caption = value; NotifyPropertyChanged(); }
        }
        public void MenuItemCommand()
        {
            ControlUI.corelApp.MsgShow("Working");
        }
  
       
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Corel.Interop.VGCore;

namespace $safeprojectname$.DataSource
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class BaseDataSource : INotifyPropertyChanged
    {
        protected DataSourceProxy m_AppProxy;

        public BaseDataSource(DataSourceProxy proxy)
        {
            this.m_AppProxy = proxy;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            try
            {
                this.m_AppProxy.UpdateListeners(propertyName);
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            catch { }
        }
    }
}

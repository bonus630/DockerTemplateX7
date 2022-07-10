using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using corel = Corel.Interop.VGCore;

namespace $safeprojectname$
{
  
    public partial class ControlUI : UserControl
    {
        public static corel.Application corelApp;
        
        public ControlUI(object app)
        {
            
            try
            {
                corelApp = app as corel.Application;
                var dsf = new DataSource.DataSourceFactory();
                dsf.AddDataSource("$safeprojectname$DS", typeof(DataSource.$safeprojectname$DataSource));
                dsf.Register();

        }
            catch 
            {
                global::System.Windows.MessageBox.Show("VGCore Erro");
            }
           
        }


  
    }
}

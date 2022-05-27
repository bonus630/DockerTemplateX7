using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectHelper
{
    public class VersionsModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Installed { get; set; }

        public VersionsModel(int ID,string Name,bool Installed)

        {
            this.ID = ID;
            this.Name = Name;
            this.Installed = Installed;
        }

    }
}

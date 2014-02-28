using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Post_Office;

namespace AWGP3Squared.Core
{
    interface IModuleManager
    {
        private List<IModule> moduleList;
        private IPostOffice postOffice;

        public void init();

        public bool readModulesFromXML(String filename);

        public bool processMessages();
    }
}

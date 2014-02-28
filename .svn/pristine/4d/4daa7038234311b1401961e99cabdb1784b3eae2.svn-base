using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Post_Office;
using AWGP3Squared.Helper;

namespace AWGP3Squared.Interfaces
{
    interface IModule
    {
        //private 
        Constant.enumModuleID m_ModuleID { get; }
        Constant.enumMessage[] m_MsgTypeArray{ get;}
        //TODO: Needs A Constructor Like So: public IModule(IPostOffice pOff);

        void Init();

        void Notify(Boolean firstPass);

        void PrintError(String errmssg);

        //void ProcessCommands(Constant.enumMessage[] commands);

        //IModule getThis();
    }
}

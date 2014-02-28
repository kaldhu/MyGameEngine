using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Post_Office;

namespace AWGP3Squared.Core
{
    interface IModule
    {
        //private IPostOffice m_PostOff;

        //TODO: Needs A Constructor Like So: public IModule(IPostOffice pOff);

        void init();

        void checkMessage();

        void processMessages(IMessage[] messages);

        IModule getThis();
    }
}

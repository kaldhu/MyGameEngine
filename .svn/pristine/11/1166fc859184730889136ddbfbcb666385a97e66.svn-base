using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Core;


namespace AWGP3Squared.Post_Office
{
    interface IPostOffice
    {

        //private static List<IMessage> m_POBox;
        //private static PostOffice instance;

        //TODO: Protected PostOffice();

        void init();

        IObserver iObserver
        {
            get;
            set;
        }

        void sendmessage(IMessage msg);

        IMessage[] deliverMessages(IModule module);
    }
}
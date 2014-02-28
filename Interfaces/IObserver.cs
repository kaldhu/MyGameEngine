using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGP3Squared.Interfaces
{
    interface IObserver
    {
        //SETUP : This is to be a singleton
        bool Attach(IModule module);
        bool Dettach(IModule module);

        void Notify(Boolean firstPass);
    }
}

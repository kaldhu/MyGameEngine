using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AWGP3Squared.Helper;

namespace AWGP3Squared.Interfaces
{

    // private static ComponentType instance;
    // protected ComponentType(int iD, String name) 
    // static Postoffice Instance(int iD, String name);
    // these must be implemented into componenttype for singleton
    interface IComponentType
    {
        Constant.enumComponent m_ComponentTypeID{get;}

    }
}

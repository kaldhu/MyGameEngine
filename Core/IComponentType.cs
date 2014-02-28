using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGP3Squared.Core
{
    interface IComponentType
    {
        int m_ComponentID
        {
            get;
        }

        String m_TypeName
        {
            get;
        }
    }
}

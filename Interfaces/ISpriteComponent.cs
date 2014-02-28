using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGP3Squared.Interfaces
{
    interface ISpriteComponent : IComponent
    { 
        string m_SpriteID { get; }
        bool m_isDrawn { get; }
        void SwitchDrawState();
    }
}

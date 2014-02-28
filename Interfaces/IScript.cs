using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGP3Squared.Interfaces
{
    interface IScript //: IComponent
    {
        IScriptController m_ScriptController { get; set; }
        IGameObject m_GameObject { get; set; }
        void Init();
        void Update();
        void Draw();
    }
}

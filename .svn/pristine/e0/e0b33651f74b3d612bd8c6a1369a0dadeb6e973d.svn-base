using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;

namespace AWGP3Squared.Components
{
    class ScriptComponent : IScriptComponent
    {
        public string m_GameObjectkey { get; private set; }
        public IComponentType m_ComponentType { get; private set; }
        public int m_GameObjectId { get; set; }
        public IScript m_Script {get;private set;}

        public ScriptComponent(int gameObjectId, String gameObjectkey, IScript script)
        {
            m_ComponentType = ComponentType.Instance(Constant.enumComponent.SCRIPT);
            m_Script = script;
            m_GameObjectId = gameObjectId;
            m_GameObjectkey = gameObjectkey;
        }

    }
}

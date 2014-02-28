using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;
using AWGP3Squared.Maths;

namespace AWGP3Squared.Components
{
    class SpriteComponent : ISpriteComponent
    {
        public int m_GameObjectId { get; set; }
        public string m_SpriteID { get; private set; }
        public bool m_isDrawn { get; private set; }

        public IComponentType m_ComponentType { get; private set; }

        public SpriteComponent(string spriteID)
        {
            m_ComponentType = ComponentType.Instance(Constant.enumComponent.SPRITE);
            m_SpriteID = spriteID;
            m_isDrawn = false;
        }

        public void SwitchDrawState()
        {
            m_isDrawn = !m_isDrawn;
        }
    }
}

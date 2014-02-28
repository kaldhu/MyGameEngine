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
    class ModelComponent : IComponent
    {
        public int m_GameObjectId { get;  set; }
        public IComponentType m_ComponentType { get; private set; }   // ComponentType of Component should be set in constructors
        public int m_ModelID { get; private set; }
        public int m_TextureID { get; private set; } 
        /*
        public int GetModelID()
        {
            return m_ModelID;
        }
        public int GetTextureID()
        {
            return m_TextureID;
        }
         * */
    }
}

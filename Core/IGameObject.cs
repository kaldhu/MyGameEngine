using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGP3Squared.Core
{
    interface IGameObject
    {
        List<IComponent> m_Components
        {
            get;
        }

        bool AddComponent(IComponent component);
        bool RemoveComponent(IComponent component);

        IComponent getComponent(IComponent component);
    }
}

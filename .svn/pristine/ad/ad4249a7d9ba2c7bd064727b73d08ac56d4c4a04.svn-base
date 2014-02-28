using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Components;
using AWGP3Squared.Helper;

namespace AWGP3Squared.Interfaces
{
    //NOTE: No Longer IModule
    interface IGameObject
    {
        int m_GameObjectId { get; }
        String m_GameObjectKey { get; }
        List<IComponent> m_Components
        {
            get;
        }

        bool AddComponent(IComponent component);
        bool AddComponent(IComponent[] component);
        bool RemoveComponent(IComponent component);

        IComponent getComponent(IComponentType componentType);
        IComponent getComponent(Constant.enumComponent componentID);
        T getComponent<T>(Constant.enumComponent componentID);
    }
}

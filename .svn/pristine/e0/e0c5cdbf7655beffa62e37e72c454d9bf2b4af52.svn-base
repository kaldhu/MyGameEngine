using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Components;
using AWGP3Squared.Post_Office;
using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;
using AWGP3Squared.Maths;

namespace AWGP3Squared.Core
{
    class GameObject :IGameObject
    {
        private static List<bool> m_UsedCounter = new List<bool>();
        private static List<String> m_KeyList = new List<String>();
        public List<IComponent> m_Components { get; private set; }
        public int m_GameObjectId { get; private set; }
        public String m_GameObjectKey { get; private set; }

        public GameObject(String key, Vector3 vector)
        {
            m_Components = new List<IComponent>();

            int nextIndex = GetAvailableIndex();
            if (nextIndex == -1)
            {
                nextIndex = m_UsedCounter.Count;
                m_UsedCounter.Add(true);
            }

            if (!m_KeyList.Contains(key))
            {
                m_GameObjectId = nextIndex;
                m_GameObjectKey = key;
                m_KeyList.Add(key);
                AddComponent(new PositionComponent3D(vector));
            }
        }

        public GameObject(String key, float x = 0.0f, float y = 0.0f, float z = 0.0f)
        {
            m_Components = new List<IComponent>();

            int nextIndex = GetAvailableIndex();
            if (nextIndex == -1)
            {
                nextIndex = m_UsedCounter.Count;
                m_UsedCounter.Add(true);
            }

            if (!m_KeyList.Contains(key))
            {
                m_GameObjectId = nextIndex;
                m_GameObjectKey = key;
                m_KeyList.Add(key);
                AddComponent(new PositionComponent3D(x, y, z));
            }
        }

        public void Dispose()
        {
            m_UsedCounter[m_GameObjectId] = false;
        }
        
        private int GetAvailableIndex()
        {
            for (int i = 0; i < m_UsedCounter.Count; i++)
            {
                if (m_UsedCounter[m_GameObjectId] == false)
                {
                    return i;
                }
            }
            // Nothing available.
            return -1;
        }


        public bool AddComponent(IComponent component)
        {
            try
            {               
                m_Components.Add(component);
                component.m_GameObjectId = m_GameObjectId;
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("GameObject AddComponent Error :" + e.ToString());
                return false;
            }
        }

        public bool AddComponent(IComponent[] componentArray)
        {
            try
            {
                m_Components.AddRange(componentArray);
                foreach (IComponent component in componentArray)
                {
                    component.m_GameObjectId = m_GameObjectId;
                }
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("GameObject AddComponent Error :" + e.ToString());
                return false;
            }
        }

        public bool RemoveComponent(IComponent component)
        {
            try
            {
                m_Components.Remove(component);
                component.m_GameObjectId = -1;
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("GameObject removeComponent Error :" + e.ToString());
                return false;
            }
        }

        public IComponent getComponent(IComponentType componentType)
        {
            return m_Components.Where(x => x.m_ComponentType == componentType).FirstOrDefault();
        }

        public IComponent getComponent(Constant.enumComponent componentID )
        {
            return m_Components.Where(x => x.m_ComponentType.m_ComponentTypeID == componentID).FirstOrDefault();            
        }
        
        public T getComponent<T>(Constant.enumComponent componentID)
        {
            return (T)m_Components.Where(x => x.m_ComponentType.m_ComponentTypeID == componentID).FirstOrDefault();
        }
    }
}
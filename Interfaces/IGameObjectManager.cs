using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Components;
using AWGP3Squared.Helper;

namespace AWGP3Squared.Interfaces
{
    interface IGameObjectManager : IModule
    {
        
        //void CollisionDetection();
        //bool AddObject(String key, IGameObject gameObject);
        //bool AddComponentToObject(String key, IComponent component);
        bool RemoveObject(String Key);

    }
}

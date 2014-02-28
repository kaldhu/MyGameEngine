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
    class PositionComponent3D : IPositionComponent3D
    {
        //TODO PHILL & LUKE : do we need a 2d and 3d version of this and can we add rotation and scale to this
        //TODO LUKE : is it possible to make this an instance so only one poscomponent can be on a gameobject.

        //NOTICE: Every GameObj has this component added at gameobject creation

        public int m_GameObjectId { get;  set; }
        public IComponentType m_ComponentType { get; private set; }   // ComponentType of Component should be set in constructors
        public Vector3 m_Position { get; set; }

        public PositionComponent3D(float x, float y, float z)
        {
            m_ComponentType = ComponentType.Instance(Constant.enumComponent.POSITIONCOMPONENT3D);
            m_Position = new Vector3(x, y, z);
        }
        public PositionComponent3D(Vector3 vector)
        {
            m_ComponentType = ComponentType.Instance(Constant.enumComponent.POSITIONCOMPONENT3D);
            m_Position = vector;
        }
    }
}

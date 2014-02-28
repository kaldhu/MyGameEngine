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
    class Collider : ICollider
    {
        public int m_GameObjectId { get;  set; }
        public IComponentType m_ComponentType   { get; private set; }   // ComponentType of collider set in constructors
        public Boolean m_Trigger                { get; private set; }   // the collider is used for triggering events and is ignored by the physics engine
        public IComponent m_Material            { get; set; }           // physics material
        public Vector3 m_Centre                 { get; private set; }   // position of collider in objects local space
        public Vector3 m_Size                   { get; private set; }   // size of the collider for a box in terms of x,y and z
        public float? m_Radius                   { get; private set; }   // size of the collider for a sphere



        public Collider(Vector3 size,Boolean trigger = false, IComponent material = null, Vector3 centre = null )
        {
            m_ComponentType = ComponentType.Instance(Constant.enumComponent.COLLIDER);
            m_Trigger = trigger;
            m_Material = material;
            m_Centre = centre != null ? centre : new Vector3();
            m_Size = size;
            m_Radius = null;
        }
        public Collider(float radius,Boolean trigger = false, IComponent material = null, Vector3 centre = null)
        {
            m_ComponentType = ComponentType.Instance(Constant.enumComponent.COLLIDER);
            m_Trigger = trigger;
            m_Material = material;
            m_Centre = centre != null ? centre : new Vector3();
            m_Radius = radius; 
            m_Size = null;
        }


      
    }
}

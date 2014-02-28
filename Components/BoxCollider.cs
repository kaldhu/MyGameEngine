using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Core;
using AWGP3Squared.Maths;

namespace AWGP3Squared.Components
{
    class Collider : IColliderComponent
    {
        private Boolean m_Trigger;      // the collider is used for triggering events and is ignored by the physics engine
        private IComponent m_Material;  // physics material
        private Vector3 m_Centre;       // position of collider in objects local space
        private Vector3 m_Size;         // size of the collider for a box in terms of x,y and z
        private float m_Radius;         // size of the collider for a sphere

        public Collider(Vector3 size,Boolean trigger = false, IComponent material = null, Vector3 centre = null )
        {
            m_Trigger = trigger;
            m_Material = material;
            m_Centre = centre != null ? centre : new Vector3();
            m_Size = size;
        }
        public Collider( float radius,Boolean trigger = false, IComponent material = null, Vector3 centre = null)
        {
            m_Trigger = trigger;
            m_Material = material;
            m_Centre = centre != null ? centre : new Vector3();
            m_Radius = radius;
        }

        public Boolean Trigger
        {
            get
            {
                return m_Trigger;
            }
        }
\
        public IComponent Material
        {
            get
            {
                return m_Material;
            }
            set
            {
                m_Material = value;
            }
        }

        public Vector3 Centre
        {
            get
            {
                return m_Centre;
            }
            set
            {
                m_Centre = value;
            }
        }
       
        public Vector3 Size
        {
            get
            {
                return m_Size;
            }
            set
            {
                m_Size = value;
            }
        }
        public float Radius
        {
            get
            {
                return m_Radius;
            }
            set
            {
                m_Radius = value;
            }
        }
    }
}

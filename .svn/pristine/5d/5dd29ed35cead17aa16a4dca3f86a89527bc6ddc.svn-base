using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Maths;

namespace AWGP3Squared.Interfaces
{
    interface ICollider : IComponent
    {
        Boolean m_Trigger { get; }   // the collider is used for triggering events and is ignored by the physics engine
        IComponent m_Material { get; set; }           // physics material
        Vector3 m_Centre { get; }   // position of collider in objects local space
        Vector3 m_Size { get; }   // size of the collider for a box in terms of x,y and z
        float? m_Radius { get; }   // size of the collider for a sphere
        /*
         * TODO Method need to run when message is sent to inform of collisions
        Boolean OnCollisionEnter();
        Boolean OnCollisionExit();
        Boolean OnCollisionStay();
        Boolean OnTriggerEnter(ICollider collider);
        Boolean OnTriggerExit(ICollider collider);
        Boolean OnTriggerStay(ICollider collider);

         */
    }
}

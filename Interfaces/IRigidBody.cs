using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Core;
using AWGP3Squared.Helper;
using AWGP3Squared.Maths;

namespace AWGP3Squared.Interfaces
{
    interface IRigidBody : IComponent
    {
         float m_Mass { get; set; }                           // amount of mass the object will have
         float m_Drag { get; set; }                           // amount of air resistance affects the object when moving via forces
         float m_AngularDrag { get; set; }                    // amount of air resistance affects the object when rotating from torque
         Boolean m_Gravity { get; set; }                      // if true object is affected by the global gravity setting
         Boolean m_Kinematic { get; set; }                    // if true the object will not be affected by forces
         Constant.enumCollisionDetection m_CollisionDetection { get; set; }               // determines what sort of CD the object uses (see above)
         Vector3 m_FreezePosition { get; set; }               // stops the movement of an object in x,y and z plane selectively (if a value within this vector3 is >0 the object will not move within that plane)
         Vector3 m_FreezeRotation { get; set; }               // stops the rotation of an object in x,y and z plane selectively (if a value within this vector3 is >0 the object will not rotate along that plane)
         Vector3 m_Position { get; set; }                       // Matches PositionComponent3D used to reduce calls for component
         Vector3 m_Rotation { get; set; }
         Vector3 m_Velocity { get; set; }
         Vector3 m_Acceleration { get; set; }
         Vector3 m_PreviousFrameAcceleration { get; set; }
         Vector3 m_PreviousFrameVelocity { get; set; }
         Vector3 m_PreviousFramePosition { get; set; }

         void AddForce(Vector3 force);
         void AddForce(float x = 0, float y = 0, float z = 0);
         void ChangePos(float x = 0, float y = 0, float z = 0);
             
         void Update();
    }
}

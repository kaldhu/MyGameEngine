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
    class RigidBody : IRigidBody
    {
        public int m_GameObjectId { get;  set; }
        public IComponentType m_ComponentType { get; private set; } // ComponentType of component set in constructors
        public float m_Mass { get; set; }                           // amount of mass the object will have
        public float m_Drag { get; set; }                           // amount of air resistance affects the object when moving via forces
        public float m_AngularDrag { get; set; }                    // amount of air resistance affects the object when rotating from torque
        public Boolean m_Gravity { get; set; }                      // if true object is affected by the global gravity setting
        public Boolean m_Kinematic { get; set; }                    // if true the object will not be affected by forces
        public Constant.enumCollisionDetection m_CollisionDetection { get; set; } // determines what sort of CD the object uses (see above)
        public Vector3 m_FreezePosition { get; set; }               // stops the movement of an object in x,y and z plane selectively (if a value within this vector3 is >0 the object will not move within that plane)
        public Vector3 m_FreezeRotation { get; set; }               // stops the rotation of an object in x,y and z plane selectively (if a value within this vector3 is >0 the object will not rotate along that plane)
        public Vector3 m_Position { get; set; }                     // Matches PositionComponent3D used to reduce calls for component
        public Vector3 m_Rotation { get; set; }                     // Matches XXXX used to reduce calls for component TODO: add rotation to posistionComponent
        public Vector3 m_Velocity { get; set; }                     // Velocity of object as a vector3
        public Vector3 m_Acceleration { get; set; }                // acceleration of object as a vector3
        public Vector3 m_PreviousFrameAcceleration { get;  set; }                // acceleration of object as a vector3
        public Vector3 m_PreviousFrameVelocity { get; set; }
        public Vector3 m_PreviousFramePosition { get; set; }


        public RigidBody(float mass = 0.0f, float drag = 0.0f, float angularDrag = 0.0f, Boolean gravity = false, Boolean kinematic = false,
                        Constant.enumCollisionDetection collisionDetection = Constant.enumCollisionDetection.DISCRETE_CD, Vector3 freezePos = null, Vector3 freezeRot = null)
        {
            m_Mass = mass;
            if (drag >= 0.0f && drag <= 1.0f)
            {
                m_Drag = drag;
            }
            else
            {
                if (drag < 0.0f)
                    m_Drag = 0.0f;
                if (drag > 1.0f)
                    m_Drag = 1.0f;
            }
            if (angularDrag >= 0.0f && angularDrag <= 1.0f)
            {
                m_AngularDrag = angularDrag;
            }
            else
            {
                if (angularDrag < 0.0f)
                    m_AngularDrag = 0.0f;
                if (angularDrag > 1.0f)
                    m_AngularDrag = 1.0f;
            }
            m_Gravity = gravity;
            m_Kinematic = kinematic;
            m_CollisionDetection = collisionDetection;
            m_FreezePosition = freezePos == null ? new Vector3() : freezePos;
            m_FreezeRotation = freezeRot == null ? new Vector3() : freezeRot;
            m_ComponentType = ComponentType.Instance(Constant.enumComponent.RIGIDBODY);
            m_Position = new Vector3();
            m_Rotation = new Vector3();
            m_Velocity = new Vector3();
            m_Acceleration = new Vector3();
            m_PreviousFrameAcceleration = new Vector3();
            m_PreviousFrameVelocity = new Vector3();
            m_PreviousFramePosition = new Vector3();

        }

        public void AddForce(Vector3 force)
        {
            //A=f/m

            // accel = ;

            m_Acceleration.AddScalarVector(force, 1 / m_Mass);
            //V=U+at
            //m_Acceleration = accel;
            //Velocity.AddScalarVector(accel, ((float)Constant.elapsedTime));
        }
        public void AddForce(float x = 0, float y = 0, float z = 0)
        {
            //A=f/m
            //Vector3 accel = m_Acceleration;
            Vector3 force = new Vector3(x, y, z);
            m_Acceleration.AddScalarVector(force, 1 / m_Mass);
            //V=U+at
            // m_Acceleration = accel;
            //Velocity.AddScalarVector(accel, ((float)Constant.elapsedTime));
        }

        public void ChangePos(float x = 0, float y = 0, float z = 0)
        {
            this.m_Position = new Vector3(x, y, z);
        }

        public void Update()
        {
            m_PreviousFrameAcceleration = m_Acceleration;
            m_PreviousFrameVelocity = m_Velocity;
            m_PreviousFramePosition = m_Position;
            if (m_Kinematic)
            {

                if (m_Acceleration != new Vector3())
                {
                    m_Acceleration = m_Acceleration.ReturnMultipleByScalar((float)Constant.elapsedTime);
                }
                if (m_Drag > 0.0f)
                {
                    Vector3 Temp = m_Velocity.ReturnMultipleByScalar(m_Drag);
                    m_Acceleration.SubtractVector(Temp);
                }

                m_Velocity.AddScalarVector(m_Acceleration, ((float)Constant.elapsedTime));
                m_Position.AddScalarVector(m_Velocity, (float)Constant.elapsedTime);


                if (m_Gravity)
                {
                    m_Acceleration = Constant.GetGravity();
                }
                else
                {
                    m_Acceleration = new Vector3();
                }
            }
        }

    }
}

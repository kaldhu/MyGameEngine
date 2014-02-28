using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;
using AWGP3Squared.Maths;

namespace AWGP3Squared.PhysicsModule
{
    class Physics : IPhysics
    {
        private static Physics m_Instance;
        private IPostOffice m_PostOffice { get;  set; }

        public Constant.enumModuleID m_ModuleID { get; private set; }
        public Constant.enumMessage[] m_MsgTypeArray { get; private set; }
        private Dictionary<int, List<int>> collisions { get; set; }

        protected Physics(IPostOffice postOffice)
        {
            collisions = new Dictionary<int, List<int>>();
            m_ModuleID = Constant.enumModuleID.PHYSICS;
            m_PostOffice = postOffice;
            m_MsgTypeArray= new Constant.enumMessage[] 
            {
                Constant.enumMessage.UPDATE,
                Constant.enumMessage.INITIALISE,
                Constant.enumMessage.SEND_COLLISIONDETECTIONDATA,
                Constant.enumMessage.SEND_POSITIONCOMPONENTS,
                Constant.enumMessage.SEND_COMPONENTS,
                Constant.enumMessage.COLLISIONDETECTION,
            };            
        }

        public static Physics Instance(IPostOffice postOffice)
        {
            if (m_Instance == null)
            {
                m_Instance = new Physics(postOffice);
            }
            
            return m_Instance;
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void Notify(Boolean firstPass)
        {
            if (firstPass)
            {
                m_PostOffice.CheckMessages(m_ModuleID, m_MsgTypeArray);
            }
            else
            {
                ProcessCommands(m_PostOffice.getMessageTypeIDs(m_ModuleID));
            }      
        }

        private void ProcessCommands(Constant.enumMessage[] commands)
        {

            foreach (Constant.enumMessage c in commands)
            {
                m_PostOffice.LockMessageType(c);
                m_PostOffice.RemoveMessage(c,m_ModuleID);
                switch (c)
                {
                    case (Constant.enumMessage.UPDATE):
                        Update();
                        break;
                    case (Constant.enumMessage.COLLISIONDETECTION):
                        CollisionDetection();
                        break;
                    default:
                        break;
                }

                m_PostOffice.UnLockMessageType(c);
            }
        }
        

        private void Update()
        {                 
            UpdateRigidBodies();
        }

        private void UpdateRigidBodies()
        {
            m_PostOffice.SendMessage(Constant.enumMessage.GET_COMPONENTS, new String[] { Constant.enumComponent.RIGIDBODY.ToString()});

            IRigidBody[] rigidBodiesArray = Methods.GetObjectArrayOf<IComponent>(m_PostOffice.GetMessageObjects(m_ModuleID, Constant.enumMessage.SEND_COMPONENTS)).OfType<IRigidBody>().ToArray();
            foreach (IRigidBody rigidBody in rigidBodiesArray)
            {
                rigidBody.Update();
            }
        }

        private void CollisionDetection()
        {
            Vector3 collPos = new Vector3();

            //m_PostOffice.SendMessage(Constant.enumMessage.GET_COLLISIONDETECTIONDATA);
            m_PostOffice.SendMessage(Constant.enumMessage.GET_COMPONENTS, new String[] { Constant.enumComponent.RIGIDBODY.ToString(), Constant.enumComponent.COLLIDER.ToString(), Constant.enumComponent.POSITIONCOMPONENT3D.ToString() });

            IComponent[] compArray =  Methods.GetObjectArrayOf<IComponent>(m_PostOffice.GetMessageObjects(m_ModuleID, Constant.enumMessage.SEND_COMPONENTS));

            IRigidBody[] rigidBodiesArray = compArray.OfType<IRigidBody>().ToArray();
            ICollider[] collidersArray = compArray.OfType<ICollider>().ToArray();
            IPositionComponent3D[] PositionComponentsArray = compArray.OfType<IPositionComponent3D>().ToArray();

            Dictionary<int, Vector3> colliderPositionDictionary = new Dictionary<int, Vector3>();
            foreach (IPositionComponent3D PosComp in PositionComponentsArray)
            {
                colliderPositionDictionary.Add(PosComp.m_GameObjectId, PosComp.m_Position);
            }

            //go through each rigidbody with a collider and check which collision dectection is needed 
            foreach (IRigidBody rigBody in rigidBodiesArray)
            {
                ICollider[] rigidBodiesColliders = collidersArray.Where(col => col.m_GameObjectId == rigBody.m_GameObjectId).ToArray();
                switch (rigBody.m_CollisionDetection)
                {
                    case Constant.enumCollisionDetection.DISCRETE_CD:

                        foreach (ICollider coll in collidersArray)
                        {
                           
                                if (coll.m_GameObjectId != rigBody.m_GameObjectId)
                                {
                                    //Vector3 collPos = PositionComponents.Where(pos=>pos.m_GameObjectId==coll.m_GameObjectId).Select(pos=>pos.m_Position).FirstOrDefault();
                                    collPos = colliderPositionDictionary[coll.m_GameObjectId];
                                    if (DiscreteCD(rigidBodiesColliders, rigBody.m_Position, coll, collPos))
                                    {
                                        if (!collisions.ContainsKey(rigBody.m_GameObjectId) || !collisions[rigBody.m_GameObjectId].Contains(coll.m_GameObjectId))
                                        {
                                            Vector3[] rigBody1Vectors = new Vector3[2];
                                            Vector3[] rigBody2Vectors = new Vector3[2];
                                            IRigidBody rigBody2 = rigidBodiesArray.Where(rb => rb.m_GameObjectId == coll.m_GameObjectId).FirstOrDefault();
                                            if (rigBody2 != null)
                                            {

                                                Vector3[] newVelocitesArray = calculateMomemtumAfterCollision(rigBody.m_Mass, rigBody.m_Velocity, rigBody2.m_Mass, rigBody2.m_Velocity);
                                                if (rigBody.m_Kinematic)
                                                    rigBody.m_Velocity = newVelocitesArray[0];
                                                if (rigBody2.m_Kinematic)
                                                    rigBody2.m_Velocity = newVelocitesArray[1];


                                                if (!collisions.ContainsKey(rigBody.m_GameObjectId))
                                                {
                                                    collisions.Add(rigBody.m_GameObjectId, new List<int>() { rigBody2.m_GameObjectId });
                                                }
                                                else if (!collisions[rigBody.m_GameObjectId].Contains(rigBody2.m_GameObjectId))
                                                {
                                                    collisions[rigBody.m_GameObjectId].Add(rigBody2.m_GameObjectId);
                                                }
                                                if (!collisions.ContainsKey(rigBody2.m_GameObjectId))
                                                {
                                                    collisions.Add(rigBody2.m_GameObjectId, new List<int>() { rigBody.m_GameObjectId });
                                                }
                                                else if (!collisions[rigBody2.m_GameObjectId].Contains(rigBody.m_GameObjectId))
                                                {
                                                    collisions[rigBody2.m_GameObjectId].Add(rigBody.m_GameObjectId);
                                                }
                                                /*
                                                float collisionAccuracy = 10.0f;
                                                for (float i = collisionAccuracy-1; i > 0; i--)
                                                {
                                                    float factor = i / collisionAccuracy;
                                                    rigBody1Vectors = timeStep(rigBody, factor);
                                                    rigBody2Vectors = timeStep(rigBody2, factor);
                                                    if (!DiscreteCD(rigidBodiesColliders, rigBody1Vectors[1], coll, rigBody2Vectors[1]))
                                                        break;
                                                }
            */

                                            }
                                            int[] GameObjectArrayId = new int[] { coll.m_GameObjectId, rigBody.m_GameObjectId };
                                            m_PostOffice.SendMessage(Constant.enumMessage.COLLISIONDETECTIONDATA, new String[] { rigBody.m_GameObjectId.ToString(), coll.m_GameObjectId.ToString() }, false);
                                        }
                                    }
                                    else if (collisions!=null)
                                    {
                                        if (collisions.ContainsKey(rigBody.m_GameObjectId) && collisions[rigBody.m_GameObjectId].Contains(coll.m_GameObjectId))
                                        {
                                            collisions.Remove(coll.m_GameObjectId);
                                        }
                                    }
                            }
                        }

                    break;
                            /*
                    case Constant.enumCollisionDetection.CONTINUOUS_CD:
C:\Users\Yosef\Uni\test\AWGP3Squared\Core\GameObject.cs
                        foreach (ICollider coll in collidersArray)
                        {
                            if (coll.m_GameObjectId != rigBody.m_GameObjectId)
                            {
                                Constant.enumCollisionDetection[] temp = rigidBodiesArray.Where(rig => rig.m_GameObjectId == coll.m_GameObjectId)
                                    .Select(rig => rig.m_CollisionDetection).ToArray();

                                if (temp.Count()==0 || !temp.Contains(Constant.enumCollisionDetection.DISCRETE_CD))
                                { 
                                    //Vector3 collPos = PositionComponents.Where(pos=>pos.m_GameObjectId==coll.m_GameObjectId).Select(pos=>pos.m_Position).FirstOrDefault();
                                    collPos = colliderPositionDictionary[coll.m_GameObjectId];
                                    ContinuousCD(rigidBodiesColliders, rigBody.m_Position, coll, collPos);
                                }
                                else
                                {
                                    //Vector3 collPos = PositionComponents.Where(pos => pos.m_GameObjectId == coll.m_GameObjectId).Select(pos => pos.m_Position).FirstOrDefault();
                                    collPos = colliderPositionDictionary[coll.m_GameObjectId];
                                    DiscreteCD(rigidBodiesColliders, rigBody.m_Position, coll, collPos);
                                }
                            }
                        }

                        break;
                        */
                    default:
                        break;
                }
            }
        }
        private bool DiscreteCD(ICollider[] obj1Colliders,Vector3 obj1Pos, ICollider obj2Collider, Vector3 obj2Pos)
        {
            bool collision = false;
            //Get obj2 colliders center pos in world cords not objectCord
            Vector3 obj2colliderPos = obj2Collider.m_Centre.ReturnAddVector(obj2Pos);

            foreach (ICollider obj1Collider in obj1Colliders)
            {
                //Get obj1 colliders center pos in world cords not objectCord
                Vector3 obj1colliderPos = obj1Collider.m_Centre.ReturnAddVector(obj1Pos);

                if (obj1Collider.m_Size!=null)
                {
                    //two boxes
                    if (obj2Collider.m_Size!=null)
                    {
                        return BoxBoxCollision(obj1colliderPos, obj1Collider.m_Size, obj2colliderPos, obj2Collider.m_Size);
                    }
                        
                    // box and sphere
                    else if (obj2Collider.m_Radius.HasValue)
                    {
                        return SphereBoxCollision(obj1colliderPos, obj2Collider.m_Radius.Value, obj1colliderPos, obj2Collider.m_Size);
                    }                        
                }
                    
                else if (obj1Collider.m_Radius.HasValue)
                {
                    //sphere and box
                    if (obj2Collider.m_Size!=null)
                    {
                        return SphereBoxCollision(obj1colliderPos, obj1Collider.m_Radius.Value, obj2colliderPos, obj2Collider.m_Size);
                    }
                    //Two spheres
                    else if (obj2Collider.m_Radius.HasValue)
                    {
                        return SphereSphereCollision(obj1colliderPos, obj1Collider.m_Radius.Value, obj2colliderPos, obj2Collider.m_Radius.Value);
                    }
                }
                
            }
            return collision;
        }
        private bool ContinuousCD(ICollider[] obj1Colliders, Vector3 obj1Pos, ICollider obj2Collider, Vector3 obj2Pos)
        {
            bool collision = false;

            return collision;
        }
        private Boolean SphereBoxCollision(Vector3 spherePos, float radius, Vector3 boxPos, Vector3 size)
        {
            Vector3 BoxSize = size.ReturnMultipleByScalar(0.5f);
            Vector3 BoxMin = boxPos.ReturnSubtractVector(size);
            Vector3 BoxMax = boxPos.ReturnAddVector(size);

            if (spherePos.x < BoxMin.x)
            {
                radius -= absoluteValue(spherePos.x - BoxMin.x);
            }
            else if (spherePos.x > BoxMax.x)
            {
                radius -= absoluteValue(spherePos.x - BoxMax.x);
            }

            if (spherePos.y < BoxMin.y)
            {
                radius -= absoluteValue(spherePos.y - BoxMin.y);
            }
            else if (spherePos.y > BoxMax.y)
            {
                radius -= absoluteValue(spherePos.y - BoxMax.y);
            }

            if (spherePos.z < BoxMin.z)
            {
                radius -= absoluteValue(spherePos.x - BoxMin.z);
            }
            else if (spherePos.z > BoxMax.z)
            {
                radius -= absoluteValue(spherePos.z - BoxMax.z);
            }
            return radius > 0;
        }
        private Boolean SphereSphereCollision(Vector3 sphere1Pos, float sphere1radius, Vector3 sphere2Pos, float sphere2radius)
        {
            float totalRadius = sphere1radius + sphere2radius;
            Vector3 distance = sphere1Pos.ReturnSubtractVector(sphere2Pos).ReturnAbsoluteVector();
            
            return (distance.x<totalRadius&&distance.y<totalRadius&&distance.z<totalRadius);
        }
        private Boolean BoxBoxCollision(Vector3 box1Pos, Vector3 box1size, Vector3 box2Pos, Vector3 box2size)
        {

            Vector3 obj1Size = box1size.ReturnMultipleByScalar(0.5f);
            Vector3 obj1Min = box1Pos.ReturnSubtractVector(obj1Size);
            Vector3 obj1Max = box1Pos.ReturnAddVector(obj1Size);

            Vector3 obj2Size = box2size.ReturnMultipleByScalar(0.5f);
            Vector3 obj2Min = box2Pos.ReturnSubtractVector(obj2Size);
            Vector3 obj2Max = box2Pos.ReturnAddVector(obj2Size);

            //if (obj1Min.x < obj2Max.x && obj1Max.x > obj2Min.x)
            if (obj1Min.ReturnAllLessAndEqualThen(obj2Max) && obj1Max.ReturnAllGreaterAndEqualThen(obj2Min))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private float absoluteValue(float value)
        {
            if (value < 0)
                value *= -1;
            return value ;
        }

        public void PrintError(string errmssg)
        {
            throw new NotImplementedException();
        }

        private Vector3[] calculateMomemtumAfterCollision(float mass1, Vector3 velocity1, float mass2, Vector3 velocity2)
        {
            float difference = mass1-mass2;
            float sum = mass1 + mass2;
            //float finalVelocity1x = ((difference * velocity1.x) + (2 * mass2 * velocity2.x)) / sum;
            //float finalVelocity2x = ((2 * mass1 * velocity1.x) - (difference * velocity2.x)) / sum;
            Vector3 finalVelocity1 = (velocity1.ReturnMultipleByScalar(difference).ReturnAddVector(velocity2.ReturnMultipleByScalar(2 * mass2))).ReturnMultipleByScalar(1 / sum);
            Vector3 finalVelocity2 = (velocity1.ReturnMultipleByScalar(2 * mass1).ReturnSubtractVector(velocity2.ReturnMultipleByScalar(difference))).ReturnMultipleByScalar(1 / sum);

            return new Vector3[] { finalVelocity1, finalVelocity2 };
        }

        private Vector3[] timeStep(IRigidBody rigidBody, float factor)
        {
            Vector3 accel = rigidBody.m_PreviousFrameAcceleration;
            Vector3 velocity = rigidBody.m_PreviousFrameVelocity;
            Vector3 pos = rigidBody.m_PreviousFramePosition;
            if (rigidBody.m_Kinematic)
            {
                if (accel != new Vector3())
                {
                    accel = rigidBody.m_PreviousFrameAcceleration.ReturnMultipleByScalar((float)Constant.elapsedTime * factor);
                }
                if (rigidBody.m_Drag > 0.0f)
                {
                    Vector3 Temp = accel.ReturnMultipleByScalar(rigidBody.m_Drag);
                    accel.SubtractVector(Temp);
                }

                velocity.AddScalarVector(rigidBody.m_PreviousFrameAcceleration, ((float)Constant.elapsedTime*factor));
                pos.AddScalarVector(rigidBody.m_PreviousFrameVelocity, (float)Constant.elapsedTime * factor);

                return new Vector3[] { velocity, pos };

            }
            else
            {
                Vector3 vectorChange = rigidBody.m_Position.ReturnSubtractVector(pos);
                pos.AddVector(vectorChange.ReturnMultipleByScalar(factor));
                return new Vector3[] { velocity, pos };
            }
        }
    }
}

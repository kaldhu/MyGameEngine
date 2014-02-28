
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AWGP3Squared.Core;
//using AWGP3Squared.Maths;

//namespace AWGP3Squared.Components
//{
//    class SphereCollider
//    {
//        private Boolean m_Trigger;      // the collider is used for triggering events and is ignored by the physics engine
//        private IComponent m_Material;  // physics material
//        private Vector3 m_Centre;       // position of collider in objects local space
//        private float m_Radius;         // size of the collider

//         public SphereCollider(Boolean trigger = false, IComponent material = null, Vector3 centre = null, float radius = 0.0f)
//        {
//            m_Trigger = trigger;
//            m_Material = material;
//            m_Centre = centre != null ? centre : new Vector3();
//            m_Radius = radius;
//        }
//         /*
//          * returns m_Trigger 
//          * if an error occurs returns false
//          */
//         public Boolean isTrigger()
//        {
//            try
//            {
//                return m_Trigger;
//            }
//            catch(Exception e)
//            {
//                System.Console.WriteLine("SphereCollider addMaterial Error :" + e.ToString());
//                return false;
//            }

//        }

//        /*
//         * sets IComponent material to m_Material          
//         * returns boolean True if successful and returns false if method encounters error
//         */
//        public Boolean addMaterial(IComponent material)
//        {
//            try
//            {
//                //TODO: add something to material component to check its the right type of component 
//                this.m_Material = material;
//                return true;
//            }
//            catch(Exception e)
//            {
//                System.Console.WriteLine("SphereCollider addMaterial Error :" + e.ToString());
//                return false;
//            }
//        }

//        /*
//         * sets m_Material to null  
//         * returns boolean True if successful and returns false if method encounters error
//         */
//        public Boolean removeMaterial()
//        {
//            try
//            {
//                this.m_Material = null;
//                return true;
//            }
//            catch (Exception e)
//            {
//                System.Console.WriteLine("SphereCollider removeMaterial Error :" + e.ToString());
//                return false;
//            }
//        }

//        /*
//         * returns m_Material 
//         * if an error occurs returns null
//         */
//        public IComponent getMaterial()
//        {
//            try
//            {
//                return m_Material;
//            }
//            catch (Exception e)
//            {
//                System.Console.WriteLine("SphereCollider getMaterial Error :" + e.ToString());
//                return null;
//            }
//        }

//        /*
//         * returns m_Centre 
//         * if an error occurs returns null
//         */
//        public Vector3 getCentre()
//        {
//            try
//            {
//                return m_Centre;
//            }
//            catch (Exception e)
//            {
//                System.Console.WriteLine("SphereCollider getCentre Error :" + e.ToString());
//                return null;
//            }
//        }
//        /*
//         * set m_Centre to Vector3 Centre
//         * returns boolean True if successful and returns false if method encounters error
//         */
//        public Boolean setCentre(Vector3 centre)
//        {
//            try
//            {
//                m_Centre = centre;
//                return true;
//            }
//            catch (Exception e)
//            {
//                System.Console.WriteLine("SphereCollider setCentre Error :" + e.ToString());
//                return false;
//            }
//        }
//        /*
//         * returns m_Radius
//         * if an error occurs returns -1.0f
//         */
//        public float getRadius()
//        {
//            try
//            {
//                return m_Radius;
//            }
//            catch (Exception e)
//            {
//                System.Console.WriteLine("SphereCollider getRadius Error :" + e.ToString());
//                return -1.0f;
//            }
//        }
//        /*
//         * set m_Radius to Vector3 radius
//         * returns boolean True if successful and returns false if method encounters error
//         */
//        public Boolean setRadius(float radius)
//        {
//            try
//            {
//                m_Radius = radius;
//                return true ;
//            }
//            catch (Exception e)
//            {
//                System.Console.WriteLine("SphereCollider setRadius Error :" + e.ToString());
//                return false;
//            }
//        }
//    }
//}

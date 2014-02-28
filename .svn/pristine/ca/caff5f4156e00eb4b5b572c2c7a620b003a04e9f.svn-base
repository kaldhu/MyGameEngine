using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Components;
using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;
using AWGP3Squared.Maths;
using AWGP3Squared.Post_Office;

namespace AWGP3Squared.Core
{
    class GameObjectManager : IGameObjectManager
    {
        // Set up the list of game objects and the Post Office
        private static GameObjectManager m_Instance;
        private IPostOffice m_PostOffice { get; set; }
        private IScriptController m_ScriptController { get; set; }
        private Dictionary<String, IGameObject> m_GameObjectList { get; set; }
        public Constant.enumModuleID m_ModuleID { get; private set; }
        public Constant.enumMessage[] m_MsgTypeArray { get; private set; }

        protected GameObjectManager(IPostOffice postOffice, IScriptController scriptController)
        {
            m_ModuleID = Constant.enumModuleID.GAMEOBJECTMANAGER;
            m_PostOffice = postOffice;
            m_ScriptController = scriptController;
            m_GameObjectList = new Dictionary<String, IGameObject>();
            m_MsgTypeArray = new Constant.enumMessage[] 
            {
                Constant.enumMessage.INITIALISE,
                Constant.enumMessage.UPDATE,
                Constant.enumMessage.DRAW,
                Constant.enumMessage.SYNC_POSITIONDATA,
                Constant.enumMessage.CREATE_OBJECTS,
                Constant.enumMessage.ADD_COMPONENTS_TO_OBJECT,
                Constant.enumMessage.ADD_SCRIPT_TO_OBJECT,
                Constant.enumMessage.GET_COLLISIONDETECTIONDATA,
                Constant.enumMessage.GET_POSITION_COMPONENTS,
                Constant.enumMessage.GET_COMPONENTS,
                Constant.enumMessage.GET_GAMEOBJECT,
                Constant.enumMessage.GET_GAMEOBJECTCOMPONENTS,
                Constant.enumMessage.GET_GAMEOBJECTKEY_BY_ID,
                Constant.enumMessage.GET_GAMEOBJECTCOMPONENTS_BY_ID,
            };
            //m_Components = new List<IComponent>();
        }

        public static GameObjectManager Instance(IPostOffice postOffice,IScriptController scriptController)
        {
            if (m_Instance == null)
            {
                m_Instance = new GameObjectManager(postOffice, scriptController);
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
        private bool AddObjects()
        {
            Object[] objArray = m_PostOffice.GetMessageObjects(m_ModuleID, Constant.enumMessage.CREATE_OBJECTS);
            IGameObject[] gameObjects = Methods.GetObjectArrayOf<IGameObject>(objArray);
            try
            {
                foreach (IGameObject gameObj in gameObjects)
                {
                    m_GameObjectList.Add(gameObj.m_GameObjectKey, gameObj);
                }
              
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("GameObject AddObject Error :" + e.ToString());
                return false;
            }
        }

        private bool AddComponentsToObject()
        {
            List<IComponent> compList = new List<IComponent>();
            String key = null;
            Object[] objArray = m_PostOffice.GetMessageObjects(m_ModuleID, Constant.enumMessage.ADD_COMPONENTS_TO_OBJECT);

            foreach (Object[] obj in objArray)
            {
                compList.AddRange(Methods.GetObjectArrayOf<IComponent>(obj));
                if (key == null)
                {
                    key = Methods.GetObjectOf<string>(obj);
                }
            }
            //IComponent[] components = Methods.GetObjectArrayOf<IComponent>(objArray);
            try
            {
               m_GameObjectList[key].AddComponent(compList.ToArray());
               return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("GameObject AddComponentToObject Error :" + e.ToString());
                return false;
            }
        }
        public bool RemoveObject(String Key)
        {
            try
            {
                m_GameObjectList.Remove(Key);
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("GameObject RemoveObject Error :" + e.ToString());
                return false;
            }
        }

        private IGameObject[] getObjectWithComponent(Constant.enumComponent componentTypeID)
        {
            return m_GameObjectList.Where(obj => obj.Value.m_Components.Any(com => com.m_ComponentType.m_ComponentTypeID == componentTypeID)).Select(obj => obj.Value).ToArray();
        }

        /// <summary>
        /// Returns all gameObjects with a Component found in componentTypeIDArray
        /// </summary>
        /// <param name="componentTypeIDArray"> array of enumcomponents which will be used for search</param>
        /// <param name="complete">does gameobject need to match all ids in componentTypeIDArray to components (false = only one match to pass gameobject)</param>
        private IGameObject[] getObjectWithComponent(Constant.enumComponent[] componentTypeIDArray, bool complete = false)
        {
            // return gameobjects in m_ObjectList which have a component in m_components whose componentTypeId is found in componentTypeIDArray
            if (!complete)
            {
                return m_GameObjectList.Where(obj => obj.Value.m_Components.Any(com => componentTypeIDArray.Contains(com.m_ComponentType.m_ComponentTypeID))).Select(obj => obj.Value).ToArray();
            }
            else
            {
                return m_GameObjectList.Where(obj => componentTypeIDArray.All(id => obj.Value.m_Components.Any(com => com.m_ComponentType.m_ComponentTypeID == id))).Select(obj => obj.Value).ToArray();
            }
        }

        private IComponent[] getComponents(Constant.enumComponent componentTypeId)
        {
            List<IComponent> componentsList = new List<IComponent>();
            //foreach gameobject within m_ObjectList which has a component in m_components whose componentTypeId is found in componentTypeIdArray
            foreach (GameObject gameObj in m_GameObjectList.Where(obj => obj.Value.m_Components.Any(com => com.m_ComponentType.m_ComponentTypeID == componentTypeId)).Select(obj => obj.Value))
            {
                componentsList.Add(gameObj.getComponent(componentTypeId));
            }
            return componentsList.ToArray();
        }
        private T[] getComponents<T>(Constant.enumComponent componentTypeId)
        {
            List<T> componentsList = new List<T>();
            //foreach gameobject within m_ObjectList which has a component in m_components whose componentTypeId is found in componentTypeIdArray
            foreach (GameObject gameObj in m_GameObjectList.Where(obj => obj.Value.m_Components.Any(com => com.m_ComponentType.m_ComponentTypeID == componentTypeId)).Select(obj => obj.Value))
            {
                componentsList.Add(gameObj.getComponent<T>(componentTypeId));
            }
            return componentsList.ToArray();
        }


        private void ProcessCommands(Constant.enumMessage[] commands)
        {

            foreach (Constant.enumMessage c in commands)
            {
                m_PostOffice.LockMessageType(c);
                m_PostOffice.RemoveMessage(c, m_ModuleID);
                switch (c)
                {
                    //TODO: remove GET_COLLISIONDETECTIONDATA now done by GET_COMPONENTS(rigidbody, collider,positioncomp3d)
                    case (Constant.enumMessage.GET_COLLISIONDETECTIONDATA):
                        CollisionDetectionData();
                        break;
                    case (Constant.enumMessage.SYNC_POSITIONDATA):
                        SyncPosData();
                        break;
                    case (Constant.enumMessage.CREATE_OBJECTS):
                        AddObjects();
                        break;
                    case (Constant.enumMessage.ADD_COMPONENTS_TO_OBJECT):
                        AddComponentsToObject();
                        break;
                    case (Constant.enumMessage.ADD_SCRIPT_TO_OBJECT):
                        AddScriptToObject();
                        break;
                    case (Constant.enumMessage.GET_COMPONENTS):
                        ComponentRequest();
                        break;
                    case (Constant.enumMessage.GET_GAMEOBJECT):
                        GameObjectRequest();
                        break;
                    case (Constant.enumMessage.GET_GAMEOBJECT_BY_ID):
                        GameObjectRequest(true);
                        break;
                    case (Constant.enumMessage.GET_GAMEOBJECTCOMPONENTS):
                        GameObjectComponentRequest();
                        break;
                    case (Constant.enumMessage.GET_GAMEOBJECTCOMPONENTS_BY_ID):
                        GameObjectComponentRequest(true);
                        break;
                    case (Constant.enumMessage.GET_GAMEOBJECTKEY_BY_ID):
                        GameObjectKeyRequest();
                        break;
                    case (Constant.enumMessage.UPDATE):
                        //GameObjectUpdate();
                        break;
                    case (Constant.enumMessage.DRAW):
                        //GameObjectDraw();
                        break;
                    case (Constant.enumMessage.INITIALISE):
                        //GameObjectInit();
                        break;
                    default:
                        break;
                }
                m_PostOffice.UnLockMessageType(c);
            }
        }

        private void GameObjectKeyRequest()
        {
            List<String> gameObjIDList = new List<string>();

            Object[] objArray = m_PostOffice.GetMessageObjects(m_ModuleID, Constant.enumMessage.GET_GAMEOBJECTKEY_BY_ID);

            foreach (Object[] obj in objArray)
            {
                gameObjIDList.AddRange( Methods.GetObjectArrayOf<String>(obj));
            }

            String[] gameObjectKey = m_GameObjectList.Where(gObj => gameObjIDList.Contains(gObj.Value.m_GameObjectId.ToString())).Select(gObj => gObj.Key).ToArray();

            m_PostOffice.SendMessage(Constant.enumMessage.SEND_GAMEOBJECTKEY, gameObjectKey , false);
        }

        private void AddScriptToObject()
        {
            List<IScript> scriptList = new List<IScript>();
            String key = null;
            Object[] objArray = m_PostOffice.GetMessageObjects(m_ModuleID, Constant.enumMessage.ADD_SCRIPT_TO_OBJECT);

            foreach (Object[] obj in objArray)
            {
                scriptList.AddRange(Methods.GetObjectArrayOf<IScript>(obj));
                if (key == null)
                {
                    key = Methods.GetObjectOf<string>(obj);
                }
            }
            try
            {
                m_ScriptController.AddScript(m_GameObjectList[key].m_GameObjectId, scriptList.ToArray());
                foreach(IScript script in scriptList)
                {
                    IScriptComponent scriptComponent = new ScriptComponent(m_GameObjectList[key].m_GameObjectId,key, script);
                    script.m_ScriptController = m_ScriptController;
                    script.m_GameObject = m_GameObjectList[key];
                    m_GameObjectList[key].AddComponent(scriptComponent);
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("GameObject AddScriptToObject Error :" + e.ToString());
            }
        }

        private void GameObjectComponentRequest(Boolean byID = false)
        {
            List<Constant.enumComponent> componentIDList = new List<Constant.enumComponent>();
            String gameObjKey = null;

            Object[] objArray = m_PostOffice.GetMessageObjects(m_ModuleID,!byID? Constant.enumMessage.GET_GAMEOBJECTCOMPONENTS:Constant.enumMessage.GET_GAMEOBJECTCOMPONENTS_BY_ID);
            
            foreach (Object[] obj in objArray)
            {
                if(gameObjKey==null)
                    gameObjKey = Methods.GetObjectOf<String>(obj);
                componentIDList.Add(Methods.GetObjectOf<Constant.enumComponent>(obj));
                componentIDList.AddRange(Methods.GetObjectArrayOf<Constant.enumComponent>(obj));
            }

            if (gameObjKey != null && componentIDList.Count > 0)
            {
                IComponent[] componentArray = null;
                if (!byID)
                {

                    componentArray = m_GameObjectList[gameObjKey].m_Components.Where(com => componentIDList.Contains(com.m_ComponentType.m_ComponentTypeID)).ToArray();
                }
                else
                {
                    componentArray = m_GameObjectList.Values.Where(gObj => gObj.m_GameObjectId == int.Parse(gameObjKey)).FirstOrDefault().m_Components.Where(com => componentIDList.Contains(com.m_ComponentType.m_ComponentTypeID)).ToArray();

                }
                m_PostOffice.SendMessage(Constant.enumMessage.SEND_GAMEOBJECTCOMPONENTS, componentArray, false);
            }

        }

        private void GameObjectRequest(Boolean byID = false)
        {
            List<IGameObject> gameObjList = new List<IGameObject>();
            List<String> gameObjKeyList = new List<String>();
            Object[] objArray = m_PostOffice.GetMessageObjects(m_ModuleID, !byID? Constant.enumMessage.GET_GAMEOBJECT:Constant.enumMessage.GET_GAMEOBJECT_BY_ID);
            gameObjKeyList.AddRange(Methods.GetObjectArrayOf<String>(objArray));

            if (!byID)
            {
                foreach (String key in gameObjKeyList)
                {
                    gameObjList.Add(m_GameObjectList[key]);
                }
            }
            else
            {
                foreach (String ID in gameObjKeyList)
                {
                    gameObjList.AddRange(m_GameObjectList.Values.Where(gObj => gObj.m_GameObjectId == int.Parse(ID)));
                }
            }
            m_PostOffice.SendMessage(Constant.enumMessage.SEND_GAMEOBJECT, gameObjList.ToArray(), false);
        }


        private void ComponentRequest()
        {
            List<IComponent> componentsList = new List<IComponent>();
            List<String> componentsNameList = new List<String>();
            Object[] objArray = m_PostOffice.GetMessageObjects(m_ModuleID, Constant.enumMessage.GET_COMPONENTS);
            //foreach (Object[] obj in objArray)
            //{
                componentsNameList.AddRange(Methods.GetObjectArrayOf<String>(objArray));
           // }
            foreach(String componentsName in componentsNameList)
            {
                componentsList.AddRange(getComponents((Constant.enumComponent)Enum.Parse(typeof(Constant.enumComponent), componentsName)));
            }
                m_PostOffice.SendMessage(Constant.enumMessage.SEND_COMPONENTS, componentsList.ToArray(), false);
        }

        private void SyncPosData()
        {
            foreach (IGameObject obj in m_GameObjectList.Where(obj => obj.Value.m_Components
                                                        .Any(com => com.m_ComponentType.m_ComponentTypeID == Constant.enumComponent.RIGIDBODY))
                                                        .Select(obj=>obj.Value))
            {
                obj.getComponent<IRigidBody>(Constant.enumComponent.RIGIDBODY).m_Position = obj.getComponent<IPositionComponent3D>(Constant.enumComponent.POSITIONCOMPONENT3D).m_Position;
            }
        }
        private void CollisionDetectionData()
        {

            //Gets all gameobjects with a collider
            IGameObject[] GameObjArray = getObjectWithComponent(Constant.enumComponent.COLLIDER);

            // Stores the rigidbody component of each gameObj in GameObjArray which have a RigidBody it into an array

            IRigidBody[] rigidBodyArray = GameObjArray.Where(obj => obj.m_Components.Any(com => com.m_ComponentType.m_ComponentTypeID == Constant.enumComponent.RIGIDBODY))
                                            .Select(c => c.getComponent<IRigidBody>(Constant.enumComponent.RIGIDBODY)).ToArray();

            //stores the collider component of each gameObj in GameObjArray into array;
            ICollider[] ColliderArray = GameObjArray.Select(c => c.getComponent<ICollider>(Constant.enumComponent.COLLIDER)).ToArray();

            //stores the IPositionComponent3D component of each gameObj in GameObjArray that DOES NOT have a RigidBody into an array;
            IPositionComponent3D[] PositionComponent3DArray = GameObjArray.Select(c => c.getComponent<IPositionComponent3D>(Constant.enumComponent.POSITIONCOMPONENT3D)).ToArray();
            
            IMessage[] messages ={
                                     new Message(Constant.enumMessage.SEND_COLLISIONDETECTIONDATA, false, rigidBodyArray),
                                     new Message(Constant.enumMessage.SEND_COLLISIONDETECTIONDATA, false, ColliderArray),
                                     new Message(Constant.enumMessage.SEND_COLLISIONDETECTIONDATA, false, PositionComponent3DArray),
                                 };

            m_PostOffice.SendMessages(messages);

        }




        public void PrintError(string errmssg)
        {
            throw new NotImplementedException();
        }
    }
}
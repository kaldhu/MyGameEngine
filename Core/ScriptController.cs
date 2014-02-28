using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;

namespace AWGP3Squared.Core
{
    class ScriptController : IScriptController , IModule
    {
        private static ScriptController m_Instance { get; set; }
        public List<int>m_GameObjectIdList { get; set; }
        public IPostOffice m_PostOffice { get; private set; }
        public List<IScript> scriptList { get; private set; }
        public Constant.enumModuleID m_ModuleID { get; private set; }
        public Constant.enumMessage[] m_MsgTypeArray { get; private set; }
        public List<Constant.enumMessage> m_TempMsgTypeList { get; private set; }


        protected ScriptController(IPostOffice postOffice)
        {
            m_ModuleID = Constant.enumModuleID.SCRIPTCONTROLLER;
            m_PostOffice = postOffice;
            m_MsgTypeArray = new Constant.enumMessage[]
            {
                Constant.enumMessage.UPDATE,
                Constant.enumMessage.INITIALISE,
                Constant.enumMessage.DRAW,
                Constant.enumMessage.COLLISIONDETECTION,
                Constant.enumMessage.SEND_GAMEOBJECT,
            };
            m_TempMsgTypeList = new List<Constant.enumMessage>();
            m_GameObjectIdList = new List<int>();
            scriptList = new List<IScript>();
        }
        public static ScriptController Instance(IPostOffice postOffice)
        {
            if (m_Instance == null)
            {
                m_Instance = new ScriptController(postOffice);
            }
            else
            {
            }
            return m_Instance;
        } 

        public void AddScript(int gameObjectId,IScript[] scriptArray)
        {
            m_GameObjectIdList.Add(gameObjectId);
            scriptList.AddRange(scriptArray);
        }
        private void ProcessCommands(Constant.enumMessage[] commands)
        {
            foreach (IScript script in scriptList)
            {
                foreach (Constant.enumMessage c in commands)
                {
                    m_PostOffice.LockMessageType(c);
                    m_PostOffice.RemoveMessage(c, m_ModuleID, true);

                    switch (c)
                    {
                        case (Constant.enumMessage.INITIALISE):
                            script.Init();
                            break;
                        case (Constant.enumMessage.UPDATE):
                            script.Update();
                            break;
                        case (Constant.enumMessage.DRAW):
                            script.Draw();
                            break;
                        case (Constant.enumMessage.COLLISIONDETECTION):
                            ICollisionDetection collisionDection = script as ICollisionDetection;
                            if (collisionDection != null)
                            {
                               int[] gameObjIDArray = Array.ConvertAll((Methods.GetObjectArrayOf<String>(m_PostOffice.GetMessageObjects(m_ModuleID, Constant.enumMessage.COLLISIONDETECTIONDATA))), s => int.Parse(s));

                               if (gameObjIDArray.Count() >0 && script.m_GameObject.m_GameObjectId != gameObjIDArray[0])
                               {
                                   String[] gameObjeKeyArray = RequestGameObjectKeyByID(gameObjIDArray);
                                   collisionDection.CollisionDetection(gameObjeKeyArray[1]);
                               }
                            }
                            break;
                        default:
                            break;
                    }
                    m_PostOffice.UnLockMessageType(c);
                }
            }
        }
        
        public void Init()
        {
            throw new NotImplementedException();
        }

        public void Notify(bool firstPass)
        {
            if (firstPass)
            {
                if (m_TempMsgTypeList.Count == 0)
                {
                    m_PostOffice.CheckMessages(m_ModuleID, m_MsgTypeArray);
                }
                else
                {
                    m_TempMsgTypeList.AddRange(m_MsgTypeArray);
                    m_PostOffice.CheckMessages(m_ModuleID, m_TempMsgTypeList.ToArray());
                }
            }
            else
            {
                ProcessCommands(m_PostOffice.getMessageTypeIDs(m_ModuleID));
            }   
        }

        public void PrintError(string errmssg)
        {
            throw new NotImplementedException();
        }

        public IGameObject[] RequestGameObject(String[] data)
        {
            m_TempMsgTypeList.Add(Constant.enumMessage.SEND_GAMEOBJECT);
                
                m_PostOffice.SendMessage(Constant.enumMessage.GET_GAMEOBJECT, data);

                Object[] objArray = m_PostOffice.GetMessageObjects(m_ModuleID, Constant.enumMessage.SEND_GAMEOBJECT);

                IGameObject[] gameObjectArray = Methods.GetObjectArrayOf<IGameObject>(objArray);

                m_TempMsgTypeList.Clear();
                return gameObjectArray;
        }

        public IGameObject RequestGameObject(String data)
        {
            m_TempMsgTypeList.Add(Constant.enumMessage.SEND_GAMEOBJECT);

            m_PostOffice.SendMessage(Constant.enumMessage.GET_GAMEOBJECT, new String[] { data });

            Object[] objArray = m_PostOffice.GetMessageObjects(m_ModuleID, Constant.enumMessage.SEND_GAMEOBJECT);

            IGameObject gameObject = Methods.GetObjectOf<IGameObject>(objArray);

            m_TempMsgTypeList.Clear();
            return gameObject;
        }

        public IGameObject RequestGameObjectByID(int ID)
        {
            m_TempMsgTypeList.Add(Constant.enumMessage.SEND_GAMEOBJECT);

            m_PostOffice.SendMessage(Constant.enumMessage.GET_GAMEOBJECT_BY_ID, new String[] { ID.ToString() });

            Object[] objArray = m_PostOffice.GetMessageObjects(m_ModuleID, Constant.enumMessage.SEND_GAMEOBJECT);

            IGameObject gameObject = Methods.GetObjectOf<IGameObject>(objArray);

            m_TempMsgTypeList.Clear();
            return gameObject;
        }

        private String[] RequestGameObjectKeyByID(int[] ID)
        {
            m_TempMsgTypeList.Add(Constant.enumMessage.SEND_GAMEOBJECTKEY);

            m_PostOffice.SendMessage(Constant.enumMessage.GET_GAMEOBJECTKEY_BY_ID, Array.ConvertAll(ID, i=>i.ToString()));

            Object[] objArray = m_PostOffice.GetMessageObjects(m_ModuleID, Constant.enumMessage.SEND_GAMEOBJECTKEY);

            String[] gameObjectkey = Methods.GetObjectArrayOf<String>(objArray);

            m_TempMsgTypeList.Clear();
            return gameObjectkey;
        }

        public T[] RequestData<T>(Constant.enumMessage msgTypeID, Object[] data = null)
        {
            Constant.enumMessage recieveMsgTypeID = Constant.enumMessage.NULL;
            switch (msgTypeID)
            {
                case Constant.enumMessage.GET_GAMEOBJECT:
                    recieveMsgTypeID = Constant.enumMessage.SEND_GAMEOBJECT;
                    break;
                case Constant.enumMessage.GET_COMPONENTS:
                    recieveMsgTypeID = Constant.enumMessage.SEND_COMPONENTS;
                    break;
                case Constant.enumMessage.GET_GAMEOBJECTCOMPONENTS:
                    recieveMsgTypeID = Constant.enumMessage.SEND_GAMEOBJECTCOMPONENTS;
                    break;
                case Constant.enumMessage.GET_GAMEOBJECTCOMPONENTS_BY_ID:
                    recieveMsgTypeID = Constant.enumMessage.SEND_GAMEOBJECTCOMPONENTS;
                    break;
                case Constant.enumMessage.LOAD_TEXTURES:
                    recieveMsgTypeID = Constant.enumMessage.SEND_TEXTURE_IDS;
                    break;
                case Constant.enumMessage.GET_PRESSED_MOUSEBUTTONS:
                    recieveMsgTypeID = Constant.enumMessage.SEND_PRESSED_MOUSEBUTTONS;
                    break;
                case Constant.enumMessage.GET_PRESSED_KEYS:
                    recieveMsgTypeID = Constant.enumMessage.SEND_PRESSED_KEYS;
                    break;
                case Constant.enumMessage.GET_MOUSE_POSITION:
                    recieveMsgTypeID = Constant.enumMessage.SEND_MOUSE_POSITION;
                    break;
                case Constant.enumMessage.GET_MOUSE_WHEEL_DELTA:
                    recieveMsgTypeID = Constant.enumMessage.SEND_MOUSE_WHEEL_DELTA;
                    break;
                case Constant.enumMessage.GET_GRAPHICS_SETTINGS:
                    recieveMsgTypeID = Constant.enumMessage.SEND_GRAPHICS_SETTINGS;
                    break;
            }

            if (recieveMsgTypeID != Constant.enumMessage.NULL)
            {
                m_TempMsgTypeList.Add(recieveMsgTypeID);

                m_PostOffice.SendMessage(msgTypeID, data);

                Object[] objArray = m_PostOffice.GetMessageObjects(m_ModuleID, recieveMsgTypeID);

                T[] TArray = Methods.GetObjectArrayOf<T>(objArray);

                m_TempMsgTypeList.Clear();
                return TArray;
            }
            else
            {
                throw new ApplicationException();
            }
        }
        public void SendData(Constant.enumMessage msgTypeID, Object[] data = null)
        {
            m_PostOffice.SendMessage(msgTypeID, data);
        }

        public void SendDataForObject<T>(Constant.enumMessage msgTypeID, String objectKey, T[] data)
        {
            SendData(msgTypeID, new Object[] { objectKey, data });
        }
        public void SendDataForObject<T>(Constant.enumMessage msgTypeID, String objectKey, T data)
        {
            T[] TArray = { data };
            SendData(msgTypeID, new Object[] { objectKey, TArray });
        }


    }
}

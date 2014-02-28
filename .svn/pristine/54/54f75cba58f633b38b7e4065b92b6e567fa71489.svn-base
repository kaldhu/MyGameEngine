using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;
using AWGP3Squared.Maths;
using AWGP3Squared.PhysicsModule;
using AWGP3Squared.Graphics;
using AWGP3Squared.AI;
using AWGP3Squared.Input;
using AWGP3Squared.Post_Office;

namespace AWGP3Squared.Core
{
    class GameCommand : IGameCommand
    {
        private Boolean m_Continue = true;
        private static GameCommand m_Instance;
        private IPostOffice m_PostOffice { get; set; }
        private IGame m_Game { get; set; }
        private IObserver m_Observer { get; set; }
        private IGameObjectManager m_GameObjectManager { get; set; }
        private IPhysics m_Phyics { get; set; }
        private IGraphics m_Graphics { get; set; }
        private IArtfc m_AI { get; set; } 
        private IInput m_Input { get; set; }
        private IModule m_ScriptController { get; set; }
        public Constant.enumModuleID m_ModuleID { get; private set; }
        public Constant.enumMessage[] m_MsgTypeArray { get; private set; }
        public List<Constant.enumMessage> m_TempMsgTypeList { get; private set; }


        protected GameCommand(IGame game)
        {
            m_Game = game;
            m_ModuleID = Constant.enumModuleID.GAMECOMMAND;
            m_TempMsgTypeList = new List<Constant.enumMessage>();
            m_MsgTypeArray = new Constant.enumMessage[] 
            {
                Constant.enumMessage.INITIALISE,
                Constant.enumMessage.DRAW,
                Constant.enumMessage.UPDATE,
                Constant.enumMessage.CLOSING,
            };
        }

        public static GameCommand Instance(IGame game)
        {
            if (m_Instance == null)
            {
                m_Instance = new GameCommand(game);
            }

            return m_Instance;
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

        private void ProcessCommands(Constant.enumMessage[] commands)
        {
            foreach (Constant.enumMessage c in commands)
            {
                m_PostOffice.LockMessageType(c);
                m_PostOffice.RemoveMessage(c, m_ModuleID, true);
                switch (c)
                {
                    case (Constant.enumMessage.INITIALISE):
                        m_Game.Init();
                        break;
                    case (Constant.enumMessage.UPDATE):
                        m_Game.Update();
                        break;
                    case (Constant.enumMessage.DRAW):
                        m_Game.Draw();
                        break;
                    case (Constant.enumMessage.CLOSING):
                        Close();
                        break;
                    default:
                        break;
                }
                m_PostOffice.UnLockMessageType(c);
            }
        }

        public void LoadGame()
        {

            m_Observer = Observer.Instance();
            m_PostOffice = PostOffice.Instance(m_Observer);

            //m_Game = GameOne.Instance(m_PostOffice);
            m_Game.LoadGameCommand(m_Instance);
            m_ScriptController = ScriptController.Instance(m_PostOffice);
            m_GameObjectManager = GameObjectManager.Instance(m_PostOffice, m_ScriptController as IScriptController);
            m_Input = SFMLInput.Instance(m_PostOffice);
            m_Phyics = Physics.Instance(m_PostOffice);
            m_AI = AIAgent.Instance(m_PostOffice);
            m_Graphics = new SFMLGraphics2D(m_PostOffice);
            m_PostOffice.AddModule(m_Instance);
            m_PostOffice.AddModule(m_GameObjectManager);
            m_PostOffice.AddModule(m_ScriptController);
            m_PostOffice.AddModule(m_Input);
            m_PostOffice.AddModule(m_Phyics);
            m_PostOffice.AddModule(m_AI);
            m_PostOffice.AddModule(m_Graphics);

            Constant.previousTime = DateTime.Now;
        }

        private void TimeElapsed()
        {
            //DateTime currentTime = DateTime.Now;
            //Constant.elapsedTime = (currentTime - Constant.previousTime).TotalSeconds;
            //Constant.previousTime = currentTime;
            Constant.elapsedTime = 0.05;
        }

        public void GameLoop()
        {

            m_PostOffice.SendMessage(Constant.enumMessage.INITIALISE);
            while (m_Continue)
            {
                TimeElapsed();
                m_PostOffice.SendMessage(Constant.enumMessage.SYNC_POSITIONDATA);
                m_PostOffice.SendMessage(Constant.enumMessage.COLLISIONDETECTION);
                m_PostOffice.SendMessage(Constant.enumMessage.UPDATE);
                m_PostOffice.SendMessage(Constant.enumMessage.DRAW);
                foreach (List<IMessage> letterbox in m_PostOffice.m_LetterBoxes.Values)
                {
                    letterbox.Clear();
                }
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

        public void RequestData<T>(Constant.enumMessage msgTypeID, Constant.enumMessage recieveMsgTypeID, Object[] data = null)
        {
            m_TempMsgTypeList.Add(recieveMsgTypeID);

            SendData(msgTypeID, data);

            Object[] objArray = m_PostOffice.GetMessageObjects(m_ModuleID, recieveMsgTypeID);

            T[] TArray = Methods.GetObjectArrayOf<T>(objArray);

            m_TempMsgTypeList.Clear();
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

                SendData(msgTypeID, data);

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


        public void Close()
        {
            m_Continue = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;
using AWGP3Squared.Maths;

namespace AWGP3Squared.Post_Office
{
    class PostOffice:IPostOffice
    {
        private List<IMessage> m_POBox { get; set; }

        public Dictionary<Constant.enumModuleID, List<IMessage>> m_LetterBoxes { get; private set; }
        public Constant.enumMessage[] m_MsgLockables { get; private set; }
        public List<Constant.enumMessage> m_LockList { get; private set; }
        //private Dictionary<Constant.enumModuleID, List<Constant.enumMessage>> m_ModuleMsgTypeList { get; set; }

        private static PostOffice m_Instance { get; set; }
        public IObserver m_Observer { get; private set; }

        protected PostOffice(IObserver observer)
        {
            m_Observer = observer;
            m_POBox = new List<IMessage>();
            m_LetterBoxes = new Dictionary<Constant.enumModuleID, List<IMessage>>();
            m_LockList = new List<Constant.enumMessage>();
            m_MsgLockables = new Constant.enumMessage[]
            {
                Constant.enumMessage.INITIALISE,
                Constant.enumMessage.DRAW,
                Constant.enumMessage.UPDATE,
            };
            //m_ModuleMsgTypeList = new Dictionary<Constant.enumModuleID, List<Constant.enumMessage>>();
        }
        public void LockMessageType(Constant.enumMessage msgTypeID)
        {
            if (m_MsgLockables.Contains(msgTypeID) && !m_LockList.Contains(msgTypeID))
            {
                m_LockList.Add(msgTypeID);
            }
        }
        public void UnLockMessageType(Constant.enumMessage msgTypeID)
        {
            if (m_MsgLockables.Contains(msgTypeID))
                m_LockList.Remove(msgTypeID);
        }
        public void AddModule(IModule module)
        {
            m_LetterBoxes.Add(module.m_ModuleID, new List<IMessage>());
            m_Observer.Attach(module);
        }
        
        public static PostOffice Instance(IObserver observer)
        {
            if (m_Instance == null)
            {
                m_Instance = new PostOffice(observer);
            }

            return m_Instance;
        }

        public void init()
        {
            throw new NotImplementedException();
        }
        
        public void SendMessages(IMessage[] msgArray)
        {
            foreach (IMessage msg in msgArray)
            {
                if (msg.m_MsgType.m_Command)
                {
                    IMessage msg2 = new Message(msg.m_MsgType, null); 
                    m_POBox.Add(msg2);
                }
                if (msg.m_Data != null)
                {
                    IMessage msg3 = new Message(msg.m_MsgType.m_MsgTypeID, false, msg.m_Data);
                    m_POBox.Add(msg3);
                }
            }
            //m_POBox.AddRange(msg);
            m_Observer.Notify(true);
            m_POBox.Clear();
            m_Observer.Notify(false);
        }
        
        public void SendMessage(Constant.enumMessage msgTypeID, Object[] data = null, Boolean command = true)
        {
            if (command)
            {
                IMessage msg2 = new Message(msgTypeID, true, null);
                m_POBox.Add(msg2);
            }
            if (data != null)
            {
                IMessage msg = new Message(msgTypeID, false, data);
                m_POBox.Add(msg);
            }
            if (data == null && !command)
            {
                throw new ApplicationException();
            }
            m_Observer.Notify(true);
            m_POBox.Clear();
            if(command)
            m_Observer.Notify(false);
        }
        //TODO find a way to pass a data object containing different data types in one object[] so they can be picked up by helper method currently have to create multiple msgs and send them using
        //SendMessages(IMessage[] msgArray)
        /*
        public void SendMessage(Constant.enumMessage msgTypeID, Boolean command = true, Object[] data =null, Boolean split = false)
        {
            List<Object> typeList = new List<Object>();
            if (data != null)
            {
                
                if (split)
                {
                    foreach (Object obj in data)
                    {
                        if (!typeList.Any(o => o.GetType() == obj.GetType()))
                        {
                            Object[] objArray = data.Where(o => o.GetType() == obj.GetType()).ToArray();
                            foreach (Object o in objArray)
                            {
                                IMessage msgSplit = new Message(msgTypeID, false,o);
                                m_POBox.Add(msgSplit);
                            }
                            typeList.Add(obj);
                        }
                    }
                }
                else
                {
                    IMessage msg = new Message(msgTypeID, false, data);
                    m_POBox.Add(msg);
                }
            }
            if (command)
            {
                IMessage msg2 = new Message(msgTypeID, true, null);
                m_POBox.Add(msg2);
            }
            if (data == null && !command)
            {
                throw new ApplicationException();
            }
            m_Observer.Notify(true);
            m_POBox.Clear();
            if (command)
                m_Observer.Notify(false);
        }
        */
        //TODO make this generic so data is stored as an array of T and is passed, it is possible to force the game developer to define all data as an array before being passed so uses the 
        //sendmessage(Constant.enumMessage msgTypeID, Boolean command = true, Object[] data = null) which bypasses all issues but i believe to be unclean
        /// <summary>
        /// NOTICE: this doesnt work correctly needs Type of data to be passed or will be converted to object[] meaning GetObjectArrayOf can not find by datas actual type
        /// </summary>
        /*
        public void SendMessage(Constant.enumMessage msgTypeID, Boolean command, Object data)
        {
            if (command)
            {
                IMessage msg2 = new Message(msgTypeID, true, null);
                m_POBox.Add(msg2);
            }
            if (data != null)
            {
                IMessage msg = new Message(msgTypeID, false, data);
                m_POBox.Add(msg);
            }
            if (data == null && !command)
            {
                throw new ApplicationException();
            }
            m_Observer.Notify(true);
            m_POBox.Clear();
            if (command)
            m_Observer.Notify(false);
        }
        */
        public void CheckMessages(Constant.enumModuleID moduleID, Constant.enumMessage[] msgIDList)
        {
            m_LetterBoxes[moduleID].AddRange(m_POBox.Where(x => msgIDList.Contains(x.m_MsgType.m_MsgTypeID)).ToArray());
        }

        public IMessage[] GetMessages(Constant.enumModuleID moduleID, Constant.enumMessage[] msgIDList = null, Boolean? command = null)
        {
            if (!command.HasValue)
            {
                return m_LetterBoxes[moduleID].Where(msg => msgIDList.Contains(msg.m_MsgType.m_MsgTypeID)).ToArray();
            }
            else
            {
                return m_LetterBoxes[moduleID].Where(msg => msgIDList.Contains(msg.m_MsgType.m_MsgTypeID) &&
                                                                                            (command.HasValue ? msg.m_MsgType.m_Command
                                                                                                             : !msg.m_MsgType.m_Command)).ToArray();
            }
        }

        public Object[] GetMessageObjects(Constant.enumModuleID moduleID, Constant.enumMessage msgID)
        {
            IMessage[] result = m_LetterBoxes[moduleID].Where(msg => msgID == msg.m_MsgType.m_MsgTypeID).ToArray();
            m_LetterBoxes[moduleID] = m_LetterBoxes[moduleID].Except(result).ToList();
            return result.Select(msg=>msg.m_Data).ToArray();
        }
        public Object[] GetMessageObjects(Constant.enumModuleID moduleID, Constant.enumMessage[] msgIDArray)
        {
            IMessage[] result = m_LetterBoxes[moduleID].Where(msg => msgIDArray.Contains(msg.m_MsgType.m_MsgTypeID)).ToArray();
            m_LetterBoxes[moduleID] = m_LetterBoxes[moduleID].Except(result).ToList();
            return result.Select(msg => msg.m_Data).ToArray();
        }
        //TODO: Remove if GetMessageObjects(Constant.enumModuleID moduleID, Constant.enumMessage msgID) works fine
        /*
        public Object[] GetMessageObjects(Constant.enumModuleID moduleID, Constant.enumMessage msgID, Boolean command = false)
        {
            return m_LetterBoxes[moduleID].Where(msg => msgID == msg.m_MsgType.m_MsgTypeID
                                                                && (command ? msg.m_MsgType.m_Command
                                                                           : !msg.m_MsgType.m_Command)).Select(msg => msg.m_Data).ToArray();
        }
        public Object[] GetMessageObjects(Constant.enumModuleID moduleID, Constant.enumMessage[] msgIDArray, Boolean command = false)
        {
            return m_LetterBoxes[moduleID].Where(msg => msgIDArray.Contains(msg.m_MsgType.m_MsgTypeID)
                                                                && (command ? msg.m_MsgType.m_Command
                                                                           : !msg.m_MsgType.m_Command)).Select(msg => msg.m_Data).ToArray();
        }*/
        /*
        public T[] GetMessageObjects<T>(Constant.enumModuleID moduleID, Constant.enumMessage msgID, Boolean command = false)
        {

            IMessage[] msgArray = m_LetterBoxes[moduleID].Where(x => msgID == x.m_MsgType.m_MsgTypeID
                                                                                && command ? x.m_MsgType.m_Command
                                                                                           : !x.m_MsgType.m_Command).ToArray();
            List<T> tList = new List<T>();
            foreach (IMessage msg in msgArray)
            {
               tList.Add(msg.m_Data(typeof(T);
            }

            throw new NotImplementedException();
        }
         */
        /*
        public Constant.enumMessage[] getMessageTypeIDs(Constant.enumModuleID moduleID, Boolean command = true)
        {
            if (!command)
            {
                return m_LetterBoxes[moduleID].Select(msg => msg.m_MsgType.m_MsgTypeID).ToArray();
            }
            else
            {
                return m_LetterBoxes[moduleID].Where(msg => (command ? msg.m_MsgType.m_Command
                                                                    : !msg.m_MsgType.m_Command)).Select(msg => msg.m_MsgType.m_MsgTypeID).ToArray();
            }
            //throw new NotImplementedException();
        }
        */
        public Constant.enumMessage[] getMessageTypeIDs(Constant.enumModuleID moduleID)
        {
            return m_LetterBoxes[moduleID].Where(msg => msg.m_MsgType.m_Command).Select(msg => msg.m_MsgType.m_MsgTypeID).Where(id=>!m_LockList.Contains(id)).ToArray();
            
            //throw new NotImplementedException();
        }

        public void RemoveMessage(Constant.enumMessage msgTypeID, Constant.enumModuleID moduleID, Boolean command = true)
        {
            //m_LetterBoxes[moduleID].Remove(m_LetterBoxes[moduleID].Where(msg => msg.m_MsgType.m_MsgTypeID == msgTypeID).FirstOrDefault());
            IMessage[] msgArray = m_LetterBoxes[moduleID].Where(msg => msg.m_MsgType.m_MsgTypeID == msgTypeID && (command ? msg.m_MsgType.m_Command
                                                                                                                           : !msg.m_MsgType.m_Command)).ToArray();
            foreach(IMessage msg in msgArray)
            {
                m_LetterBoxes[moduleID].Remove(msg);
             }
            //throw new NotImplementedException();
        }
    }
}

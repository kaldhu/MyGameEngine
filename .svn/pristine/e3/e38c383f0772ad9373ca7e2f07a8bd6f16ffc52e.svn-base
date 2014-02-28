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
    class MessageType : IMessageType
    {

        private static List<MessageType> m_InstanceList { get; set; }
        public Constant.enumMessage m_MsgTypeID { get; private set; }
        public bool m_Command { get; private set; }

        protected MessageType(Constant.enumMessage msgTypeID, Boolean command)
        {
            
            m_MsgTypeID = msgTypeID;
            m_Command = command;
        }

        public static MessageType Instance(Constant.enumMessage iD, Boolean command)
        {
            if (m_InstanceList == null)
            {
                m_InstanceList = new List<MessageType>();
            }
            if (!m_InstanceList.Any(x => x.m_MsgTypeID == iD && x.m_Command == command))
            {
                m_InstanceList.Add(new MessageType(iD, command));
            }

            return m_InstanceList.FirstOrDefault(x => x.m_MsgTypeID == iD && x.m_Command == command);
        }
        
    }
}

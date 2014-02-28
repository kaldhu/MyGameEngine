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
    class Message:IMessage
    {
        private IMessageType messageType;

        public IMessageType m_MsgType { get; private set; }
        public Object[] m_Data { get; private set; }

        public Message(IMessageType msgType, Object[] data = null)
        {
            m_MsgType = msgType;
            m_Data = data;
        }
        public Message(Constant.enumMessage msgTypeID, Boolean command = false, Object[] data = null)
        {
            m_MsgType = MessageType.Instance(msgTypeID, command);
            m_Data = data;
        }

        //NOTICE: This doesnt work Needs to be generic see genericMessage class it might work
        /*
        public Message(Constant.enumMessage msgTypeID, Boolean command, Object data)
        {
            m_MsgType = MessageType.Instance(msgTypeID, command);
            Object[] dataArray = { data };
            m_Data = dataArray;
        }
         */
    }
    //Generic Typing the message seems great idea as no longer have to type cast msg but cant ge
    /* class Message<T>:IMessage
    {
        public IMessageType m_MsgType { get; private set; }
        private T m_Data { get;  set; }
        
        public Message(MessageType msgType, T data)
        {
            m_MsgType = msgType;
            m_Data = data;
        }

        public Message(Constant.enumMessage msgTypeID,T data, Boolean command = false )
        {
            m_MsgType = MessageType.Instance(msgTypeID, command);
            m_Data = data;
        }
        
        public T GetData()
        {
            return m_Data;
        }
     }*/
}
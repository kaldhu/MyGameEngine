using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;
using AWGP3Squared.Maths;
using AWGP3Squared.Components;
using AWGP3Squared.Core;

namespace AWGP3Squared.AI
{
    class AIAgent : IArtfc
    {
        private IPostOffice m_PostOffice { get; set; }
        public Constant.enumMessage[] m_MsgTypeArray { get; private set; }
        public Constant.enumModuleID m_ModuleID { get; private set; }
        protected static AIAgent m_Instance { get; set; }

        protected AIAgent(IPostOffice postOff)
        {
            m_PostOffice = postOff;
            m_ModuleID = Constant.enumModuleID.AI;
            m_MsgTypeArray = new Constant.enumMessage[]
            {
                Constant.enumMessage.INITIALISE,
                Constant.enumMessage.UPDATE,
            };
        }

        public static AIAgent Instance(IPostOffice postOff)
        {
            if (m_Instance == null)
            {
                m_Instance = new AIAgent(postOff);
            }
            return m_Instance;
        }

        public void Update()
        {
            FSMComponent[] FSMs = new FSMComponent[]{
                new FSMComponent(new FSMachine(), true), 
                new FSMComponent(new FSMachine(), false)};

            PositionComponent3D[] positions = new PositionComponent3D[]{
                new PositionComponent3D(new Vector3()), 
                new PositionComponent3D(new Vector3(0.0f,1.0f,0.0f))
            };

            Vector3 targetPos = null;
            int targetID = -1;

            for( int i = 0; i< FSMs.Length; i++)
            {
                if (FSMs[i].m_IsTarget)
                {
                    targetPos = positions[i].m_Position;
                    targetID = i;
                }
            }

            if(targetPos != null && targetID != -1){
                for (int i = 0; i < FSMs.Length; i++)
                {
                    if(i != targetID){
                        FSMs[i].UpdateMachine(positions[i].m_Position, targetPos);
                    }
                }
            }
            else
            {
                //WHY IS IT GOING TO GO HERE?
                //Because there is nothing going in the array of FSMs 
            }
        }



        public void Init()
        {
           //throw new NotImplementedException();
        }

        public void Notify(bool firstPass)
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
                m_PostOffice.RemoveMessage(c, m_ModuleID);
                switch (c)
                {
                    case Constant.enumMessage.INITIALISE:
                        Init();
                        break;
                    case Constant.enumMessage.UPDATE:
                        Update();
                        break;
                    default:
                        Console.WriteLine("AI Module: Unhandled Post Office Message: " + c.ToString());
                        break;
                }
                m_PostOffice.UnLockMessageType(c);
            }
        }

        public void PrintError(string errmssg)
        {
            //throw new NotImplementedException();
        }
    }
}

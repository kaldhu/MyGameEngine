using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;
using AWGP3Squared.Maths;
using AWGP3Squared.AI;

namespace AWGP3Squared.Components
{
    class FSMComponent : IComponent
    {

        public IComponentType m_ComponentType{ get; private set; }
        public int m_GameObjectId{ get; set; }
        public FSMachine m_FSM { get; private set; }
        public bool m_IsTarget { get; private set; }

        public FSMComponent(FSMachine fsm, bool isTarget)
        {
            m_IsTarget = isTarget;
            m_FSM = fsm;
        }

        public void UpdateMachine(Vector3 currentPos, Vector3 targetPos)
        {
            m_FSM.UpdateMachine(currentPos,targetPos);
        }
    }
}
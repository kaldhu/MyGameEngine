using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;
using AWGP3Squared.Maths;

namespace AWGP3Squared.AI
{
    class FSMachine
    {
        List<Constant.enumAIState> m_States = new List<Constant.enumAIState>(); // list of states an object can be
        Constant.enumAIState currentstate { get; set; }
        
        public void UpdateMachine(Vector3 currentPos,Vector3 targetPos)
        {
            if (currentstate == Constant.enumAIState.WALK)
            {
                MoveRandomly(currentPos,targetPos);
            }
            if (currentstate == Constant.enumAIState.ATTACK)
            {
                TrackPlayer(currentPos,targetPos);
            }
            if (currentstate == Constant.enumAIState.RETREAT)
            {
                EvadePlayer(currentPos,targetPos);
            }
        }

        public void TrackPlayer(Vector3 currentPos, Vector3 targetPos)
            {
                float moveUnit = 1.0f;
                //float EvadeX = 10.0f;
                //float EvadeY = 10.0f;

                if (currentPos.x > targetPos.x)
                    currentPos.x = targetPos.x;
                else if (currentPos.x < targetPos.x)
                    currentPos.x += moveUnit;

                if (currentPos.y > targetPos.y)
                    currentPos.y = targetPos.y;
                else if (currentPos.y < targetPos.y)
                    currentPos.y += moveUnit;

                float distanceX = currentPos.x - targetPos.x;
                float distanceY = currentPos.y - targetPos.y;

                //If the enemy health is less than a certain amount
                //then retreat (run away from the player)
                //if (distanceX < EvadeX && distanceY < EvadeY)
                //{
                //    currentstate = Constant.enumAIState.RETREAT;
                //}
            }

        public void MoveRandomly(Vector3 currentPos, Vector3 targetPos)
        {
            Random _r = new Random();
            Vector3 RandomVelocity;
            RandomVelocity = new Vector3(0, 0, 0);
            
            float SeekX = 5.0f;
            float SeekY = 5.0f;

            //has the appropriate amount of time passed
                RandomVelocity.x = _r.Next(-1, 2);
                RandomVelocity.y = _r.Next(-1, 2);

                currentPos.x = currentPos.x + RandomVelocity.x;
                currentPos.y = currentPos.y + RandomVelocity.y;

                float distanceX = currentPos.x - targetPos.x;
                float distanceY = currentPos.y - targetPos.y;

                if (distanceX < SeekX && distanceY < SeekY)
                {
                        currentstate = Constant.enumAIState.ATTACK;
                }
        }

        public void EvadePlayer(Vector3 currentPos, Vector3 targetPos)
        {
            float EvadeX = 10.0f;
            float EvadeY = 10.0f;

            float distanceX = currentPos.x - targetPos.x;
            float distanceY = currentPos.y - targetPos.y;

            float moveUnit = 1.0f;

            if (currentPos.x > targetPos.x)
                currentPos.x = -moveUnit;
            else if (currentPos.x < targetPos.x)
                currentPos.x = moveUnit;

            if (currentPos.y > targetPos.y)
                currentPos.y = -moveUnit;
            else if (currentPos.y < targetPos.y)
                currentPos.y = moveUnit;

            if (distanceX > EvadeX && distanceY > EvadeY)
                currentstate = Constant.enumAIState.WALK;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Games;
using AWGP3Squared.Interfaces;
using AWGP3Squared.Maths;
using AWGP3Squared.PhysicsModule;
using AWGP3Squared.Post_Office;

namespace AWGP3Squared.Core
{
    class GameSystem
    {
        static void Main()
        {
            //IGame m_Game = GameOne.Instance();
           // IGame m_Game = new GraphicsExampleGame();
            IGame m_Game = new GameOne();
            IGameCommand m_GameCommand = GameCommand.Instance(m_Game); ;
            m_GameCommand.LoadGame();
            m_GameCommand.GameLoop();
#if DEBUG
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
#endif
        }

    }
}

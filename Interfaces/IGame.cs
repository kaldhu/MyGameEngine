using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGP3Squared.Interfaces
{
    interface IGame
    {
        void LoadGameCommand(IGameCommand gameCommand);
        void Init();
        void Update();
        void Draw();
    }
}

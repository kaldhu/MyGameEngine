using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AWGP3Squared.Interfaces;
using SFML.Graphics;

namespace AWGP3Squared.Interfaces
{
    interface ITextureManager
    {

        Dictionary<string, Texture> m_Sprites { get; }
        
        Texture GetTextureByID(string spriteID);
        
        string AddTexture(Texture tex, string ID = "");
        
        string loadTexture(string path, string ID = "");
    }
}

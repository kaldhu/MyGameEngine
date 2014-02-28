using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;
using AWGP3Squared.Maths;

using SFML.Graphics;

namespace AWGP3Squared.Graphics
{
    class SpriteManager : ITextureManager
    {

        private static ITextureManager m_Instance;

        public Dictionary<string, Texture> m_Sprites { get; private set; }

        private string m_LastKey;

        protected SpriteManager()
        {
            m_Sprites = new Dictionary<string, Texture>();
            m_LastKey = "0";
        }

        public static ITextureManager Instance()
        {
            if (m_Instance == null)
            {
                m_Instance = new SpriteManager();
            }
            return m_Instance;
        }

        public Texture GetTextureByID(string spriteID)
        {
            try
            {
                Texture retTex = m_Sprites[spriteID];
                return retTex;
            }
            catch
            {
                Color[,] tempPixelArr = new Color[,] { { new Color(255, 255, 255), new Color(255, 255, 255) },
                                                        { new Color(255, 255, 255), new Color(255, 255, 255) } }; //2X2 Pixel Black Square
                return new Texture(new Image(tempPixelArr));
            }

        }

        public string AddTexture(Texture tex, string ID = "")
        {
            if (ID == "") 
            { 
                ID = GenerateNextKey();
            }
            m_Sprites.Add(ID, tex);
            return ID;
        }

        public string loadTexture(string path, string ID = "")
        {
            try
            {
                Texture loadedTex = new Texture(path);
                return AddTexture(loadedTex,ID);
            }
            catch
            {
                Console.WriteLine("SpriteManager: Unable to load Texture,\n Path Not Valid: " + path);
                return "";
            }
        }

        private string GenerateNextKey()
        {
            int lastKeyAsInt = Convert.ToInt32(m_LastKey);
            m_LastKey = "" +(++lastKeyAsInt);
            return m_LastKey;
        }
    }
}

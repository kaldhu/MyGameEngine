using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;
using AWGP3Squared.Maths;
using System.Drawing;

namespace AWGP3Squared.Graphics
{
    class GraphicsSettings2D : IGraphicsSettings
    {
        public string m_Title { get; private set; }

        public Vector2 m_ScreenSize { get; private set; }

        public bool m_FullScreen { get; private set; }

        public bool m_VSync { get; private set; }

        public bool m_MouseEnabled { get; private set; }

        public Vector3 m_BackBufferClear { get; private set; }

        public void Initialise(Vector2 screenSize, bool fullScreen, bool vSync = true, bool mouseEnabled = false, Vector3 backBufferClear = null)
        {
            if(backBufferClear == null)
            {
                m_BackBufferClear = new Vector3(255, 128, 0);
            }
            else
            {
                m_BackBufferClear = backBufferClear;
            }

            if (!fullScreen)
            {
                m_ScreenSize = screenSize;
            }
            else
            {
                Rectangle screen = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
                m_ScreenSize = new Vector2(screen.Width, screen.Height);
            }
            m_FullScreen = fullScreen;
            m_VSync = vSync;
            m_MouseEnabled = mouseEnabled;
        }


        
    }
}

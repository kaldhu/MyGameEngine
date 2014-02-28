using AWGP3Squared.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGP3Squared.Interfaces

{
    interface IGraphicsSettings
    {
        string m_Title { get; }
        Vector2 m_ScreenSize { get; }
        bool m_FullScreen { get; }
        bool m_VSync { get; }
        bool m_MouseEnabled { get; }
        Vector3 m_BackBufferClear { get; }

        void Initialise(Vector2 screenSize, bool fullScreen, bool vSync = true, bool mouseEnabled = false, Vector3 backBufferClear = null);
    }
}

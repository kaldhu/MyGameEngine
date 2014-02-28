using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;
using AWGP3Squared.Maths;
using AWGP3Squared.Components;

using SFML.Graphics;
using SFML.Window;
using System.Windows.Forms;

namespace AWGP3Squared.Graphics
{
    class SFMLGraphics2D : IGraphics
    {
        //IModule Vars/Methods
        public Constant.enumModuleID m_ModuleID { get; private set; }
        public IPostOffice m_PostOffice { get; private set; }
        public Constant.enumMessage[] m_MsgTypeArray { get; private set; }

        //Graphics Vars
        private ITextureManager m_SpriteManager;
        private RenderWindow m_RenderWindow;
        private IGraphicsSettings m_GraphicsSettings;

        //Event Handler Vars
        private EventHandler<SFML.Window.KeyEventArgs> m_KeyEventHandler;
        private EventHandler<MouseButtonEventArgs> m_MouseButtonEventHandler;
        private EventHandler<MouseMoveEventArgs> m_MouseMoveEventHandler;
        private EventHandler<MouseWheelEventArgs> m_MouseWheelEventHandler;

        //Output Const (TODO: Move Output Code to a new class)
        private enum m_ErrorStatus
        {
            SEVERE,
            DEBUG,
        }

        private SFMLGraphics2D()
        {
            m_PostOffice = null;
            m_RenderWindow = null;
            m_SpriteManager = null;
            m_GraphicsSettings = null;
            m_ModuleID = Constant.enumModuleID.GRAPHICS;
            m_MsgTypeArray = new Constant.enumMessage[]
            {
                Constant.enumMessage.INITIALISE,
                Constant.enumMessage.UPDATE,
                Constant.enumMessage.DRAW,
                Constant.enumMessage.CLOSING,

                Constant.enumMessage.LOAD_TEXTURES,
                Constant.enumMessage.UPDATE_GRAPHICS_SETTINGS,
                Constant.enumMessage.SWITCH_SPRITES_DRAW_STATUS,

                Constant.enumMessage.GET_GRAPHICS_SETTINGS,
                Constant.enumMessage.SEND_GAMEOBJECT,

                Constant.enumMessage.SEND_SPRITE_COMPONENTS,
                Constant.enumMessage.SEND_POSITION_COMPONENTS,
                Constant.enumMessage.SEND_TEXTURE_IDS,                
                Constant.enumMessage.SEND_COMPONENTS,
                Constant.enumMessage.SEND_GAMEOBJECT,
                Constant.enumMessage.SEND_KEYBOARD_EVENT_HANDLER,
                Constant.enumMessage.SEND_MOUSE_BUTTON_EVENT_HANDLER,
                Constant.enumMessage.SEND_MOUSE_MOVE_EVENT_HANDLER,
                Constant.enumMessage.SEND_MOUSE_WHEEL_EVENT_HANDLER,
                Constant.enumMessage.SEND_GAMEOBJECTCOMPONENTS
            };
        }

        public SFMLGraphics2D(IPostOffice pOff)
            : this()
        {
            m_PostOffice = pOff;
        }

        private bool CheckMemberVariables()
        {
            if (m_MsgTypeArray == null)
            {
                PrintError("MessageTypeArray Not Initialised", m_ErrorStatus.DEBUG);
                return false;
            }
            if (m_SpriteManager == null)
            {
                PrintError("SpriteManager Not Initialised", m_ErrorStatus.DEBUG);
                return false;
            }
            if (m_RenderWindow == null)
            {
                if (!UserPromptedInitError("SDL Render Window has not been Initialised."))
                {
                    return false;
                }
            }

            return true;
        }

        private bool UserPromptedInitError(string error)
        {
            DialogResult errRes = DialogResult.Retry;
            while (errRes != DialogResult.Ignore)
            {
                errRes = PrintError(error, m_ErrorStatus.SEVERE);
                if (errRes == DialogResult.Retry)
                {
                    Init();
                }
                else if (errRes == DialogResult.Abort)
                {
                    return false;
                }
            }
            return true;
        }

        private void Update()
        {
            m_RenderWindow.DispatchEvents();
            if (!m_RenderWindow.IsOpen())
            {
                m_PostOffice.SendMessage(Constant.enumMessage.CLOSING);
            }
        }

        private void Render()
        {
            if (CheckMemberVariables())
            {
                m_RenderWindow.Clear(new Color((byte)((int)m_GraphicsSettings.m_BackBufferClear.x), (byte)((int)m_GraphicsSettings.m_BackBufferClear.y), (byte)((int)m_GraphicsSettings.m_BackBufferClear.z)));

                m_PostOffice.SendMessage(Constant.enumMessage.GET_COMPONENTS, new string[] { Constant.enumComponent.SPRITE.ToString() });
                SpriteComponent[] SpriteComponents = Methods.GetObjectArrayOf<IComponent>(m_PostOffice.GetMessageObjects(m_ModuleID, Constant.enumMessage.SEND_COMPONENTS)).OfType<SpriteComponent>().ToArray();

                m_PostOffice.SendMessage(Constant.enumMessage.GET_COMPONENTS, new string[] { Constant.enumComponent.POSITIONCOMPONENT3D.ToString() });
                PositionComponent3D[] PositionComponents = Methods.GetObjectArrayOf<IComponent>(m_PostOffice.GetMessageObjects(m_ModuleID, Constant.enumMessage.SEND_COMPONENTS)).OfType<PositionComponent3D>().ToArray();


                if (SpriteComponents.Length > PositionComponents.Length)
                {
                    PrintError("There are more Sprites than Positions received, check every GameObject with sprite has a Position", m_ErrorStatus.DEBUG);
                }
                else if (PositionComponents.Length >= SpriteComponents.Length)
                {
                    int j = 0;
                    for (int i = 0; i < PositionComponents.Length && j < SpriteComponents.Length; i++)
                    {
                        if (SpriteComponents[i].m_isDrawn)
                        {
                            Sprite currSprite = new Sprite(m_SpriteManager.GetTextureByID(SpriteComponents[i].m_SpriteID));
                            currSprite.Position = new Vector2f(PositionComponents[i].m_Position.x, PositionComponents[i].m_Position.y);
                            m_RenderWindow.Draw(currSprite);
                        }
                        if (j <= SpriteComponents.Length)
                            j++;
                    }
                }
                m_RenderWindow.Display();
            }
        }


        private void UpdateGraphicsSettings(IGraphicsSettings graphicsSettings)
        {
            m_GraphicsSettings = graphicsSettings;

            m_PostOffice.SendMessage(Constant.enumMessage.GET_INPUT_EVENT_HANDLERS);

            if (m_RenderWindow != null)
            {
                m_RenderWindow.Clear();
                m_RenderWindow.Close();
                
            }
            m_RenderWindow = new RenderWindow(new VideoMode((uint)m_GraphicsSettings.m_ScreenSize.x, (uint)m_GraphicsSettings.m_ScreenSize.y), m_GraphicsSettings.m_Title,m_GraphicsSettings.m_FullScreen ? Styles.Fullscreen : Styles.Default );
            m_RenderWindow.SetVerticalSyncEnabled(m_GraphicsSettings.m_VSync);
            m_RenderWindow.Closed += new EventHandler(CloseWindow);
            m_RenderWindow.KeyPressed += m_KeyEventHandler;
            m_RenderWindow.KeyReleased += m_KeyEventHandler;
            m_RenderWindow.MouseButtonPressed += m_MouseButtonEventHandler;
            m_RenderWindow.MouseButtonReleased += m_MouseButtonEventHandler;
            m_RenderWindow.MouseMoved += m_MouseMoveEventHandler;
            m_RenderWindow.MouseWheelMoved += m_MouseWheelEventHandler;
        }

        public void Init()
        {
            if (m_GraphicsSettings == null)
            {
                m_GraphicsSettings = Methods.GetObjectOf<IGraphicsSettings>(m_PostOffice.GetMessageObjects(m_ModuleID, Constant.enumMessage.GET_GRAPHICS_SETTINGS));
                if (m_GraphicsSettings == null)
                {
                    m_GraphicsSettings = new GraphicsSettings2D();
                }
                m_GraphicsSettings.Initialise(new Vector2(800, 600), false, true, true);
            }
            UpdateGraphicsSettings(m_GraphicsSettings);

        }

        public void Notify(Boolean firstPass)
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
                    case Constant.enumMessage.DRAW:
                        Render();
                        break;
                    case Constant.enumMessage.GET_GRAPHICS_SETTINGS:
                        SendGraphicsSettings();
                        break;
                    case Constant.enumMessage.UPDATE_GRAPHICS_SETTINGS:
                        IGraphicsSettings[] sentSettings = Methods.GetObjectArrayOf<IGraphicsSettings>(m_PostOffice.GetMessageObjects(m_ModuleID, c));
                        if (sentSettings.Length != 0)
                        {
                            UpdateGraphicsSettings(sentSettings[0]);
                        }
                        break;
                    case Constant.enumMessage.LOAD_TEXTURES:
                        AddTextures();
                        break;
                    case Constant.enumMessage.SEND_MOUSE_WHEEL_EVENT_HANDLER:
                        m_MouseWheelEventHandler = Methods.GetObjectArrayOf<EventHandler<MouseWheelEventArgs>>(m_PostOffice.GetMessageObjects(m_ModuleID, c)).FirstOrDefault();
                        break;
                    case Constant.enumMessage.SEND_MOUSE_BUTTON_EVENT_HANDLER:
                        m_MouseButtonEventHandler = Methods.GetObjectArrayOf<EventHandler<MouseButtonEventArgs>>(m_PostOffice.GetMessageObjects(m_ModuleID, c)).FirstOrDefault();
                        break;
                    case Constant.enumMessage.SEND_MOUSE_MOVE_EVENT_HANDLER:
                        m_MouseMoveEventHandler = Methods.GetObjectArrayOf<EventHandler<MouseMoveEventArgs>>(m_PostOffice.GetMessageObjects(m_ModuleID, c)).FirstOrDefault();
                        break;
                    case Constant.enumMessage.SEND_KEYBOARD_EVENT_HANDLER:
                        m_KeyEventHandler = Methods.GetObjectArrayOf<EventHandler<SFML.Window.KeyEventArgs>>(m_PostOffice.GetMessageObjects(m_ModuleID, c)).FirstOrDefault();
                        break;
                    case Constant.enumMessage.SWITCH_SPRITES_DRAW_STATUS:
                        SwitchSpritesDrawState(Methods.GetObjectArrayOf<string>(m_PostOffice.GetMessageObjects(m_ModuleID, c)));
                        break;
                    case Constant.enumMessage.CLOSING:
                        DisposeRenderWindow();
                        break;
                    default:
                        PrintError(("Unhandled PostOffice Message - " + c.ToString()), m_ErrorStatus.DEBUG);
                        break;
                }
                m_PostOffice.UnLockMessageType(c);
            }
        }

        private void AddTextures()
        {

            object[] textureArgs = m_PostOffice.GetMessageObjects(m_ModuleID, Constant.enumMessage.LOAD_TEXTURES);

            string[] texpaths= Methods.GetObjectArrayOf<string>(textureArgs);
            string[] IDs = null;

            if (texpaths.Length == 0)
            {

                object[][] texArgs = (object[][])textureArgs[0];
                if (texArgs.Length == 2)
                {
                    texpaths = (string[])texArgs[0];
                    IDs = (string[])texArgs[1];
                }
                else
                {
                    PrintError("Too Many things arrived with LOAD_TEXTURES Method!");
                }
            }

            AddTextures(texpaths,IDs);
        }

        private void SwitchSpritesDrawState(string[] objectIDs)
        {
            foreach (string key in objectIDs)
            {
                m_PostOffice.SendMessage(Constant.enumMessage.GET_GAMEOBJECT, new string[]{key});
                m_PostOffice.SendMessage(Constant.enumMessage.GET_GAMEOBJECT, new string[]{key});
                IGameObject currObject = Methods.GetObjectArrayOf<IGameObject>(m_PostOffice.GetMessageObjects(m_ModuleID, Constant.enumMessage.SEND_GAMEOBJECT)).Where(gobj => gobj.m_GameObjectKey == key).FirstOrDefault();

                m_PostOffice.SendMessage(Constant.enumMessage.GET_GAMEOBJECTCOMPONENTS, new Object[]{currObject.m_GameObjectKey, new Constant.enumComponent[]{Constant.enumComponent.SPRITE}} );
                ISpriteComponent[] spriteComponents = Methods.GetObjectArrayOf<IComponent>(m_PostOffice.GetMessageObjects(m_ModuleID, Constant.enumMessage.SEND_GAMEOBJECTCOMPONENTS)).OfType<ISpriteComponent>().ToArray();
                foreach (ISpriteComponent currentSprite in spriteComponents)
                {
                    currentSprite.SwitchDrawState();
                }
            }
        }

        private void SendGraphicsSettings()
        {
            m_PostOffice.SendMessage(Constant.enumMessage.SEND_GRAPHICS_SETTINGS, new IGraphicsSettings[] { m_GraphicsSettings });
        }

        private string AddTexture(string path, string ID = "")
        {
            if (m_SpriteManager == null)
            {
                m_SpriteManager = SpriteManager.Instance();
            }
            string generatedID = m_SpriteManager.loadTexture(path, ID);
            PrintError(("Texture Added with ID: " + generatedID + "\n Path:" + path), m_ErrorStatus.DEBUG);
            return generatedID;
        }

        //HACK: This needs to be in an ouput class, Windows.Forms causes class conflicts with SFML.Windows
        private DialogResult PrintError(string errmssg, m_ErrorStatus errorStatus)
        {
            if (errorStatus == m_ErrorStatus.SEVERE)
            {
#if DEBUG
                Console.WriteLine("SDLGraphics2D: " + errmssg);
#endif
                return MessageBox.Show(errmssg, "SEVERE GRAPHICS ERROR", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                PrintError(errmssg);
                return DialogResult.Cancel;
            }
        }


        public void PrintError(string errmssg)
        {
#if DEBUG
            Console.WriteLine("SDLGraphics:" + errmssg);
#endif
        }

        private void AddTextures(string[] textures, string[] IDs = null)
        {
            string[] generatedIDs = new string[textures.Length];
            int j = 0;
            for (int i = 0; i < textures.Length; i++)
            {
                if (IDs != null && j < IDs.Length)
                {
                    generatedIDs[i] = AddTexture(textures[i], IDs[j]);
                    j++;
                }
                else
                {
                    generatedIDs[i] = AddTexture(textures[i]);
                }
            }
            m_PostOffice.SendMessage(Constant.enumMessage.SEND_TEXTURE_IDS, generatedIDs, false);
        }

        static void CloseWindow(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        private void DisposeRenderWindow()
        {
            m_RenderWindow.Close();
        }
    }
}

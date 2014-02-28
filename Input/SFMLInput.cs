using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;
using AWGP3Squared.Maths;
using SFML.Window;

namespace AWGP3Squared.Input
{
    class SFMLInput : IInput
    {
        private static SFMLInput m_Instance;
        private static List<KeyEventArgs> m_NewKeyEvents;
        private static MouseWheelEventArgs m_NewMouseWheelEventArgs;
        private static List<MouseButtonEventArgs> m_NewMouseButtonEvents;
        private static Vector2 m_MousePosition { get; set; }

        private IPostOffice m_PostOffice { get; set; }
        public Constant.enumModuleID m_ModuleID { get; private set; }
        public Constant.enumMessage[] m_MsgTypeArray{ get; private set; }

        private List<Constant.enumKey> m_PressedKeys { get; set; }
        private List<Constant.enumMouseButton> m_PressedMouseButtons { get; set; }
        private int m_MouseWheelDelta { get; set; }

        protected SFMLInput()
        {
            m_PostOffice = null;
            m_ModuleID = Constant.enumModuleID.INPUT;
            m_PressedKeys = new List<Constant.enumKey>();
            m_PressedMouseButtons = new List<Constant.enumMouseButton>();
            m_MousePosition = new Vector2();
            m_MouseWheelDelta = 0;
            m_MsgTypeArray = new Constant.enumMessage[]
            {
                Constant.enumMessage.INITIALISE,
                Constant.enumMessage.UPDATE,
                Constant.enumMessage.GET_INPUT_EVENT_HANDLERS,
                Constant.enumMessage.GET_MOUSE_POSITION,
                Constant.enumMessage.GET_PRESSED_KEYS,
                Constant.enumMessage.GET_PRESSED_MOUSEBUTTONS,
                Constant.enumMessage.GET_MOUSE_WHEEL_DELTA,

            };
        }

        protected SFMLInput(IPostOffice postOffice) : this()
        {
            m_PostOffice = postOffice;
        }

        public static SFMLInput Instance(IPostOffice postOffice)
        {
            if (m_Instance == null)
            {
                m_Instance = new SFMLInput(postOffice);
            }

            return m_Instance;
        }

        public void Init()
        {
            //throw new NotImplementedException();
        }

        public void Update()
        {
            UpdateKeyEvents();
            UpdateMouseButtonEvents();
            UpdateMouseDelta();
        }

        private void UpdateMouseDelta()
        {
            if (m_NewMouseWheelEventArgs != null)
            {
                m_MouseWheelDelta = m_NewMouseWheelEventArgs.Delta;
            }
            else
            {
                m_MouseWheelDelta = 0;
            }
        }

        private void UpdateKeyEvents()
        {
            if (m_NewKeyEvents != null)
            {
                foreach (KeyEventArgs e in m_NewKeyEvents)
                {
                    m_PressedKeys.Add(KeyEventToEnum(e));
                }
                m_NewKeyEvents = null;
            }
            else
            {
                m_PressedKeys.Clear();
            }

        }

        private void UpdateMouseButtonEvents()
        {
            if (m_NewMouseButtonEvents != null)
            {
                foreach (MouseButtonEventArgs e in m_NewMouseButtonEvents)
                {
                    m_PressedMouseButtons.Add(MouseButtonEventToEnum(e));
                }
                m_NewMouseButtonEvents = null;
            }
            else
            {
                m_PressedMouseButtons.Clear();
            }

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
                    case Constant.enumMessage.GET_INPUT_EVENT_HANDLERS:
                        SendInputEventHandlers();
                        break;
                    case Constant.enumMessage.GET_MOUSE_POSITION:
                        SendMousePosition();
                        break;
                    case Constant.enumMessage.GET_PRESSED_MOUSEBUTTONS:
                        SendPressedMouseButtons();
                        break;
                    case Constant.enumMessage.GET_MOUSE_WHEEL_DELTA:
                        SendMouseWheelDelta();
                        break;
                    case Constant.enumMessage.GET_PRESSED_KEYS:
                        SendKeysPressed();
                        break;
                    default:
                        PrintError("Unhandled PostOffice Message - " + c.ToString());
                        break;
                }
                m_PostOffice.UnLockMessageType(c);
            }
        }

        private void SendKeysPressed()
        {
            string[] pressedKeys = new string[m_PressedKeys.Count];

            for (int i = 0; i < m_PressedKeys.Count; i++)
            {
                pressedKeys[i] = m_PressedKeys[i].ToString();
            }

            m_PostOffice.SendMessage(Constant.enumMessage.SEND_PRESSED_KEYS, pressedKeys);
        }

        private void SendMouseWheelDelta()
        {
            m_PostOffice.SendMessage(Constant.enumMessage.SEND_MOUSE_WHEEL_DELTA, new string[] { m_MouseWheelDelta.ToString() });
        }

        private void SendPressedMouseButtons()
        {
            string[] pressedMBs = new string[m_PressedMouseButtons.Count];

            for (int i = 0; i < m_PressedMouseButtons.Count; i++)
            {
                pressedMBs[i] = m_PressedMouseButtons[i].ToString();
            }

            m_PostOffice.SendMessage(Constant.enumMessage.SEND_PRESSED_MOUSEBUTTONS, pressedMBs);
        }

        private void SendMousePosition()
        {
            m_PostOffice.SendMessage(Constant.enumMessage.SEND_MOUSE_POSITION, new Vector2[] { m_MousePosition });
        }

        private void SendInputEventHandlers()
        {
            m_PostOffice.SendMessage(Constant.enumMessage.SEND_KEYBOARD_EVENT_HANDLER, new EventHandler<KeyEventArgs>[] { new EventHandler<KeyEventArgs>(HandleKeyPressed) });
            m_PostOffice.SendMessage(Constant.enumMessage.SEND_MOUSE_BUTTON_EVENT_HANDLER, new EventHandler<MouseButtonEventArgs>[] { new EventHandler<MouseButtonEventArgs>(HandleMouseButtonEvent) });
            m_PostOffice.SendMessage(Constant.enumMessage.SEND_MOUSE_WHEEL_EVENT_HANDLER, new EventHandler<MouseWheelEventArgs>[] { new EventHandler<MouseWheelEventArgs>(HandleMouseWheelEvent) });
            m_PostOffice.SendMessage(Constant.enumMessage.SEND_MOUSE_MOVE_EVENT_HANDLER, new EventHandler<MouseMoveEventArgs>[] { new EventHandler<MouseMoveEventArgs>(HandleMouseMoveEvent) });
        }

        public void PrintError(string errmssg)
        {
#if DEBUG
            Console.WriteLine("SFMLInput: " + errmssg);
#endif
        }

        private Constant.enumKey[] getPressedKeys()
        {
            return m_PressedKeys.ToArray();
        }

        private Constant.enumMouseButton[] getPressedMouseButtons()
        {
            return m_PressedMouseButtons.ToArray();
        }

        private Constant.enumMouseButton MouseButtonEventToEnum(MouseButtonEventArgs e)
        {
            switch (e.Button)
            {
                case Mouse.Button.Left:
                    return Constant.enumMouseButton.LEFT;
                case Mouse.Button.Right:
                    return Constant.enumMouseButton.RIGHT;
                case Mouse.Button.Middle:
                    return Constant.enumMouseButton.MIDDLE;
                default:
                    PrintError("Unhandled MouseButtonEvent: " + e.Button.ToString());
                    return Constant.enumMouseButton.NULL;
            }
        }

        private Constant.enumKey KeyEventToEnum(KeyEventArgs e)
        {
            if (Enum.GetNames(typeof(Constant.enumKey)).Contains(e.Code.ToString()))
            {
                return (Constant.enumKey)Enum.Parse(typeof(Constant.enumKey),e.Code.ToString());
            }
            else
            {
                    PrintError("Unhandled Key: " + e.Code.ToString());
                    return Constant.enumKey.NULL;
            }
        }

        public static void HandleKeyPressed(object sender, KeyEventArgs e)
        {
            if (m_NewKeyEvents == null)
            {
                m_NewKeyEvents = new List<KeyEventArgs>();
            }
            m_NewKeyEvents.Add(e);
        }

        public static void HandleMouseButtonEvent(object sender, MouseButtonEventArgs e)
        {
            if (m_NewMouseButtonEvents == null)
            {
                m_NewMouseButtonEvents = new List<MouseButtonEventArgs>();
            }
            m_NewMouseButtonEvents.Add(e);
            m_MousePosition.x = e.X;
            m_MousePosition.y = e.Y;
        }

        public static void HandleMouseWheelEvent(object sender, MouseWheelEventArgs e)
        {
            m_NewMouseWheelEventArgs = e;
            m_MousePosition.x = e.X;
            m_MousePosition.y = e.Y;
        }

        public static void HandleMouseMoveEvent(object sender, MouseMoveEventArgs e)
        {
            m_MousePosition.x = e.X;
            m_MousePosition.y = e.Y;
        }
    }
}

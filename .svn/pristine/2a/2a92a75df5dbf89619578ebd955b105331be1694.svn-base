using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Maths;

namespace AWGP3Squared.Helper
{

    static class Constant
    {
        private static Vector3 g_VGravity = new Vector3(0.0f, -10.0f, 0.0f);
        public static double elapsedTime { get; set; }
        public static DateTime previousTime { get; set; }

        public static void SetGravity(Vector3 gravity)
        {
            g_VGravity = gravity;
        }
        public static void SetGravity(float x, float y, float z)
        {
            Vector3 gravity = new Vector3(x, y, z);
            g_VGravity = gravity;
        }
        public static Vector3 GetGravity()
        {
            return g_VGravity;
        }

        //MessageType Constants
        public enum enumMessage
        { // Enum Name                          //Expected Message Creation
            NULL,

            INITIALISE,
            UPDATE,
            DRAW,
            CLOSING,      

            SYNC_POSITIONDATA,
            ADD_COMPONENTS_TO_OBJECT,
            ADD_SCRIPT_TO_OBJECT,
            CREATE_OBJECTS,
            COLLISIONDETECTION,

            GET_POSITION_COMPONENTS,
            GET_SPRITE_COMPONENTS,
            GET_COMPONENTS,
            GET_GAMEOBJECT,
            GET_COLLISIONDETECTIONDATA,
            GET_GAMEOBJECTCOMPONENTS,
            GET_GRAPHICS_SETTINGS,
            GET_GAMEOBJECT_BY_ID,
            GET_GAMEOBJECTCOMPONENTS_BY_ID,
            GET_INPUT_EVENT_HANDLERS,
            GET_PRESSED_KEYS,
            GET_PRESSED_MOUSEBUTTONS,
            GET_MOUSE_POSITION,
            GET_MOUSE_WHEEL_DELTA,

            SEND_KEYBOARD_EVENT_HANDLER,
            SEND_MOUSE_MOVE_EVENT_HANDLER,
            SEND_MOUSE_BUTTON_EVENT_HANDLER,
            SEND_MOUSE_WHEEL_EVENT_HANDLER,
            SEND_PRESSED_KEYS,
            SEND_PRESSED_MOUSEBUTTONS,
            SEND_MOUSE_POSITION,
            SEND_MOUSE_WHEEL_DELTA,
            SEND_POSITION_COMPONENTS,
            SEND_POSITIONCOMPONENTS,
            SEND_SPRITE_COMPONENTS,
            SEND_COMPONENTS,
            SEND_GAMEOBJECT,
            SEND_COLLISIONDETECTIONDATA,
            SEND_GAMEOBJECTCOMPONENTS,
            SEND_GRAPHICS_SETTINGS,
            SEND_TEXTURE_IDS,

            LOAD_TEXTURES,
            UPDATE_GRAPHICS_SETTINGS,
            COLLISIONDETECTIONDATA,
            SWITCH_SPRITES_DRAW_STATUS,
            GET_GAMEOBJECTKEY_BY_ID,
            SEND_GAMEOBJECTKEY,
        }

        //ComponentType Constants
        public enum enumComponent
        { // Enum Name                          //Expected Message Creation
            NULL,
            COLLIDER,
            RIGIDBODY,
            POSITIONCOMPONENT3D,
            SPRITE,
            SCRIPT,
            SCRIPTCONTROLLER,
            CURRENTSTATE,
        }

        /*
        * Discrete CD is traditional checking each frame for collision
        * Continous CD is predicting the path of the objects to determine if they will collide before next update
        * DISCRETE_CD will use discrete collision agaisnt all colliders
        * DYNAMIC_CD will use discrete CD with DISCRETE_CD objects and Continous CD with CONTINUOUS_CD and DYNAMIC_CD objects
        * CONTINUOUS_CD will use continuous CD with CONTINUOUS_CD AND DYNAMIC_CD and static and discrete agaisnt DISCRETE_CD
        */

        public enum enumCollisionDetection
        { // Enum Name      
            NULL,//Expected Message Creation
            DISCRETE_CD,
            //DYNAMIC_CD,  //This might not be needed tried to copy from unity but makes no sense right now
            CONTINUOUS_CD,
        }

        public enum enumModuleID
        { // Enum Name                          //Expected Message Creation
            NULL,
            GAMEOBJECTMANAGER,
            PHYSICS,
            GRAPHICS,
            AI,
            INPUT,
            GAMECOMMAND,
            SCRIPTCONTROLLER,
        }

        /// <summary>
        /// An Enum for Keyboard Keys, taken from SFML's Keyboard.Keys enum other than "NULL" used as an error value
        /// </summary>
        public enum enumKey
        {
            Unknown,
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H,
            I,
            J,
            K,
            L,
            M,
            N,
            O,
            P,
            Q,
            R,
            S,
            T,
            U,
            V,
            W,
            X,
            Y,
            Z,
            Num0,
            Num1,
            Num2,
            Num3,
            Num4,
            Num5,
            Num6,
            Num7,
            Num8,
            Num9,
            Escape,
            LControl,
            LShift,
            LAlt,
            LSystem,
            RControl,
            RShift,
            RAlt,
            RSystem,
            Menu,
            LBracket,
            RBracket,
            SemiColon,
            Comma,
            Period,
            Quote,
            Slash,
            BackSlash,
            Tilde,
            Equal,
            Dash,
            Space,
            Return,
            BackSpace,
            Tab,
            PageUp,
            PageDown,
            End,
            Home,
            Insert,
            Delete,
            Add,
            Subtract,
            Multiply,
            Divide,
            Left,
            Right,
            Up,
            Down,
            Numpad0,
            Numpad1,
            Numpad2,
            Numpad3,
            Numpad4,
            Numpad5,
            Numpad6,
            Numpad7,
            Numpad8,
            Numpad9,
            F1,
            F2,
            F3,
            F4,
            F5,
            F6,
            F7,
            F8,
            F9,
            F10,
            F11,
            F12,
            F13,
            F14,
            F15,
            Pause,
            KeyCount,
            NULL,
        }

        public enum enumMouseButton
        {
            NULL,
            LEFT,
            RIGHT,
            MIDDLE,
        }

        public enum enumAIState
        {
            INITIALISE,
            IDLE,
            WALK,
            ATTACK,
            RETREAT,
            DIE,
        }
    }
}
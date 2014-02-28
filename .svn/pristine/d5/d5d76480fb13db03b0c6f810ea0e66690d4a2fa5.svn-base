using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Components;
using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;
using AWGP3Squared.Maths;

namespace AWGP3Squared.Scripts
{
    class TestScript : IScript, ICollisionDetection
    {
        public IScriptController m_ScriptController {get; set;}
        public IGameObject m_GameObject { get; set; }

        public void Init()
        {
            string[] textures = {"Resources/ship.jpg" };
            string[] spriteIDs = m_ScriptController.RequestData<string>(Constant.enumMessage.LOAD_TEXTURES, textures); //Load Textures and Get related IDS

            IComponent[] playerCompArray = { new RigidBody(1, 0.0f, 0.0f, false, true), new Collider(new Vector3(150, 80, 0)), new SpriteComponent(spriteIDs[0]) };
            m_ScriptController.SendDataForObject<IComponent>(Constant.enumMessage.ADD_COMPONENTS_TO_OBJECT, m_GameObject.m_GameObjectKey, playerCompArray);

            m_ScriptController.SendData(Constant.enumMessage.SWITCH_SPRITES_DRAW_STATUS, new String[] { m_GameObject.m_GameObjectKey }); 


        }

        public void Update()
        {
            
            IGraphicsSettings graphicSetting = m_ScriptController.RequestData<IGraphicsSettings>(Constant.enumMessage.GET_GRAPHICS_SETTINGS).FirstOrDefault();
            //IRigidBody playerRigidBody = m_ScriptController.RequestData<IComponent>(Constant.enumMessage.GET_GAMEOBJECTCOMPONENTS, new Object[] { m_GameObject.m_GameObjectKey, new Constant.enumComponent[] { Constant.enumComponent.RIGIDBODY } }).OfType<IRigidBody>().FirstOrDefault();
            IRigidBody playerRigidBody = m_GameObject.getComponent<IRigidBody>(Constant.enumComponent.RIGIDBODY);
            if (playerRigidBody.m_Position.ReturnAnyLessThen(new Vector3(-90,-40,0)) || playerRigidBody.m_Position.ReturnAnyGreaterThen(new Vector3(graphicSetting.m_ScreenSize.x, graphicSetting.m_ScreenSize.y, 0)))
            {
               // IPositionComponent3D playerPos = m_ScriptController.RequestData<IComponent>(Constant.enumMessage.GET_GAMEOBJECTCOMPONENTS, new Object[] { "player", new Constant.enumComponent[] { Constant.enumComponent.POSITIONCOMPONENT3D } }).OfType<IPositionComponent3D>().FirstOrDefault();
                IPositionComponent3D playerPos = m_GameObject.getComponent<IPositionComponent3D>(Constant.enumComponent.POSITIONCOMPONENT3D);
                playerPos.m_Position.x = (graphicSetting.m_ScreenSize.x / 2)-45;
                playerPos.m_Position.y = graphicSetting.m_ScreenSize.y / 2;
                playerRigidBody.m_Velocity = new Vector3();
            }

            string[] pressedKeys = m_ScriptController.RequestData<string>(Constant.enumMessage.GET_PRESSED_KEYS);
            foreach (string key in pressedKeys)
            {
                Constant.enumKey currKey = (Constant.enumKey)Enum.Parse(typeof(Constant.enumKey), key);
                switch (currKey)
                {
                    case Constant.enumKey.Up:
                        playerRigidBody.AddForce(0, -500, 0); ;
                        break;
                    case Constant.enumKey.Down:
                        playerRigidBody.AddForce(0, 500, 0); ;
                        break;
                    case Constant.enumKey.Left:
                        playerRigidBody.AddForce(-500, 0, 0); ;
                        break;
                    case Constant.enumKey.Right:
                        playerRigidBody.AddForce(500, 0, 0); ;
                        break;
                }
            }
        }

        public void Draw()
        {
        }
        
        public void CollisionDetection(String gameObjectKey)
        {
        }
    }
}

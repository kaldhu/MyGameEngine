using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWGP3Squared.Components;
using AWGP3Squared.Scripts;
using AWGP3Squared.Core;
using AWGP3Squared.Helper;
using AWGP3Squared.Interfaces;
using AWGP3Squared.Maths;
using AWGP3Squared.Graphics;

namespace AWGP3Squared
{
    class GameOne:IGame
    {
        private IGameCommand m_GameCommand;
        private static int rockCount = 5;
        String[] rockKeys = new String[rockCount];
        String playerKey = "player";
        private float m_ScreenHeight;
        private float m_ScreenWidth;
        Random m_Random;

        public void LoadGameCommand(IGameCommand gameCommand)
        {
            m_GameCommand = gameCommand;
        }
        public void Init()
        {
            m_Random = new Random();
            string[] textures = { "Resources/rocktex.jpg"};
            string[] spriteIDs = m_GameCommand.RequestData<string>(Constant.enumMessage.LOAD_TEXTURES, textures); //Load Textures and Get related IDS


            
            IGraphicsSettings graphicsSettings = new GraphicsSettings2D();
            graphicsSettings.Initialise(new Vector2(800, 600), false, true, true);
            m_GameCommand.SendData(Constant.enumMessage.UPDATE_GRAPHICS_SETTINGS, new IGraphicsSettings[] { graphicsSettings });
            m_ScreenHeight = graphicsSettings.m_ScreenSize.y;
            m_ScreenWidth = graphicsSettings.m_ScreenSize.x;

            IGameObject[] rocks = new IGameObject[rockCount];
            for (int i = 0; i < rockCount; i++)
            {
                rockKeys[i] = "rock_" + i.ToString();
                rocks[i] = new GameObject(rockKeys[i], -800, 0, 0);
            }

            m_GameCommand.SendData(Constant.enumMessage.CREATE_OBJECTS, rocks);

            for (int i = 0; i < rockCount; i++)
            {
                IComponent[] rockCompArray = { new RigidBody(5, 0, 0, false, true), new Collider(new Vector3(80, 80, 0)), new SpriteComponent(spriteIDs[0]) };
                rockKeys[i] = "rock_" + i.ToString();
                m_GameCommand.SendData(Constant.enumMessage.ADD_COMPONENTS_TO_OBJECT, new Object[] { rockKeys[i], rockCompArray });
            }

            IGameObject[] player = { new GameObject(playerKey, 0, 0, 0) };
            m_GameCommand.SendData(Constant.enumMessage.CREATE_OBJECTS, player);
            m_GameCommand.SendDataForObject<IScript>(Constant.enumMessage.ADD_SCRIPT_TO_OBJECT, playerKey, new TestScript());

            m_GameCommand.SendData(Constant.enumMessage.SWITCH_SPRITES_DRAW_STATUS, rockKeys);           }

        public void Update()
        {
            IRigidBody[] rockArray = new IRigidBody[rockCount];
            for (int i = 0; i < rockCount; i++)
            {
                rockArray[i] = m_GameCommand.RequestData<IComponent>(Constant.enumMessage.GET_GAMEOBJECTCOMPONENTS, new Object[] { "rock_" + i.ToString(), new Constant.enumComponent[] { Constant.enumComponent.RIGIDBODY } }).OfType<IRigidBody>().FirstOrDefault();
            }

            IPositionComponent3D[] rockPosArray = new IPositionComponent3D[rockCount];
            for (int i = 0; i < rockCount; i++)
            {
                rockPosArray[i] = m_GameCommand.RequestData<IComponent>(Constant.enumMessage.GET_GAMEOBJECTCOMPONENTS, new Object[] { "rock_" + i.ToString(), new Constant.enumComponent[] { Constant.enumComponent.POSITIONCOMPONENT3D } }).OfType<IPositionComponent3D>().FirstOrDefault();
            }

            for (int i = 0;i<rockCount;i++)
            {
                IRigidBody rock= rockArray[i];
                if (rock.m_Position.ReturnAnyLessThen(new Vector3()) || rock.m_Position.ReturnAnyGreaterThen(new Vector3(m_ScreenWidth, m_ScreenHeight, 0)))
                {
                    rockPosArray[i].m_Position = new Vector3(m_ScreenWidth - 1, m_Random.Next((int)m_ScreenHeight), 0);
                    Vector3 test = new Vector3(30,30,0);
                    Boolean wrongPlace = true;
                    while (wrongPlace)
                    {
                        wrongPlace = false;
                        foreach(IPositionComponent3D rockPos in rockPosArray.Where(r=>r.m_GameObjectId != rock.m_GameObjectId))
                        {
                            float distance = Math.Abs(rockPos.m_Position.y - rockPosArray[i].m_Position.y);
                            if(distance< 60 && distance>0)
                            {
                                wrongPlace = true;
                                rockPosArray[i].m_Position = new Vector3(m_ScreenWidth - 1, m_Random.Next((int)m_ScreenHeight), 0);
                            }
                        }
                    }
                    rock.m_Velocity = new Vector3();
                    rock.AddForce(new Vector3(-30000, m_Random.Next(-3,3)*10000, 0));
                }
            }
        }

        public void Draw()
        {
            //throw new NotImplementedException();
        }
    }
}

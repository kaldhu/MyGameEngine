using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AWGP3Squared.Interfaces;
using AWGP3Squared.Core;
using AWGP3Squared.Helper;
using AWGP3Squared.Components;
using AWGP3Squared.Graphics;
using AWGP3Squared.Maths;

namespace AWGP3Squared.Games
{
    class GraphicsExampleGame : IGame
    {
        private IGameCommand m_GameCommand;
        private IGraphicsSettings m_CurrGraphicsSettings;

        private bool m_SetFullscreen = false, m_StateJustChanged = false;

        private int m_CurrState = 0, m_CurrPlayer = 0;
        double m_InputCooldown = 0.125, m_MouseElapsedTime = 0.0, m_KeyboardElapsedTime = 0.0;

        private string[,] m_OObjTable = new string[,]{
                {"TopLeftO","TopCentO","TopRightO"},
                {"CenLeftO","CenCentO","CenRightO"},
                {"BotLeftO","BotCentO","BotRightO"}
            },
            m_XObjTable = new string[,]{
                {"TopLeftX","TopCentX","TopRightX"},
                {"CenLeftX","CenCentX","CenRightX"},
                {"BotLeftX","BotCentX","BotRightX"}
            },
            m_TblObjTable = new string[,]{
                {"TopLeftSqr","TopCentSqr","TopRightSqr"},
                {"CenLeftSqr","CenCentSqr","CenRightSqr"},
                {"BotLeftSqr","BotCentSqr","BotRightsqr"}
            };

        Vector3[,] m_BoardGrid = new Vector3[,] { 
        {new Vector3(10,10,0),new Vector3(657,10,0),new Vector3(1314,10,0)},
        {new Vector3(10,509,0),new Vector3(657,509,0),new Vector3(1314,509,0)},
        {new Vector3(10,1018,0),new Vector3(657,1018,0),new Vector3(1314,1018,0)}
        };

        bool[,] m_BoardIsOccupuied = new bool[3, 3];

        private string[] m_texIDs = new string[]{
            "Naughtspr",
            "CrossSpr",
            "SquareSpr",
            "StartSpr",
            "WinSpr",
            "DrawSpr",
            "AISpr",
        };

        private string[] m_spritePaths = new string[]{
            "resources/GraphicsExampleGame/O.png",
            "resources/GraphicsExampleGame/X.gif",
            "resources/GraphicsExampleGame/TTT_Box.jpg",
            "resources/GraphicsExampleGame/EnterToStart.jpg",
            "resources/GraphicsExampleGame/YouWin.jpg",
            "resources/GraphicsExampleGame/Draw.jpg",
            "resources/GraphicsExampleGame/AIWin.jpg",
        };

        Random m_Random;

        public GraphicsExampleGame()
        {
            m_Random = new Random();
            m_CurrGraphicsSettings = new GraphicsSettings2D();
        }

        public void LoadGameCommand(IGameCommand gameCommand)
        {
            m_GameCommand = gameCommand;
        }

        public void Init()
        {
            IGameObject[] objects = new IGameObject[31];
            SpriteComponent[] sprites = new SpriteComponent[31];
            m_CurrGraphicsSettings.Initialise(new Vector2(1314, 1018), false, true, true, new Vector3(0, 0, 0));

           m_GameCommand.SendData(Constant.enumMessage.LOAD_TEXTURES, new string[][] { m_spritePaths, m_texIDs });

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    objects[((i * 3) + j)] = new GameObject(m_TblObjTable[i, j],m_BoardGrid[i,j]);
                    sprites[((i * 3) + j)] = new SpriteComponent(m_texIDs[2]);
                    sprites[((i * 3) + j)].SwitchDrawState();
                    objects[((i * 3) + j) + 9] = new GameObject(m_XObjTable[i, j], m_BoardGrid[i, j]);
                    sprites[((i * 3) + j) + 9] = new SpriteComponent(m_texIDs[1]);
                    objects[((i * 3) + j) + 18] = new GameObject(m_OObjTable[i, j], m_BoardGrid[i, j]);
                    sprites[((i * 3) + j) + 18] = new SpriteComponent(m_texIDs[0]);
                }
            }
            Vector3 midScreen = new Vector3(m_CurrGraphicsSettings.m_ScreenSize.x/2, m_CurrGraphicsSettings.m_ScreenSize.y/2, 0.0f);
            objects[27] = new GameObject("StartScreen", midScreen);
            sprites[27] = new SpriteComponent(m_texIDs[3]);
            sprites[27].SwitchDrawState();
            objects[28] = new GameObject("WinScreen", midScreen);
            sprites[28] = new SpriteComponent(m_texIDs[4]);
            objects[29] = new GameObject("DrawScreen", midScreen);
            sprites[29] = new SpriteComponent(m_texIDs[5]);
            objects[30] = new GameObject("LoseScreen", midScreen);
            sprites[30] = new SpriteComponent(m_texIDs[6]);

            m_GameCommand.SendData(Constant.enumMessage.CREATE_OBJECTS, objects);

            for(int i = 0; i < sprites.Length; i++)
            {
                m_GameCommand.SendDataForObject<IComponent>(Constant.enumMessage.ADD_COMPONENTS_TO_OBJECT, objects[i].m_GameObjectKey, sprites[i]);
            }
            m_GameCommand.SendData(Constant.enumMessage.UPDATE_GRAPHICS_SETTINGS,new IGraphicsSettings[]{ m_CurrGraphicsSettings});
        }

        public void Update()
        {
            switch (m_CurrState)
            {
                case 0:
                    StartUpdate();
                    break;
                case 1:
                    GameUpdate();
                    break;
                case 2:
                    PlayerWins();
                    break;
                case 3:
                    AIWins();
                    break;
                case 4:
                    GameDraw();
                    break;
                case 5:
                    Close();
                    break;
            }
                
            //Keyboard kybd;
            //throw new NotImplementedException();
        }

        private void GameDraw()
        {
            if (m_StateJustChanged)
            {
                m_GameCommand.SendData(Constant.enumMessage.SWITCH_SPRITES_DRAW_STATUS, new string[] { "DrawScreen" });
                m_StateJustChanged = false;
            }
            StartUpdate();
        }

        private void GameUpdate()
        {
            string[] pressedKeys = m_GameCommand.RequestData<string>(Constant.enumMessage.GET_PRESSED_KEYS);
            bool altPressed = false, userClicked = false;
            if (m_KeyboardElapsedTime > m_InputCooldown)
            {
                foreach (string key in pressedKeys)
                {
                    Constant.enumKey currKey = (Constant.enumKey)Enum.Parse(typeof(Constant.enumKey), key);
                    switch (currKey)
                    {
                        case Constant.enumKey.Escape:
                            m_CurrState = 5;
                            break;
                        case Constant.enumKey.Return:
                            if (altPressed)
                            {
                                m_SetFullscreen = !m_SetFullscreen;
                            }
                            break;
                        case Constant.enumKey.LAlt:
                        case Constant.enumKey.RAlt:
                            altPressed = true;
                            break;
                    }
                }
                m_KeyboardElapsedTime = 0.0;
            }
            else
            {
                m_KeyboardElapsedTime += Constant.elapsedTime;
            }

            string[] pressedMouseButtons = m_GameCommand.RequestData<string>(Constant.enumMessage.GET_PRESSED_MOUSEBUTTONS);
            if (m_MouseElapsedTime > m_InputCooldown)
            {
                foreach (string button in pressedMouseButtons)
                {
                    Constant.enumMouseButton currButton = (Constant.enumMouseButton)Enum.Parse(typeof(Constant.enumMouseButton), button);
                    switch (currButton)
                    {
                        case Constant.enumMouseButton.LEFT:
                            userClicked = true;
                            break;
                    }
                }
                m_MouseElapsedTime = 0.0;
            }
            else
            {
                m_MouseElapsedTime += Constant.elapsedTime;
            }

            if (userClicked && m_CurrPlayer == 0)
            {
                Vector2 MousePos = m_GameCommand.RequestData<Vector2>(Constant.enumMessage.GET_MOUSE_POSITION)[0];
                int[] relativePos = GetRelativeMousePos(MousePos);
                if (relativePos != null && !IsBoardOccupied(relativePos[0],relativePos[1]))
                {
                    //m_GameCommand.SendData(Constant.enumMessage.SWITCH_SPRITES_DRAW_STATUS, new string[] {m_OObjTable[relativePos[0],relativePos[1]]});
                    SetBoardOccupied(relativePos[0], relativePos[1]);
                    SwitchPlayer();
                }
            }
            else if (m_CurrPlayer == 1)
            {
                int genx = m_Random.Next(3), geny = m_Random.Next(3);
                if (!IsBoardOccupied(genx, geny))
                {
                    //m_GameCommand.SendData(Constant.enumMessage.SWITCH_SPRITES_DRAW_STATUS, new string[] { m_XObjTable[genx, geny] });
                    SetBoardOccupied(genx, geny);
                    SwitchPlayer();
                }
            }
            CheckGameWin();
            if (m_CurrState != 1)
            {
                ClearBoard();
            }
        }

        private void ClearBoard()
        {
            foreach (string currX in m_XObjTable)
            {
                SpriteComponent SC = m_GameCommand.RequestData<IComponent[]>(Constant.enumMessage.GET_GAMEOBJECTCOMPONENTS_BY_ID, new object[] { currX, new Constant.enumComponent[]{Constant.enumComponent.SPRITE} }).OfType<SpriteComponent>().FirstOrDefault();
                if (SC.m_isDrawn)
                {
                    SC.SwitchDrawState();
                }
            }
            foreach (string currO in m_OObjTable)
            {
                SpriteComponent SC = m_GameCommand.RequestData<IComponent[]>(Constant.enumMessage.GET_GAMEOBJECTCOMPONENTS_BY_ID, new object[] { currO, new Constant.enumComponent[] { Constant.enumComponent.SPRITE } }).OfType<SpriteComponent>().FirstOrDefault();
                if (SC.m_isDrawn)
                {
                    SC.SwitchDrawState();
                }
            }
        }

        private void CheckGameWin()
        {
            bool hasdrawn = true;
            foreach (bool currsquare in m_BoardIsOccupuied)
            {
                hasdrawn = (hasdrawn && currsquare);
            }
            if (!hasdrawn)
            {
                for (int player = 0; player < 2; player++)
                {

                    bool[] rowCompleted = new bool[] { true, true, true }, colCompleted = new bool[] { true, true, true }, diagsCompleted = new bool[] { true, true };
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            string currentID = player == 0 ? m_OObjTable[i, j] : m_XObjTable[i, j];
                            SpriteComponent SC = m_GameCommand.RequestData<IComponent>(Constant.enumMessage.GET_GAMEOBJECTCOMPONENTS, new object[] { currentID, new Constant.enumComponent[] {Constant.enumComponent.SPRITE} }).OfType<SpriteComponent>().FirstOrDefault();
                            if (!SC.m_isDrawn)
                            {
                                rowCompleted[i] = false;
                                colCompleted[j] = false;
                                if (i == j)
                                {
                                    diagsCompleted[0] = false;
                                    if (i == 1)
                                    {
                                        diagsCompleted[1] = false;
                                    }
                                }
                                else if (i == (2 - j))
                                {
                                    diagsCompleted[1] = false;
                                }
                            }
                            if (!rowCompleted[0] && !rowCompleted[1] && !rowCompleted[2] && !colCompleted[0] && !colCompleted[1] && !colCompleted[2] && !diagsCompleted[0] && !diagsCompleted[1])
                            {
                                m_CurrState = 1;
                                break;
                            }
                            else
                            {
                                if (player == 0)
                                {
                                    m_CurrState = 2;
                                }
                                else
                                {
                                    m_CurrState = 3;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                m_CurrState = 4;
            }
        }

        private int[] GetRelativeMousePos(Vector2 MousePos)
        {
            int[] relativePos = null;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Vector3 currPos = m_BoardGrid[i,j];
                    if((MousePos.x > currPos.x && MousePos.x <currPos.x + 662) && (MousePos.y > currPos.y && MousePos.y < currPos.y + 514))
                    {
                        relativePos = new int[]{i,j};
                    }
                }
            }
            return relativePos;
        }

        private void Close()
        {
            m_GameCommand.SendData(Constant.enumMessage.CLOSING);
        }

        private void AIWins()
        {
            if (m_StateJustChanged)
            {
                m_GameCommand.SendData(Constant.enumMessage.SWITCH_SPRITES_DRAW_STATUS, new string[] { "LoseScreen" });
                m_StateJustChanged = false;
            }
            StartUpdate();
        }

        private void PlayerWins()
        {
            if (m_StateJustChanged)
            {
                m_GameCommand.SendData(Constant.enumMessage.SWITCH_SPRITES_DRAW_STATUS, new string[] { "WinScreen" });
                m_StateJustChanged = false;
            }
            StartUpdate();
        }

        //HACK: This is linked into the Win, Lose and Draw Screens too, each should have their own handling of input!
        private void StartUpdate()
        {
            //TODO: add a switch draw state on the "StartScreen" Object on leaving the state
            string[] pressedKeys = m_GameCommand.RequestData<string>(Constant.enumMessage.GET_PRESSED_KEYS);
            foreach (string key in pressedKeys)
            {
                Constant.enumKey currKey = (Constant.enumKey)Enum.Parse(typeof(Constant.enumKey), key);
                switch (currKey)
                {
                    case Constant.enumKey.Return:
                        m_CurrState = 1;
                        break;
                    case Constant.enumKey.Escape:
                        m_CurrState = 5;
                        break;
                }
            }
        }

        private void SetBoardOccupied(int x, int y)
        {
            m_BoardIsOccupuied[x, y] = true;
        }

        private bool IsBoardOccupied(int x, int y)
        {
            return m_BoardIsOccupuied[x, y];
        }

        private void SwitchPlayer()
        {
            m_CurrPlayer = 1 - m_CurrPlayer;
        }

        public void Draw()
        {
            //throw new NotImplementedException();
        }
    }
}

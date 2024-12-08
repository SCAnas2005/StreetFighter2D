using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;

//using GLOBALS;
using UTILITY;
using GameClass;
using GameObjects;
using FigthScene;
using Manager;
using Importer;
using Audio;

namespace GameProject;
public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private PlayerImporter _PlayerImporter;
    private BackgroundImporter _BackgroundImporter;
    private FontImporter _FontImporter;

    private FightManager _FightManager;
    private PlayerSelectorManager _PlayerSelectorManager;
    private BackgroundSelectorManager _BackgroundSelectorManager;

    private Chrono _GameChrono;

    private ButtonControler MainControls;
    private ButtonControler SecondControls;

    private Music _MenuMusic;

    // Player coords
    private Vector2 _player1_position;
    private Vector2 _player2_position;
    private int _Player1Selected;
    private int _Player2Selected;
    private int _BackgroundSelected;

    private GameState STATE;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = 1080;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();

        _player1_position = new Vector2(200, 300);
        _player2_position = new Vector2(600, 300);

        _PlayerImporter = new PlayerImporter();
        _BackgroundImporter = new BackgroundImporter();
        _FontImporter = new FontImporter();

        _MenuMusic = new Music();


        _FightManager = new FightManager(GraphicsDevice);

        MainControls = new ButtonControler(Keys.D, Keys.Q, Keys.Z, Keys.S, Keys.Space, Keys.J, Keys.K);
        SecondControls = new ButtonControler(Keys.Right, Keys.Left, Keys.Up, Keys.Down, Keys.Up, Keys.NumPad1, Keys.NumPad2);

        _GameChrono = new Chrono();

        ChangeScreen(GameState.PRESENTATION);
        _GameChrono.Start();
    }

    protected override void Initialize()
    {
        _PlayerImporter.Init(Content, GraphicsDevice);
        _BackgroundImporter.Init(Content, GraphicsDevice);
        _FontImporter.Init(GraphicsDevice, Content);
        _FightManager.Init(Content);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        _PlayerImporter.LoadAll();
        _BackgroundImporter.LoadAll();
        _FontImporter.LoadAll();

        _MenuMusic.Load(Content, "Musics/Menu_Music");

        _FightManager.Load();
    }

    protected override void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (STATE)
            {
                case GameState.TEST:

                break;

                case GameState.PRESENTATION:
                    _MenuMusic.Play();

                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        ChangeScreen(GameState.PLAYER_SELECTION_INIT);
                    }
                break;

                case GameState.PLAYER_SELECTION_INIT:
                    _PlayerSelectorManager = new PlayerSelectorManager(_graphics.GraphicsDevice, _PlayerImporter.GetIcons(), 
                    new Vector2(250, 300), 100, 120, 2);

                    ChangeScreen(GameState.PLAYER_SELECTION);
                break;  
                case GameState.PLAYER_SELECTION:
                    _MenuMusic.Play();
                    _PlayerSelectorManager.Update();
                    if (_PlayerSelectorManager.IsFinished)
                    {
                        
                        _Player1Selected = _PlayerSelectorManager.GetIndexPlayerSelected1();
                        _Player2Selected = _PlayerSelectorManager.GetIndexPlayerSelected2();

                        if (_GameChrono.Wait(2))
                        {
                            ChangeScreen(GameState.BACKGROUND_SELECTION_INIT);
                        }
                    }
                break;

                case GameState.BACKGROUND_SELECTION_INIT:
                    _BackgroundSelectorManager = new BackgroundSelectorManager(GraphicsDevice, 
                    _BackgroundImporter.GetIcons(), new Vector2(350, 100), 400, 100);

                    ChangeScreen(GameState.BACKGROUND_SELECTION);
                break;

                case GameState.BACKGROUND_SELECTION:
                    _MenuMusic.Play();
                    _BackgroundSelectorManager.Update();
                    if (_BackgroundSelectorManager.Selected)
                    {
                        if (_GameChrono.Wait(2))
                        {
                            _BackgroundSelected = _BackgroundSelectorManager.Get();
                            ChangeScreen(GameState.FIGHT_INIT);
                        }
                    }
                break;

                case GameState.FIGHT_INIT:
                    _FightManager.NewFight(_PlayerImporter.Get(_Player1Selected), 
                    _PlayerImporter.Get(_Player2Selected), 
                    _BackgroundImporter.Get((Backgrounds)_BackgroundSelected));

                    _FightManager.InitPlayers(_player1_position, _player2_position, 
                    MainControls, 
                    SecondControls);

                    ChangeScreen(GameState.FIGHT);
                break;

                case GameState.FIGHT:
                    _FightManager.Update((float)gameTime.ElapsedGameTime.Milliseconds);

                    if (_FightManager.IsFinished)
                    {
                        if (_GameChrono.Wait(5))
                        {
                            ChangeScreen(GameState.PLAYER_SELECTION_INIT);
                        }
                    }
                break;
            }
                
            _GameChrono.Update((float)gameTime.ElapsedGameTime.Milliseconds);
            base.Update(gameTime);
        }
    }

    protected override void Draw(GameTime gameTime)
    { 
        GraphicsDevice.Clear(Color.White);
        _spriteBatch.Begin();

        switch (STATE)
        {
            case GameState.TEST:

            break;

            case GameState.PRESENTATION:
                GraphicsDevice.Clear(Color.Black);

                Text.WriteScreen(_spriteBatch, _FontImporter.Get(Fonts.MainFont), "Press Space to Start", 
                new Vector2(300, 720/2), Color.White);
            break;

            case GameState.PLAYER_SELECTION:
                _PlayerSelectorManager.Draw(_spriteBatch);
            break;

            case GameState.BACKGROUND_SELECTION:
                _BackgroundSelectorManager.Draw(_spriteBatch);
            break;

            case GameState.FIGHT:
                _FightManager.Draw(_spriteBatch);
            break;

            default:
            break;
        }

        Text.WriteScreen(_spriteBatch, _FontImporter.Get(Fonts.MainFont), "STATE : "+Enum.GetName(typeof(GameState), STATE), 0.75f,
        new Vector2(0,0), Color.Blue);

        Text.WriteScreen(_spriteBatch, _FontImporter.Get(Fonts.MainFont), "TIME : "+_GameChrono.Seconds, 
        new Vector2(0+400, 0), Color.Blue);

        
        _spriteBatch.End();
        base.Draw(gameTime); 
    }

    protected override void UnloadContent()
    {
        Content.Unload();
        base.UnloadContent();
    }

    public void ChangeScreen(GameState ScreenValue)
    {
        STATE = ScreenValue;
    }
}

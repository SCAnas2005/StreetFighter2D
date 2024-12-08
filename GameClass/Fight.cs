
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

using GameClass;
using GameObjects;
using UTILITY;
using SpriteClass;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;
using Audio;

namespace FigthScene
{   
    public enum FightSounds
    {
        KO
    }

    public enum FightMusics
    {

    }

    // Class Fight qui represente un combat 1vs1
    public class FightManager
    {
        private GraphicsDevice _GraphicsDevice;
        private ContentManager _Content;

        private Player _player1;
        private Player _player2;

        private HealthBar _healthbar1;
        private HealthBar _healthbar2;

        private EnergyBar _energybar1;
        private EnergyBar _energybar2;


        private Background _FightBackground;

        private AnimationSprite KO;
        private Sound[] Sounds;

        private TimeSprite _TimeSprite;
        private Timer _FightTimer;
        private Chrono _FightChrono;

        private Camera camera;

        private SpriteFont _font;

        private FightScreenStates STATE;
        public bool IsFinished {get; set;}

        public FightManager(GraphicsDevice graphics)
        {
            _GraphicsDevice = graphics;
            _FightChrono = new Chrono();
            _FightTimer = new Timer(50*1000);

            Sounds = new Sound[Enum.GetNames(typeof(FightSounds)).Length];
            for (int i = 0; i < Sounds.Length; i++)
            {
                Sounds[i] = new Sound();
            }

            IsFinished = false;
        }

        ~FightManager()
        {
            for (int i = 0; i < Sounds.Length; i++)
            {
                Sounds[i].Dispose();
            } 
        }

        public void Init(ContentManager content)
        {
            _Content = content;
        }

        public void Load()
        {
            _font = _Content.Load<SpriteFont>("Fonts/MainFont");
            KO = new AnimationSprite(_Content.Load<Texture2D>("Others/KO"), 23, 132, 75, 75, 5, 5, 0, 0);
            Sounds[(int)FightSounds.KO].Load(_Content, "Sounds/KO");
        }

        public void NewFight(Player player1, Player player2, Background background)
        {
            _player1 = new Player(player1); 
            _player2 = new Player(player2);

            _healthbar1 = new HealthBar(new Rectangle(50,50, 400,40), _player1.Health, _player1.Health, 'r');
            _energybar1 = new EnergyBar(new Rectangle(50, 91, 400, 20), _player1.Energy, _player1.Energy, 'r');
            _TimeSprite = new TimeSprite(new Vector2(450+40, 35), Color.Black);
            _healthbar2 = new HealthBar(new Rectangle(600,50, 400,40), _player2.Health, _player2.Health, 'l');
            _energybar2 = new EnergyBar(new Rectangle(600, 91, 400, 20), _player2.Energy, _player2.Energy, 'l');

            _FightBackground = background;
            

            _healthbar1.Init(_GraphicsDevice);
            _energybar1.Init(_GraphicsDevice);
            _healthbar2.Init(_GraphicsDevice);
            _energybar2.Init(_GraphicsDevice);

            camera = new Camera();
            camera.Init(_GraphicsDevice);
            camera.New(_FightBackground);

            STATE = FightScreenStates.BLACKSCREEN;

            _TimeSprite.Init(_font);
            _FightChrono.Restart();
            _FightTimer.Reset();

            IsFinished = false;
        }

        public bool GameStopped()
        {
            if (_FightTimer.IsFinished())
            {
                return true;
            }
            if (_player1.IsDead())
            {
                return true;
            }
            else if (_player2.IsDead())
            {
                return true;
            }
            return false;
        }

        public void InitPlayers(Vector2 p1_position, Vector2 p2_position, ButtonControler bc1, ButtonControler bc2)
        {
            _player1.Controls = bc1;
            _player2.Controls = bc2;

            _player1.Init(p1_position);
            _player2.Init(p2_position);

            if (object.ReferenceEquals((object)_player1._sprite, (object)_player2._sprite))
            {
                Debug.Write("Sprite are the same");
            }
            else
            {
                Debug.Write("Sprite are not the same");
            }
            
        }

        public void PlaySound(FightSounds sound)
        {
            Sounds[(int)sound].Play();
        }

        public void Update(float milliseconds)
        {
            switch(STATE)
            {
                case FightScreenStates.BLACKSCREEN:

                    if (_FightChrono.Wait(2))
                    {
                        STATE = FightScreenStates.FIGHTING;
                        _FightTimer.Start();
                    }
                break;

                case FightScreenStates.FIGHTING:
                    if (Keyboard.GetState().IsKeyDown(Keys.G))
                        _player1.SetDamage(2);
                    if (Keyboard.GetState().IsKeyDown(Keys.H))
                        _player2.SetDamage(2);

                    if (_player1.IsHurt(_player2))
                    {
                        _player1.SetDamage(_player2.GetAttackDamage());
                    }
                    if (_player2.IsHurt(_player1))
                    {
                        _player2.SetDamage(_player1.GetAttackDamage());
                    }

                    if (_player1.Position.X < 50)
                    {
                        camera.Move(-3, -2);
                    }
                    else if (_player1.Position.X > 800)
                    {
                        camera.Move(3, 2);
                    }

                    if (GameStopped())
                    {
                        PlaySound(FightSounds.KO);
                        STATE = FightScreenStates.KO;
                    }

                    _player1.Update(milliseconds);
                    _player2.Update(milliseconds);

                    _healthbar1.Update(_player1.Health);
                    _energybar1.Update(_player1.Energy);
                    _healthbar2.Update(_player2.Health);
                    _energybar2.Update(_player2.Energy);

                    _FightTimer.Update(milliseconds);
                    _TimeSprite.Update(_FightTimer.Seconds);
                    _FightBackground.Update(milliseconds);
                break;

                case FightScreenStates.KO:
                    KO.Update(milliseconds);

                    if (KO.IsFinished)
                    {
                        KO.Stop = true;
                        if (_FightChrono.Wait(3))
                        {
                            IsFinished = true;
                        }
                    }
                break;

                default:
                break;
            }

            _FightChrono.Update(milliseconds);
        }

        public void DrawBackground(SpriteBatch _spriteBatch)
        {
            _FightBackground.Draw(_spriteBatch);
        }

        public void DrawTime(SpriteBatch _spriteBatch)
        {
            Shapes.FillRectangle(_GraphicsDevice, _spriteBatch, new Rectangle(450, 40, 150, 60), Color.Beige);
            Shapes.DrawRectangle(_GraphicsDevice, _spriteBatch, new Rectangle(450, 40, 150, 60), Color.Black, 2);
            _TimeSprite.Draw(_spriteBatch, 2f);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            switch(STATE)
            {
                case FightScreenStates.BLACKSCREEN:
                    _GraphicsDevice.Clear(Color.Black);
                break;

                case FightScreenStates.KO:
                case FightScreenStates.FIGHTING:
                    DrawBackground(_spriteBatch);
                    //camera.Draw(_spriteBatch);

                    _player1.Draw(_spriteBatch);
                    _player2.Draw(_spriteBatch);

                    Text.WriteScreen(_spriteBatch, _font, _player1.Name, 
                    new Vector2(_healthbar1.Position.X+10, _healthbar1.Position.Y-40), Color.Red);
                    _healthbar1.Draw(_spriteBatch);
                    _energybar1.Draw(_spriteBatch);
                    Text.WriteScreen(_spriteBatch, _font, _player2.Name, 
                    new Vector2(_healthbar2.Position.X+10, _healthbar2.Position.Y-40), Color.Blue);
                    _healthbar2.Draw(_spriteBatch);
                    _energybar2.Draw(_spriteBatch);

                    DrawTime(_spriteBatch);

                    if (STATE == FightScreenStates.KO)
                    {
                        KO.Draw(_spriteBatch, new Rectangle(300, 150, 700, 400));
                    }
                break;

                default:
                break;
            }
        }
    }
}
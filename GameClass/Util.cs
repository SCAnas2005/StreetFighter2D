
using System.Collections;
using System.Diagnostics;
using GameProject;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UTILITY
{
    public static class Shapes
    {
        public static void FillRectangle(GraphicsDevice GraphicsDevice, SpriteBatch _spriteBatch, Rectangle rectangle, Color color)
        {
            Texture2D _texture = new Texture2D(GraphicsDevice, 1,1);
            _texture.SetData(new[] { Color.White });
            _spriteBatch.Draw(_texture, rectangle, color);
        }

        public static void FillRectangle(SpriteBatch _spriteBatch, Texture2D texture, Rectangle rectangle, Color color)
        {
            if (_spriteBatch == null)
                Debug.Write("SpriteBatch is null");
            if (texture == null)
                Debug.Write("Texture is null");
            _spriteBatch.Draw(texture, rectangle, color);
        }

        public static void DrawRectangle(GraphicsDevice GraphicsDevice, SpriteBatch _spriteBatch, Rectangle rectangle, Color color)
        {
            Texture2D _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new[] { Color.White });
            
            _spriteBatch.Draw(_texture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, 1), color);
            _spriteBatch.Draw(_texture, new Rectangle(rectangle.X, rectangle.Y, 1, rectangle.Height), color);
            _spriteBatch.Draw(_texture, new Rectangle(rectangle.X, rectangle.Y+rectangle.Height, rectangle.Width, 1), color);
            _spriteBatch.Draw(_texture, new Rectangle(rectangle.X+rectangle.Width, rectangle.Y, 1, rectangle.Height), color);
        }

        public static void DrawRectangle(GraphicsDevice GraphicsDevice, SpriteBatch _spriteBatch, Rectangle rectangle, Color color, int heigth)
        {
            Texture2D _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new[] { Color.White });
            
            _spriteBatch.Draw(_texture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, heigth), color);
            _spriteBatch.Draw(_texture, new Rectangle(rectangle.X, rectangle.Y, heigth, rectangle.Height), color);
            _spriteBatch.Draw(_texture, new Rectangle(rectangle.X, rectangle.Y+rectangle.Height, rectangle.Width, heigth), color);
            _spriteBatch.Draw(_texture, new Rectangle(rectangle.X+rectangle.Width, rectangle.Y, heigth, rectangle.Height), color);
        }

        public static void DrawRectangle(SpriteBatch _spriteBatch, Texture2D texture, Rectangle rectangle, Color color)
        {
            _spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, 1), color);
            _spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y, 1, rectangle.Height), color);
            _spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y+rectangle.Height, rectangle.Width, 1), color);
            _spriteBatch.Draw(texture, new Rectangle(rectangle.X+rectangle.Width, rectangle.Y, 1, rectangle.Height), color);
        }
    }

    public struct ButtonControler
    {
        public Keys Right;
        public Keys Left;
        public Keys Up;
        public Keys Down;
        public Keys Jump;
        public Keys Attack1;
        public Keys Attack2;

        public ButtonControler(Keys right, Keys left, Keys up, Keys down, Keys jump, Keys a1, Keys a2)
        {
            Right = right; Left = left; Up = up; Down = down;
            Jump = jump; Attack1 = a1; Attack2 = a2;
        }
    }

    // Animation State R = right, L = left
    public enum AnimationState
    {
        STANDR,
        STANDL,
        RUNR,
        RUNL,
        JUMPR,
        JUMPL,
        ATTACK1R,
        ATTACK1L,
        ATTACK2R,
        ATTACK2L,
        TAKEHITR,
        TAKEHITL,
        DEATHR,
        DEATHL
    }

    public enum TimeMode
    {
        CHRONO,
        TIMER
    }

    public class Timer
    {
        public float Milliseconds;
        public int Seconds {get{return (int)Milliseconds/1000;}}

        public float _lastMillisecondsBreak;

        private bool _IsStart;

        public Timer(float milliseconds)
        {
            _lastMillisecondsBreak = milliseconds;
            Milliseconds = milliseconds;

            _IsStart = false;
        }
    
        public bool IsFinished()
        {
            return (Milliseconds <= 0);
        } 

        public void Start() {_IsStart = true;}
        public void Stop() {_IsStart = false;}
        public void Reset(){ Stop(); Milliseconds = _lastMillisecondsBreak; }
        public void Restart() { Reset(); Start();}

        public void SetTime(float NewMilli)
        {
            _lastMillisecondsBreak = NewMilli;
        }

        public void Update(float milli)
        {
            if (_IsStart && !IsFinished())
            {
                Milliseconds -= milli;
            }
        }   
    }

    public class Chrono
    {
        public float Milliseconds {get; set;}
        public int Seconds {get {return (int)Milliseconds/1000;}}
        private int _TimeToWait;
        public bool IsWaiting;

        public bool IsStart {get; set;}

        public Chrono()
        {
            Milliseconds = 0;
            IsStart = false;
            IsWaiting = false;
        }

        public bool Wait(int seconds)
        {
            if (!IsWaiting)
            {
                _TimeToWait = Seconds;
                IsWaiting = true;
            }
            else
            {
                if (_TimeToWait+seconds == Seconds)
                {
                    IsWaiting = false;
                    return true;
                }
            }
            return false;
        }

        public void Start()
        {
            IsStart = true;
        }

        public void Stop()
        {
            IsStart = false;
        }

        public void Reset()
        {
            Stop();
            Milliseconds = 0;
        }

        public void Restart()
        {
            Reset();
            Start();
        }

        public void Update(float milliseconds)
        {
            if (IsStart)
            {
                Milliseconds += milliseconds;
            }
        }
    }

    public enum FightScreenStates
    {
        BLACKSCREEN,
        PLAYERSPRESENTATION,
        READYTOSTART,
        FIGHTING,
        KO,
        TIMEOVER,
        ENDING
    }

    public static class Text
    {
        public static void WriteScreen(SpriteBatch _spriteBatch, SpriteFont font, string text, Vector2 pos, Color color)
        {
            _spriteBatch.DrawString(font, text, pos, color);
        }

        public static void WriteScreen(SpriteBatch _spriteBatch, SpriteFont font, string text, float scale, Vector2 pos, Color color)
        {
            _spriteBatch.DrawString(font, text, pos, color, 0f, Vector2.Zero, scale, SpriteEffects.None, default);
        } 

    }


    public enum GameState
    {
        TEST,
        PRESENTATION,
        PLAYER_SELECTION_INIT,
        PLAYER_SELECTION,
        BACKGROUND_SELECTION_INIT,
        BACKGROUND_SELECTION,
        FIGHT_INIT,
        FIGHT
    }
}
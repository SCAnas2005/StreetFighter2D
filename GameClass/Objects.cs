
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UTILITY;
//using GLOBALS;

namespace GameObjects
{
    public class HealthBar
    {
        private Texture2D _TextureHealthBar;
        
        private Rectangle _HealthBar;
        private Rectangle _HealthBarStatus;


        private int _health;
        private int _MaxHealth;

        private int _percentage;
        private Color _color;

        public Vector2 Position {get { return new Vector2(_HealthBar.X, _HealthBar.Y);}}
        public char Direction {get; set;}

        public HealthBar(Rectangle healthbar, int health, int MaxHealth, char direction)
        {
            _HealthBar = healthbar;
            _HealthBarStatus = _HealthBar;
            _health = health;
            _MaxHealth = MaxHealth;
            _color = Color.Green;

            Direction = direction;
        }

        ~HealthBar()
        {
            _TextureHealthBar.Dispose();
        }

        public void Init(GraphicsDevice GraphicsDevice)
        {
            _TextureHealthBar = new Texture2D(GraphicsDevice, 1, 1);
            _TextureHealthBar.SetData(new[] {Color.White});
        }

        public void Update(int health)
        {
            if (IsGoodHealth(health))
            {
                _health = health;
                _percentage = (_health*100)/_MaxHealth;
                _HealthBarStatus.Width = (_HealthBar.Width*_percentage)/100;
                if (Direction == 'l')
                    _HealthBarStatus.X = _HealthBar.X +(_HealthBar.Width - _HealthBarStatus.Width);
                this.SetColor(_percentage);
            }
            
        }

        public bool IsGoodHealth(int health) => (health >= 0 && health <= _MaxHealth);

        public void SetColor(int percentage)
        {
            if (percentage >= 60)
                _color = Color.Green;
            else if (percentage >= 40)
                _color = Color.Yellow;
            else if (percentage >= 15)
                _color = Color.Orange;
            else
                _color = Color.Red;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            Shapes.FillRectangle(_spriteBatch, _TextureHealthBar, _HealthBar, Color.LightGray);
            Shapes.FillRectangle(_spriteBatch, _TextureHealthBar, _HealthBarStatus, _color);
            Shapes.DrawRectangle(_spriteBatch, _TextureHealthBar, _HealthBar, Color.Blue);
        }
    }


    public class EnergyBar
    {
        private Texture2D _TextureEnergyBar;
        
        private Rectangle _EnergyBar;
        private Rectangle _EnergyBarStatus;


        private int _Energy;
        private int _MaxEnergy;

        private int _percentage;
        private Color _color;

        public Vector2 Position {get { return new Vector2(_EnergyBar.X, _EnergyBar.Y);}}
        public char Direction {get; set;}

        public EnergyBar(Rectangle Energybar, int Energy, int MaxEnergy, char direction)
        {
            _EnergyBar = Energybar;
            _EnergyBarStatus = _EnergyBar;
            _Energy = Energy;
            _MaxEnergy = MaxEnergy;
            _color = Color.Blue;

            Direction = direction;
        }

        ~EnergyBar()
        {
            _TextureEnergyBar.Dispose();
        }

        public void Init(GraphicsDevice GraphicsDevice)
        {
            _TextureEnergyBar = new Texture2D(GraphicsDevice, 1, 1);
            _TextureEnergyBar.SetData(new[] {Color.White});
        }

        public void Update(int Energy)
        {
            if (IsGoodEnergy(Energy))
            {
                _Energy = Energy;
                _percentage = (_Energy*100)/_MaxEnergy;
                _EnergyBarStatus.Width = (_EnergyBar.Width*_percentage)/100;
                if (Direction == 'l')
                    _EnergyBarStatus.X = _EnergyBar.X +(_EnergyBar.Width - _EnergyBarStatus.Width);
            }
            
        }

        public bool IsGoodEnergy(int Energy) => (Energy >= 0 && Energy <= _MaxEnergy);

        public void Draw(SpriteBatch _spriteBatch)
        {
            Shapes.FillRectangle(_spriteBatch, _TextureEnergyBar, _EnergyBar, Color.LightBlue);
            Shapes.FillRectangle(_spriteBatch, _TextureEnergyBar, _EnergyBarStatus, _color);
            Shapes.DrawRectangle(_spriteBatch, _TextureEnergyBar, _EnergyBar, Color.Black);
        }
    }



    public class TimeSprite
    {
        private Vector2 _position;
        private SpriteFont _font;
        private Color _color;
        private int Seconds {get; set;}


        public TimeSprite(Vector2 pos, Color color)
        {
            _position = pos;

            _color = color;

            Seconds = 0;
        }

        public void Init(SpriteFont font)
        {
            _font = font;
        }

        public void Update(int seconds)
        {
            Seconds = seconds;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(_font, ""+Seconds, _position, _color);
        }

        public void Draw(SpriteBatch _spriteBatch, float scale)
        {
            _spriteBatch.DrawString(_font, ""+Seconds, _position, _color, 0f, Vector2.Zero, scale, 
            SpriteEffects.None, default);
        }
    }

    

}
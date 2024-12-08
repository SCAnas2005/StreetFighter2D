
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteClass;

namespace GameClass
{
    public class Background
    {
        private AnimationSprite _animation;
        public Texture2D Icon;
        private Vector2 _position;
        private int _width, _height;
        
        public int FrameWidth {get; set;}
        public int FrameHeight {get; set;}
        
        public Background(int width, int height)
        {
            _position = Vector2.Zero;
            _width = width;
            _height = height;
        }

        public void Load(Texture2D texture, int SpriteFrame, 
        int width, int height, int FrameTime, int LineNumber, int ColumnNumber, int LineIndex, int ColumnIndex)
        {
            FrameWidth = width;
            FrameHeight = height;
            _animation = new AnimationSprite(texture, SpriteFrame, width, height, FrameTime, LineNumber, ColumnNumber,
            LineIndex, ColumnIndex);
        }

        public void Update(float milliseconds)
        {
            _animation.Update(milliseconds);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _animation.Draw(_spriteBatch, new Rectangle((int)_position.X, (int)_position.Y, _width, _height));
        }

        public void Draw(SpriteBatch _spriteBatch, Rectangle part)
        {
            _animation.Draw(_spriteBatch, new Rectangle((int)_position.X, (int)_position.Y, _width, _height), part);
        }
    }
}
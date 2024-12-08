
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameClass
{
    public class Camera
    {
        private GraphicsDevice GraphicsDevice;

        private Background _background;

        private Rectangle CurrentBackgroundScreen;

        private int _WindowWidth;
        private int _WindowHeight;

        public void Init(GraphicsDevice gd)
        {
            GraphicsDevice = gd;
            _WindowWidth = GraphicsDevice.Viewport.Width;
            _WindowHeight = GraphicsDevice.Viewport.Height;
        }

        public void New(Background bg)
        {
            _background = bg;
            CurrentBackgroundScreen = new Rectangle(200, 0, _background.FrameWidth, _background.FrameHeight);
        }

        public void Move(int vx, int vy)
        {
            if (CurrentBackgroundScreen.X + vx >= 0 
            && CurrentBackgroundScreen.X + vx < 1080
            && CurrentBackgroundScreen.Width > 0
            && CurrentBackgroundScreen.Width - vx < 1080)
            {
                CurrentBackgroundScreen.X += vx;
                //CurrentBackgroundScreen.Width += -vx;
            }

            if (CurrentBackgroundScreen.Y + vy >= 0 
            && CurrentBackgroundScreen.Y + vy < 720
            && CurrentBackgroundScreen.Height > 0
            && CurrentBackgroundScreen.Height - vy < 720)
            {
                //CurrentBackgroundScreen.Y += vy;
                //CurrentBackgroundScreen.Height += -vy;
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _background.Draw(_spriteBatch, CurrentBackgroundScreen);
        }   
    }
}
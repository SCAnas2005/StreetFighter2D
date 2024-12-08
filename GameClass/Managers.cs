
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;

using UTILITY;
using GameClass;
using Importer;
using System.Security.Cryptography.X509Certificates;

namespace Manager
{
    public class PlayerSelectorManager
    {
        GraphicsDevice _graphics;

        private Texture2D[] _icons;
        private Rectangle[] _rectangles;

        private Rectangle _PlayerShownPos1;
        private Rectangle _PlayerShownPos2;

        private int _SelectorIndex1;
        private int _SelectorIndex2;

        private Vector2 _StartPos;

        private int _width;
        private int _heigth;
        private int _PlayerNumber;

        private Color _color1;
        private Color _color2;

        private bool[] _IsPressed1;
        private bool[] _IsPressed2;
        private bool _IsSelected1;
        private bool _IsSelected2;

        public bool IsFinished{get; set;}


        public PlayerSelectorManager(GraphicsDevice gd, Texture2D[] icons, Vector2 pos, int CaseWidth, int CaseHeigth, 
        int PlayerNumber)
        {
            _PlayerNumber = PlayerNumber;

            _graphics = gd;
            _icons = icons;
            _rectangles = new Rectangle[_icons.Length];

            _StartPos = pos;

            _width = CaseWidth;
            _heigth = CaseHeigth;

            _PlayerShownPos1 = new Rectangle((int)_StartPos.X+10, (int)_StartPos.Y-_heigth*2-35, _width*2, _heigth*2);
            _PlayerShownPos2 = new Rectangle((int)_StartPos.X+_width*(_icons.Length-1)-10, (int)_StartPos.Y-_heigth*2-35, _width*2, _heigth*2);

            _SelectorIndex1 = 0;
            _SelectorIndex2 = 1;

            _color1 = Color.LightBlue;
            _color2 = Color.Pink;

            _IsPressed1 = new bool[]{false, false};
            _IsPressed2 = new bool[]{false, false};
            _IsSelected1 = false;
            _IsSelected2 = false;

            IsFinished = false;
            InitIcons();
        }
        
        public void InitIcons()
        {
            for (int i = 0; i < _rectangles.Length; i++)
            {
                _rectangles[i] = new Rectangle((i*_width)+(int)_StartPos.X, (int)_StartPos.Y, _width, _heigth);
            }
        }

        public void MoveSelector(ref int Selector, int direction)
        {
            Selector += direction;
            if (Selector < 0)
                Selector = _icons.Length-1;
            else if (Selector >= _icons.Length)
                Selector = 0;
        }

        public int GetIndexPlayerSelected1()
        {
            if (_IsSelected1)
                return _SelectorIndex1;
            return -1;
        }
        public int GetIndexPlayerSelected2()
        {
            if (_IsSelected2)
                return _SelectorIndex2;
            return -1;
        }

        public void Update()
        {
            if (!_IsSelected1)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    if (!_IsPressed1[0])
                    {
                        MoveSelector(ref _SelectorIndex1, 1);
                        _IsPressed1[0] = true;
                    }
                    
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                    if (!_IsPressed1[1])
                    {
                        MoveSelector(ref _SelectorIndex1, -1);
                        _IsPressed1[1] = true;
                    }
                    
                }
                if (Keyboard.GetState().IsKeyUp(Keys.D))
                    _IsPressed1[0] = false;
                if (Keyboard.GetState().IsKeyUp(Keys.Q))
                    _IsPressed1[1] = false;

                if (Keyboard.GetState().IsKeyDown(Keys.J))
                {
                    _color1 = Color.Blue;
                    _IsSelected1 = true;
                }
            }
            if (_PlayerNumber == 2)
            {
                if (!_IsSelected2)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        if (!_IsPressed2[0])
                        {
                            MoveSelector(ref _SelectorIndex2, 1);
                            _IsPressed2[0] = true;
                        }
                        
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        if (!_IsPressed2[1])
                        {
                            MoveSelector(ref _SelectorIndex2, -1);
                            _IsPressed2[1] = true;
                        }
                        
                    }
                    if (Keyboard.GetState().IsKeyUp(Keys.Right))
                        _IsPressed2[0] = false;
                    if (Keyboard.GetState().IsKeyUp(Keys.Left))
                        _IsPressed2[1] = false;

                    if (Keyboard.GetState().IsKeyDown(Keys.NumPad1))
                    {
                        _color2 = Color.Red;
                        _IsSelected2 = true;
                    }
                }
            }

            if (_IsSelected1)
            {
                if (_PlayerNumber == 2)
                {
                    if (_IsSelected2)
                        IsFinished = true;
                }
                else
                    IsFinished = true;
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _graphics.Clear(Color.Black);
            
            Shapes.FillRectangle(_graphics, _spriteBatch, 
            new Rectangle((int)_StartPos.X-30, (int)_StartPos.Y-200, _width*_icons.Length+60, _heigth*2+200+60), Color.White);

            for (int i = 0; i < _icons.Length; i++)
            {
                _spriteBatch.Draw(_icons[i], _rectangles[i], Color.White);
            }  
            _spriteBatch.Draw(_icons[_SelectorIndex1], _PlayerShownPos1, Color.White);
            Shapes.DrawRectangle(_graphics, _spriteBatch, _rectangles[_SelectorIndex1], _color1, 3);
            if (_PlayerNumber == 2)
            {
                _spriteBatch.Draw(_icons[_SelectorIndex2], _PlayerShownPos2, Color.White);
                Shapes.DrawRectangle(_graphics, _spriteBatch, _rectangles[_SelectorIndex2], _color2, 3);
            }
        }
    }

    public class BackgroundSelectorManager
    {
        GraphicsDevice GraphicsDevice;

        Texture2D[] Icons;
        private int Selector;

        private int CaseWidth, CaseHeight;
        Vector2 _StartPosition;

        private Rectangle[] CasePositions;

        private bool[] IsPressed;

        public bool Selected {get; set;}


        public BackgroundSelectorManager(GraphicsDevice gd, Texture2D[] icons, Vector2 pos, int width, int height)
        {
            GraphicsDevice = gd;
            Icons = icons;

            CaseWidth = width;
            CaseHeight = height;

            _StartPosition = pos;

            Selector = 0;
            CasePositions = new Rectangle[Icons.Length];

            for (int i = 0; i < CasePositions.Length; i++)
            {
                CasePositions[i] = new Rectangle((int)_StartPosition.X, (int)_StartPosition.Y*(i+1), CaseWidth, CaseHeight); 
            }

            IsPressed = new bool[2];
            IsPressed[0] = false;
            IsPressed[1] = false;
            Selected = false;
        }

        public void MoveSelector(ref int selector, int vector)
        {
            selector += vector;
            if (selector < 0)
                selector = Icons.Length-1;
            else if (selector >= Icons.Length)
                selector = 0;
        }       

        public int Get()
        {
            if (Selected)
                return Selector;
            return -1;
        }

        public void Update()
        {
            if (!Selected)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    if (!IsPressed[0])
                    {
                        MoveSelector(ref Selector, 1);
                        IsPressed[0] = true;
                    }
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Z))
                {
                    if (!IsPressed[1])
                    {
                        MoveSelector(ref Selector, -1);
                        IsPressed[1] = true;
                    }
                }

                if (Keyboard.GetState().IsKeyUp(Keys.S))
                {
                    if (IsPressed[0])
                    {
                        IsPressed[0] = false;
                    }
                }
                else if (Keyboard.GetState().IsKeyUp(Keys.Z))
                {
                    if (IsPressed[1])
                    {
                        IsPressed[1] = false;
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.J))
                {
                    Selected = true;
                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            GraphicsDevice.Clear(Color.Black);

            Shapes.FillRectangle(GraphicsDevice, _spriteBatch, new Rectangle((int)_StartPosition.X-50, 
            (int)_StartPosition.Y-20, CaseWidth+100, CaseHeight*Icons.Length+CaseHeight+40), Color.White);
            for (int i = 0; i < Icons.Length; i++)
            {
                _spriteBatch.Draw(Icons[i], CasePositions[i], Color.White);
                Shapes.DrawRectangle(GraphicsDevice, _spriteBatch, CasePositions[i], Color.Black, 2);
            }
            Shapes.DrawRectangle(GraphicsDevice, _spriteBatch, CasePositions[Selector], Color.Blue, 2);
        }
    }
}
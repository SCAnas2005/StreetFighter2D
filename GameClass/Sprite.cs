using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UTILITY;

namespace SpriteClass
{
    public class AnimationSprite
    {   
        Texture2D _SpriteSheet;

        private Rectangle[] _SourceRectangles;
        private int _SpriteFrame;
        private int _CurrentFrameIndex;
        
        private int _SpriteWidth;
        private int _SpriteHeight;

        private int _lines;
        private int _columns;

        private float _timer;
        private int _FrameTime;

        private bool _isFinished;
        public bool IsFinished { get {return _isFinished; }} // Is the animation finished ?

        public int CurrentFrame {get {return _CurrentFrameIndex; }}
        public bool Stop {get; set;}

        // Contruct the animation with Sprite texture
        public AnimationSprite(Texture2D texture, int SpriteFrame, int width, int height, 
        int FrameTime, int LineNumber, int ColumnNumber, int LineIndex, int ColumnIndex)
        {
            _SpriteSheet = texture;
            _SpriteFrame = SpriteFrame;

            _SpriteWidth = width;
            _SpriteHeight = height;

            _lines = LineNumber;
            _columns = ColumnNumber;

            _CurrentFrameIndex = 0;
            _SourceRectangles = new Rectangle[_SpriteFrame];
            
            int i = 0, l = LineIndex, c = ColumnIndex;
            while (i < _SourceRectangles.Length && l <= _lines)
            {   
                if (c >= _columns)
                {
                    c = 0;
                    l++;
                }
                _SourceRectangles[i] = new Rectangle(c*_SpriteWidth, l*_SpriteHeight, _SpriteWidth, _SpriteHeight);
                c++;
                i++;
            }

            _timer = 0;
            _FrameTime = FrameTime;

            _isFinished = false;
            Stop = false;
        }

        // Replay Animation to begining
        public void replay()
        {
            _CurrentFrameIndex = 0;
            _isFinished = false;
        }

        // Update Animation Frame
        public void Update(float milli)
        {
            if (!_isFinished && !Stop)
            {
                if (_timer > _FrameTime) 
                {                   
                    _timer = 0;
                    _CurrentFrameIndex++;
                    if (_CurrentFrameIndex == _SpriteFrame-1)
                    {
                        _isFinished = true;
                    }
                }
                else {
                    _timer += milli;
                }
            }
            else if (_isFinished)
            {
                if (Stop == false)
                    this.replay();
            }
            
        }

        // Draw Animation with Source Rectangle
        public void Draw(SpriteBatch _spriteBatch, Rectangle position)
        {
            _spriteBatch.Draw(this._SpriteSheet, position, _SourceRectangles[_CurrentFrameIndex], Color.White);
        }   

        public void Draw(SpriteBatch _spriteBatch, Rectangle position, Rectangle source)
        {
            Rectangle src = new Rectangle(_SourceRectangles[_CurrentFrameIndex].X+source.X, 
            _SourceRectangles[_CurrentFrameIndex].Y+source.Y, source.Width, source.Height);
            _spriteBatch.Draw(this._SpriteSheet, position, src, Color.White);
        }
    }

    public class Sprite
    {
        public GraphicsDevice _graphics;

        private Vector2 _pos;

        private Rectangle source;
        private Rectangle _HurtboxOffset;
        private Rectangle _hurtbox;
        private int _width, _height;

        private AnimationSprite[] Animations;
        private int CurrentAnimationIndex;

        private char _direction;
        private int _isAttacking;
        private bool _isDead;

        public Vector2 Position {get{return _pos;} set{_pos = value;}}
        public int Attack {get{return _isAttacking;} set{_isAttacking = value;}}
        public bool StopAnimation {get; set;}
        public bool IsDead { get { return _isDead;} set{ _isDead = value; }}
        public char Direction { get {return _direction;} set { _direction = value; }}
        public Rectangle Hurtbox {get { return _hurtbox; } set { _hurtbox = value; }}
        public Rectangle Source {get{ return source; } set { source = value;}}
        public AnimationSprite CurrentAnimation {get { return Animations[CurrentAnimationIndex]; }}

        public Sprite(GraphicsDevice graphics, Vector2 pos, int width, int height)
        {
            _graphics = graphics;
            
            _pos = pos;
            _width = width; _height = height;
            source = new Rectangle((int)_pos.X, (int)_pos.Y, _width, _height);

            CurrentAnimationIndex = (int)AnimationState.STANDR;
            Animations = new AnimationSprite[Enum.GetNames(typeof(AnimationState)).Length];


            _direction = 'r';
            _isAttacking = 0;
            _isDead = false;
            StopAnimation = false;
        }

        public Sprite(Sprite sprite)
        {
            _graphics = sprite._graphics;

            _pos = sprite.Position;
            _width = sprite._width; _height = sprite._height;
            source = new Rectangle((int)_pos.X, (int)_pos.Y, _width, _height);

            Animations = sprite.Animations;
            CurrentAnimationIndex = (int)AnimationState.STANDR;

            _direction = 'r';
            _isAttacking = 0;
            _isDead = false;
            StopAnimation = false;

            _HurtboxOffset = sprite._HurtboxOffset;
            Hurtbox = sprite.Hurtbox;
        }

        public Sprite New()
        {
            Sprite sprite = (Sprite)this.Clone();
            return sprite;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void Init(Vector2 pos)
        {
            _pos = pos;
            source = new Rectangle((int)_pos.X, (int)_pos.Y, _width, _height);

            CurrentAnimationIndex = (int)AnimationState.STANDR;
            _direction = 'r';
            _isAttacking = 0;
            _isDead = false;
            StopAnimation = false;
        }

        public void SetHurbox(Rectangle HurboxOffset)
        {
            _HurtboxOffset = HurboxOffset;
            if (_direction == 'l')
                _hurtbox = new Rectangle(source.X+_HurtboxOffset.X, source.Y+_HurtboxOffset.Y, _HurtboxOffset.Width, _HurtboxOffset.Height);
            else
                _hurtbox = new Rectangle((source.X+source.Width)-(_HurtboxOffset.X+_HurtboxOffset.Width), source.Y+_HurtboxOffset.Y, _HurtboxOffset.Width, _HurtboxOffset.Height);
        }

        // Create Animation from Sprite Sheet Texture
        public void CreateAnimation(int index, Texture2D texture, int SpriteFrame, int width, int height, int FrameTime,
        int LineNumber, int ColumnNumber, int LineIndex, int ColumnIndex)
        {
            Animations[index] = new AnimationSprite(texture, SpriteFrame, width, height, FrameTime,
            LineNumber, ColumnNumber, LineIndex, ColumnIndex);
        }

        public void CreateAnimationStandRight(Texture2D texture, int SpriteFrame, int width, int height, int FrameTime,
        int LineNumber, int ColumnNumber, int LineIndex, int ColumnIndex)
        {
            this.CreateAnimation((int)AnimationState.STANDR, texture, SpriteFrame, width, height, FrameTime,
            LineNumber, ColumnNumber, LineIndex, ColumnIndex);
        }
        public void CreateAnimationStandLeft(Texture2D texture, int SpriteFrame, int width, int height, int FrameTime,
        int LineNumber, int ColumnNumber, int LineIndex, int ColumnIndex)
        {
            this.CreateAnimation((int)AnimationState.STANDL, texture, SpriteFrame, width, height, FrameTime,
            LineNumber, ColumnNumber, LineIndex, ColumnIndex);
        }   

        public void CreateAnimationJumpRight(Texture2D texture, int SpriteFrame, int width, int height, int FrameTime,
        int LineNumber, int ColumnNumber, int LineIndex, int ColumnIndex)
        {
            this.CreateAnimation((int)AnimationState.JUMPR, texture, SpriteFrame, width, height, FrameTime,
            LineNumber, ColumnNumber, LineIndex, ColumnIndex);
        }  
        public void CreateAnimationJumpLeft(Texture2D texture, int SpriteFrame, int width, int height, int FrameTime,
        int LineNumber, int ColumnNumber, int LineIndex, int ColumnIndex)
        {
            this.CreateAnimation((int)AnimationState.JUMPL, texture, SpriteFrame, width, height, FrameTime,
            LineNumber, ColumnNumber, LineIndex, ColumnIndex);
        } 

        public void CreateAnimationRunRight(Texture2D texture, int SpriteFrame, int width, int height, int FrameTime,
        int LineNumber, int ColumnNumber, int LineIndex, int ColumnIndex)
        {
            this.CreateAnimation((int)AnimationState.RUNR, texture, SpriteFrame, width, height, FrameTime,
            LineNumber, ColumnNumber, LineIndex, ColumnIndex);
        }
        public void CreateAnimationRunLeft(Texture2D texture, int SpriteFrame, int width, int height, int FrameTime,
        int LineNumber, int ColumnNumber, int LineIndex, int ColumnIndex)
        {
            this.CreateAnimation((int)AnimationState.RUNL, texture, SpriteFrame, width, height, FrameTime,
            LineNumber, ColumnNumber, LineIndex, ColumnIndex);
        }

        public void CreateAnimationAttack1Left(Texture2D texture, int SpriteFrame, int width, int height, int FrameTime,
        int LineNumber, int ColumnNumber, int LineIndex, int ColumnIndex)
        {
            this.CreateAnimation((int)AnimationState.ATTACK1L, texture, SpriteFrame, width, height, FrameTime,
            LineNumber, ColumnNumber, LineIndex, ColumnIndex);
        } 
        public void CreateAnimationAttack1Right(Texture2D texture, int SpriteFrame, int width, int height, int FrameTime,
        int LineNumber, int ColumnNumber, int LineIndex, int ColumnIndex)
        {
            this.CreateAnimation((int)AnimationState.ATTACK1R, texture, SpriteFrame, width, height, FrameTime,
            LineNumber, ColumnNumber, LineIndex, ColumnIndex);
        } 

        public void CreateAnimationAttack2Right(Texture2D texture, int SpriteFrame, int width, int height, int FrameTime,
        int LineNumber, int ColumnNumber, int LineIndex, int ColumnIndex)
        {
            this.CreateAnimation((int)AnimationState.ATTACK2R, texture, SpriteFrame, width, height, FrameTime,
            LineNumber, ColumnNumber, LineIndex, ColumnIndex);
        } 
        public void CreateAnimationAttack2Left(Texture2D texture, int SpriteFrame, int width, int height, int FrameTime,
        int LineNumber, int ColumnNumber, int LineIndex, int ColumnIndex)
        {
            this.CreateAnimation((int)AnimationState.ATTACK2L, texture, SpriteFrame, width, height, FrameTime,
            LineNumber, ColumnNumber, LineIndex, ColumnIndex);
        } 

        public void CreateAnimationTakeHitRight(Texture2D texture, int SpriteFrame, int width, int height, int FrameTime,
        int LineNumber, int ColumnNumber, int LineIndex, int ColumnIndex)
        {
            this.CreateAnimation((int)AnimationState.TAKEHITR, texture, SpriteFrame, width, height, FrameTime,
            LineNumber, ColumnNumber, LineIndex, ColumnIndex);
        } 
        public void CreateAnimationTakeHitLeft(Texture2D texture, int SpriteFrame, int width, int height, int FrameTime,
        int LineNumber, int ColumnNumber, int LineIndex, int ColumnIndex)
        {
            this.CreateAnimation((int)AnimationState.TAKEHITL, texture, SpriteFrame, width, height, FrameTime,
            LineNumber, ColumnNumber, LineIndex, ColumnIndex);
        } 

        public void CreateAnimationDeathRight(Texture2D texture, int SpriteFrame, int width, int height, int FrameTime,
        int LineNumber, int ColumnNumber, int LineIndex, int ColumnIndex)
        {
            this.CreateAnimation((int)AnimationState.DEATHR, texture, SpriteFrame, width, height, FrameTime,
            LineNumber, ColumnNumber, LineIndex, ColumnIndex);
        } 
        public void CreateAnimationDeathLeft(Texture2D texture, int SpriteFrame, int width, int height, int FrameTime,
        int LineNumber, int ColumnNumber, int LineIndex, int ColumnIndex)
        {
            this.CreateAnimation((int)AnimationState.DEATHL, texture, SpriteFrame, width, height, FrameTime,
            LineNumber, ColumnNumber, LineIndex, ColumnIndex);
        } 

        // Change the animation with Animation State
        public void ChangeAnimation(AnimationState newAnimation)
        {
            if (CurrentAnimationIndex != (int)newAnimation)
            {
                if ( newAnimation == AnimationState.DEATHL || newAnimation == AnimationState.DEATHR
                || CurrentAnimationIndex == (int)AnimationState.STANDR 
                || CurrentAnimationIndex == (int)AnimationState.STANDL
                || CurrentAnimationIndex == (int)AnimationState.RUNR 
                || CurrentAnimationIndex == (int)AnimationState.RUNL
                || Animations[CurrentAnimationIndex].IsFinished)
                {
                    CurrentAnimationIndex = (int)newAnimation;
                    Animations[CurrentAnimationIndex].replay();
                }
            }
        }

        public void Jump()
        {
            if (Direction == 'r')
                ChangeAnimation(AnimationState.JUMPR);
            else 
                ChangeAnimation(AnimationState.JUMPL);
        }

        public void TakeHit()
        {
            if (Direction == 'r')
                this.ChangeAnimation(AnimationState.TAKEHITR);
            else 
                this.ChangeAnimation(AnimationState.TAKEHITL);
        }

        public void Death()
        {
            if (_isDead == false)
            {
                _isDead = true;
                if (_direction == 'r')
                    this.ChangeAnimation(AnimationState.DEATHR);
                else
                    this.ChangeAnimation(AnimationState.DEATHL);
            }
        }

        public void UpdatePosition(Vector2 NewPosition)
        {
            _pos = NewPosition;
        }

        public void UpdateSource()
        {
            source.X = (int)_pos.X;
            source.Y = (int)_pos.Y;
        }

        public void UpdateHurtbox()
        {
            _hurtbox.X = source.X+_HurtboxOffset.X;
            _hurtbox.Y = source.Y+_HurtboxOffset.Y;
        }

        public void MoveRight()
        {
            Direction = 'r';
            ChangeAnimation(AnimationState.RUNR);
        }

        public void MoveLeft()
        {
            Direction = 'l';
            ChangeAnimation(AnimationState.RUNL);
        }

        public void Stand()
        {
            if (Direction == 'r')
                ChangeAnimation(AnimationState.STANDR);
            else
                ChangeAnimation(AnimationState.STANDL);
        }

        public void Attack1()
        {
            Attack = 1;                   // attack 1
            if (Direction == 'r')
                ChangeAnimation(AnimationState.ATTACK1R);
            else 
                ChangeAnimation(AnimationState.ATTACK1L);
        }

        public void Attack2()
        {
            Attack = 2;                   // attack 2
            if (Direction == 'r')
                ChangeAnimation(AnimationState.ATTACK2R);
            else 
                ChangeAnimation(AnimationState.ATTACK2L);
        }

        // Update sprite method
        public void Update(float milliseconds)
        {
            if (!StopAnimation)
            {
                CurrentAnimation.Update(milliseconds); // Update Current Animation 

                UpdateSource();         // Update position rectangle
                UpdateHurtbox();        // Update Hurtbox rectangle
            }
            
        }


        // Draw Sprite method
        public void Draw(SpriteBatch _spriteBatch)
        {
            Shapes.DrawRectangle(_graphics, _spriteBatch, source, Color.Blue);
            Shapes.DrawRectangle(_graphics, _spriteBatch, Hurtbox, Color.Red);
            Animations[CurrentAnimationIndex].Draw(_spriteBatch, source);
        }
    }



    public class Attack
    {       
        private AnimationSprite _animation;
        private Rectangle _hitbox;
        private Rectangle _source;
        private Rectangle _HitboxOffset;

        public int Damage {get; set;}
        public bool IsActive {get; set;}

        private int[] _FramesActive;
        
        public Attack(int damage)
        {
            Damage = damage;
            IsActive = false;
        }
        public Attack(Attack attack)
        {
            Damage = attack.Damage;
            IsActive = false;

            _animation = attack._animation;
            _HitboxOffset = attack._HitboxOffset;
            _hitbox = attack._hitbox;

            _FramesActive = attack._FramesActive;
        }

        public void SetHitbox(Rectangle source, Rectangle HitboxOffset, char direction, int[] FramesActive)
        {
            _FramesActive = FramesActive;
            _source = source;
            _HitboxOffset = HitboxOffset; 
            if (direction == 'l')
                _hitbox = new Rectangle(source.X+HitboxOffset.X, source.Y+HitboxOffset.Y, HitboxOffset.Width, HitboxOffset.Height);
            else    
                _hitbox = new Rectangle((source.X+source.Width)-(HitboxOffset.X+HitboxOffset.Width), 
                source.Y+HitboxOffset.Y, HitboxOffset.Width, HitboxOffset.Height);
        }

        public void Update(Rectangle source, char direction)
        {
            _source = source;
            if (direction == 'l')
                _hitbox.X = source.X+_HitboxOffset.X;
            else
                _hitbox.X = (source.X+source.Width)-(_HitboxOffset.X+_HitboxOffset.Width);
            _hitbox.Y = source.Y += _HitboxOffset.Y;
        }
    
        public bool IsGoodFrame(int frame)
        {
            for (int i = 0; i < _FramesActive.Length; i++)
            {
                if (_FramesActive[i] == frame)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Collision(Rectangle rectangle)
        {
            return _hitbox.Intersects(rectangle);
        }

        public void Draw(GraphicsDevice GraphicsDevice, SpriteBatch _spriteBatch, Color color)
        {
            if (IsActive)
            {
                if (_animation != null)
                    _animation.Draw(_spriteBatch, _hitbox);
                Shapes.DrawRectangle(GraphicsDevice, _spriteBatch, _hitbox, color);
            }
        }

    }
}
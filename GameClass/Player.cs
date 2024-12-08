
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


using UTILITY;
using SpriteClass;
using System.Diagnostics;
//using GLOBALS;

namespace GameClass
{
    public class Player 
    {
        private GraphicsDevice _graphics;
        public Sprite _sprite;
        public Attack[] _attacks;
        public int _AttackIndex;

        private int _health;
        private int _MaxHealth;

        private int _energy;
        private int _MaxEnergy;

        private float _speed;

        private Vector2 _velocity;
        private Vector2 _position;
        private Vector2 _gravity = new Vector2(0, 2500f);

        private Rectangle _box;
        int _width,  _heigth;

        private bool _IsDie;
        private bool _isTouched;

        ButtonControler _controls;
        private float _jump;

        public string Name {get; set;}
        public bool IsDie {get {return _IsDie;} set { _IsDie = value; }}
        public int Health {get {return _health;} set{ _health = value;}}
        public int Energy {get {return _energy;} set{_energy = value;}}
        public int MaxHealth {get {return _MaxHealth;}}
        public bool IsTouched { get {return _isTouched;} set{_isTouched = value;}}
        public bool HitboxActive {get; set;}
        public Attack CurrentAttack {get {return _attacks[_AttackIndex]; }} 
        public Texture2D Icon {get; set;}
        public float Speed {get {return _speed;} set {_speed = value;}}

        public Vector2 Position {get {return _position;} set{_position = value;}}
        public ButtonControler Controls {get {return _controls; } set{_controls = value;}}


        public bool Jumped { get; set; }

        public Player(string name, Vector2 pos, int width, int height, int health, int MaxHealth,
        GraphicsDevice gd, ButtonControler controls)
        {
            _graphics = gd;
            Name = name;
            _position = pos;
            
            _width = width; _heigth = height;
            _sprite = new Sprite(gd, pos, _width, _heigth);

            _attacks = new Attack[2];
            _AttackIndex = 0;

            _health = health;
            _MaxHealth = MaxHealth;

            _MaxEnergy = 200;
            _energy = _MaxEnergy;

            _IsDie = false;
            _isTouched = false;

            _controls = controls;
            _speed = 5f;
            _jump = -50f;
            Jumped = false;
        }

        public Player(Player player)
        {
            _graphics = player._graphics;
            Name = player.Name;
            _position = player._position;
            
            _width = player._width; _heigth = player._heigth;
            _sprite = new Sprite(player._sprite);

            _attacks = new Attack[2];
            for (int i = 0; i < _attacks.Length; i++){
                _attacks[i] = new Attack(player._attacks[i]);
            }
            _AttackIndex = 0;

            _health = player._health;
            _MaxHealth = player._MaxHealth;

            _MaxEnergy = 200;
            _energy = _MaxEnergy;

            _IsDie = false;
            _isTouched = false;

            _controls = player.Controls;
            _speed = 5f;
            _jump = -50f;
            Jumped = false;
        }

        public void Init(Vector2 position)
        {
            _position = position;

            _box = new Rectangle((int)_position.X, (int)_position.X, _width, _heigth);
            _health = _MaxHealth;
            _energy = _MaxEnergy;

            _IsDie = false;
            _isTouched = false;
            Jumped = false;
            
            _sprite.Init(_position);
        }

        public void SetAttack(int AttackNumber, int damage, Rectangle HitboxOffset, int[] FramesActive)
        {
            _attacks[AttackNumber] = new Attack(damage);
            _attacks[AttackNumber].SetHitbox(_sprite.Source, HitboxOffset, _sprite.Direction, FramesActive);
        }

        public bool IsDead() => (_health <= 0);

        public void SetDamage(int damage)
        {
            if (damage >= 0)
            {
                _health -= damage;
                if (_health < 0)
                    _health = 0;
            }
        }

        public int GetAttackDamage()
        {
            if (_sprite.Attack != 0)
                return CurrentAttack.Damage;
            return 0;
        }

        public bool IsHurt(Player player)
        {
            if (!IsDead())
            {
                if (player._sprite.Attack != 0)
                {
                    if (player.CurrentAttack.IsActive)
                    {
                        if (player.CurrentAttack.Collision(_sprite.Hurtbox) && _isTouched == false)
                        {
                            player.CurrentAttack.IsActive = false;
                            _isTouched = true;
                            return true;
                        }
                    }
                }
                
            }
            
            return false;
        }

        public void ApplyGravity(float milliseconds)
        {
            if (Jumped)
            {
                _velocity.Y += _gravity.Y * (milliseconds/1000);
            }
            if (_sprite.Hurtbox.Y+_sprite.Hurtbox.Width >= 500 && Jumped)
            {
                Jumped = false;
                _speed -= 4f;
                _velocity.Y = 0f;
            }

            _position.Y += _velocity.Y * (milliseconds/1000);
        } 

        public bool IsOutMap(Vector2 v)
        {
            return (v.X < 0 || v.X > 1080 || v.Y < 0 || v.Y > 720); 
        }

        public void Move(Vector2 v)
        {
            if (_sprite.Attack == 0 || true)
            {
                if (!IsOutMap(new Vector2(_sprite.Hurtbox.X+v.X, _sprite.Hurtbox.Y+v.Y)) && 
                !IsOutMap(new Vector2(_sprite.Hurtbox.X+_sprite.Hurtbox.Width+v.X, _sprite.Hurtbox.Y+_sprite.Hurtbox.Height+v.Y)))
                {
                    _position += v;
                }
            }
        }
        public void MoveRight() { this.Move(new Vector2(_speed, 0)); }
        public void MoveLeft() { this.Move(new Vector2(-_speed, 0)); }
        public void Jump() { 
            if (!Jumped)
            {
                this.Move(new Vector2(0, _jump)); 
                _speed += 4f;
                Jumped = true;
            }
        }


        public void Update(float milliseconds)
        {
            _sprite.UpdatePosition(_position);
            if (IsDead())
            {
                _sprite.Death();
            }

            if (!_sprite.StopAnimation)
            {
                if (_sprite.IsDead == false)
                {
                    if (_sprite.Attack != 0) 
                    {
                        _AttackIndex = _sprite.Attack-1;

                        if(CurrentAttack.IsGoodFrame(_sprite.CurrentAnimation.CurrentFrame))
                            CurrentAttack.IsActive = true;
                        else 
                            CurrentAttack.IsActive = false;
                        
                        if (_sprite.CurrentAnimation.IsFinished) {
                            _sprite.Attack = 0;
                            CurrentAttack.IsActive = false;
                        }
                    }
                    if (Keyboard.GetState().IsKeyDown(_controls.Right)) {
                        this.MoveRight();
                        _sprite.MoveRight();
                    }
                    else if (Keyboard.GetState().IsKeyDown(_controls.Left)) {
                        this.MoveLeft();
                        _sprite.MoveLeft();
                    }
                    else{
                        _sprite.Stand();
                    }

                    if (Keyboard.GetState().IsKeyDown(_controls.Jump)) {
                        this.Jump();
                        _sprite.Jump();
                    }
                    else if (Keyboard.GetState().IsKeyDown(_controls.Attack1)) {
                        if (_sprite.Attack == 0)
                            _energy -= 10;
                        _sprite.Attack1();
                    }
                    else if(Keyboard.GetState().IsKeyDown(_controls.Attack2)) {
                        if (_sprite.Attack == 0)
                            _energy -= 10;
                        _sprite.Attack2(); 
                    }

                    if (_isTouched)
                    {
                        _sprite.TakeHit();
                        _isTouched = false;
                    }
                }
                //ApplyGravity(milliseconds); // Apply gravity method 
                ApplyGravity(milliseconds);
                _sprite.Update(milliseconds);
                _attacks[_AttackIndex].Update(_sprite.Source, _sprite.Direction);

                // Stop Sprite Update method
                if (_sprite.IsDead && _sprite.CurrentAnimation.IsFinished)
                {
                    _sprite.StopAnimation = true;
                    return;
                }
            }
        }
        
        public void Draw(SpriteBatch _spriteBatch)
        {
            _sprite.Draw(_spriteBatch);
            if (this.CurrentAttack.IsActive)
            {
                CurrentAttack.Draw(_graphics, _spriteBatch, Color.Red);
            }
        }
    }
}
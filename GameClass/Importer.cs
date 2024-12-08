
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using GameClass;
using SpriteClass;
using System.Diagnostics;

namespace Importer
{
    public enum Players
    {
        WIZARD,
        EVIL_WIZARD, 
        FIRE_WIZARD,
        WARRIOR,
        MARTIAL_HERO,
        WATER_MASTER
    }

    public class PlayerImporter
    {
        private ContentManager Content;
        private GraphicsDevice _GraphicsDevice;
        private Player[] _players;


        private int _WWidth = 183, _WHeight = 150,  _WHealth = 500, _WDamage1 = 10, _WDamage2 = 10;
        private int _EWWidth = 300, _EWHeight = 300, _EWHealth = 300, _EWDamage1 = 10, _EWDamage2 = 10;
        private int _FWWidth = 300, _FWHeight = 300, _FWHealth = 300, _FWDamage1 = 2, _FWDamage2 = 2;
        private int _WARWidth = 300, _WARHeight = 300, _WARHealth = 300, _WARDamage1 = 10, _WARDamage2 = 10;

        private int _MHWidth = 300, _MHHeight = 300,  _MHHealth = 500, _MHDamage1 = 10, _MHDamage2 = 10;
        private int _WMWidth = 400, _WMHeight = 200,  _WMHealth = 500, _WMDamage1 = 10, _WMDamage2 = 10;

        public PlayerImporter()
        {
            _players = new Player[Enum.GetNames(typeof(Players)).Length];
        }

        public void Init(ContentManager content, GraphicsDevice gd)
        {
            Content = content;
            _GraphicsDevice = gd;

            this.CreateAllPlayers();
            this.InitAll();
        }

        public void LoadAll()
        {
            this.LoadAllSprite();
        }

        public Player Get(int PlayerSelected)
        {
            return _players[PlayerSelected];
        }

        /*
        public void CreateRoot()
        {
            _players[(int)Players.ROOT] = new Player("Root", default, 
            _RWidth, _RHeight, _RHealth, _RHealth, _GraphicsDevice, default);
        }
        */
        public void CreateWizard()
        {
            _players[(int)Players.WIZARD] = new Player("Wizard", default, 
            _WWidth, _WHeight, _WHealth, _WHealth, _GraphicsDevice, default);
        }

        public void CreateEvilWizard()
        {
            _players[(int)Players.EVIL_WIZARD] = new Player("Evil Wizard", default, 
            _EWWidth,_EWHeight, _EWHealth,_EWHealth, _GraphicsDevice, default);
        }

        public void CreateFireWizard()
        {
            _players[(int)Players.FIRE_WIZARD] = new Player("Fire Wizard", default, 
            _FWWidth,_FWHeight, _FWHealth,_FWHealth, _GraphicsDevice, default);
        }

        public void CreateWarrior()
        {
            _players[(int)Players.WARRIOR] = new Player("Warrior", default, 
            _WARWidth,_WARHeight, _WARHealth,_WARHealth, _GraphicsDevice, default);
        }

        public void CreateMartialHero()
        {
            _players[(int)Players.MARTIAL_HERO] = new Player("Martial Hero", default, 
            _MHWidth,_MHHeight, _MHHealth,_MHHealth, _GraphicsDevice, default);
        }

        public void CreateWaterMaster()
        {
            _players[(int)Players.WATER_MASTER] = new Player("Water Master", default, 
            _WMWidth,_WMHeight, _WMHealth,_WMHealth, _GraphicsDevice, default);
        }

        public void CreateAllPlayers()
        {
            //CreateRoot();
            CreateWizard();
            CreateEvilWizard();
            CreateFireWizard();
            CreateWarrior();
            CreateMartialHero();
            CreateWaterMaster();
        }
    

        /*
        public void InitRoot() 
        {
            _players[(int)Players.ROOT]._sprite.SetHurbox(new Rectangle((29*_RWidth)/100,(15*_RHeight)/100,(48*_RWidth)/100,(75*_RHeight)/100));

            _players[(int)Players.ROOT].SetAttack(0, _RDamage1, new Rectangle((1*_RWidth)/100,(41*_RHeight)/100,(22*_RWidth)/100,(16*_RHeight)/100)
            , new int[]{5});
            _players[(int)Players.ROOT].SetAttack(1, _RDamage2, new Rectangle((1*_RWidth)/100,(38*_RHeight)/100,(23*_RWidth)/100,(20*_RHeight)/100)
            , new int[]{2});
        }
        */
        public void InitEvilWizard()
        {
            _players[(int)Players.EVIL_WIZARD]._sprite.SetHurbox(new Rectangle((42*_EWWidth)/100,(44*_EWHeight)/100,(17*_EWWidth)/100,(24*_EWHeight)/100));

            _players[(int)Players.EVIL_WIZARD].SetAttack(0, _EWDamage1, new Rectangle((4*_EWWidth)/100,(15*_EWHeight)/100,(34*_EWWidth)/100,(52*_EWHeight)/100)
            , new int[]{4,5});
            _players[(int)Players.EVIL_WIZARD].SetAttack(1, _EWDamage2, new Rectangle((4*_EWWidth)/100,(15*_EWHeight)/100,(34*_EWWidth)/100,(52*_EWHeight)/100)
            , new int[]{1,5});
        }

        public void InitWizard()
        {
            _players[(int)Players.WIZARD]._sprite.SetHurbox(new Rectangle((39*_WWidth)/100,(30*_WHeight)/100,(26*_WWidth)/100,(45*_WHeight)/100));
        
            _players[(int)Players.WIZARD].SetAttack(0, _WDamage1, new Rectangle((0*_WWidth)/100,(0*_WHeight)/100,(87*_WWidth)/100,(75*_WHeight)/100) 
            , new int[]{4});

            _players[(int)Players.WIZARD].SetAttack(1, _WDamage2, new Rectangle((4*_WWidth)/100,(15*_WHeight)/100,(34*_WWidth)/100,(52*_WHeight)/100),
            new int[]{4,5});
        }
    
        public void InitFireWizard()
        {
            _players[(int)Players.FIRE_WIZARD]._sprite.SetHurbox(new Rectangle((39*_FWWidth)/100,(34*_FWHeight)/100,(23*_FWWidth)/100,(34*_FWHeight)/100));
        
            _players[(int)Players.FIRE_WIZARD].SetAttack(0, _FWDamage1, new Rectangle((5*_FWWidth)/100,(33*_FWHeight)/100,(41*_FWWidth)/100,(31*_FWHeight)/100)
            , new int[]{0,1,2,3,4,5,6,7});
            _players[(int)Players.FIRE_WIZARD].SetAttack(1, _FWDamage2, new Rectangle((5*_FWWidth)/100,(33*_FWHeight)/100,(41*_FWWidth)/100,(31*_FWHeight)/100)
            , new int[]{0,1,2,3,4,5,6,7});
        }

        public void InitWarrior()
        {
            _players[(int)Players.WARRIOR]._sprite.SetHurbox(new Rectangle((39*_WARWidth)/100,(33*_WARHeight)/100,(23*_WARWidth)/100,(33*_WARHeight)/100));
            // Calculer et mettre les hurtboxs
            _players[(int)Players.WARRIOR].SetAttack(0, _WARDamage1, new Rectangle((6*_WARWidth)/100,(26*_WARHeight)/100,(84*_WARWidth)/100,(34*_WARHeight)/100), 
            new int[]{3});

            _players[(int)Players.WARRIOR].SetAttack(1, _WARDamage2, new Rectangle((0*_WARWidth)/100,(4*_WARHeight)/100,(79*_WARWidth)/100,(60*_WARHeight)/100), 
            new int[]{3});
        }
    
        public void InitMartialHero()
        {
            _players[(int)Players.MARTIAL_HERO]._sprite.SetHurbox(new Rectangle((37*_MHWidth)/100,(33*_MHHeight)/100,(29*_MHWidth)/100,(42*_MHHeight)/100));
            // Calculer et mettre les hurtboxs
            _players[(int)Players.MARTIAL_HERO].SetAttack(0, _MHDamage1, new Rectangle((0*_MHWidth)/100,(0*_MHHeight)/100,(62*_MHWidth)/100,(67*_MHHeight)/100), 
            new int[]{3});

            _players[(int)Players.MARTIAL_HERO].SetAttack(1, _MHDamage2, new Rectangle((0*_MHWidth)/100,(6*_MHHeight)/100,(50*_MHWidth)/100,(48*_MHHeight)/100), 
            new int[]{3});
        }

        public void InitWaterMaster()
        {
            _players[(int)Players.WATER_MASTER]._sprite.SetHurbox(new Rectangle((45*_WMWidth)/100,(69*_WMHeight)/100,(10*_WMWidth)/100,(33*_WMHeight)/100));
            // Calculer et mettre les hurtboxs
            _players[(int)Players.WATER_MASTER].SetAttack(0, _WMDamage1, new Rectangle((20*_WMWidth)/100,(66*_WMHeight)/100,(22*_WMWidth)/100,(31*_WMHeight)/100), 
            new int[]{2,3,8,15,16,17});

            _players[(int)Players.WATER_MASTER].SetAttack(1, _WMDamage2, new Rectangle((16*_WMWidth)/100,(56*_WMHeight)/100,(30*_WMWidth)/100,(46*_WMHeight)/100), 
            new int[]{13,14,15,16,17});
        }

        public void InitAll()
        {
            //InitRoot();
            InitWizard();
            InitEvilWizard();
            InitFireWizard();
            InitWarrior();
            InitMartialHero();
            InitWaterMaster();
        }
    
    
        /*
        public void LoadRootSprite()
        {
            _players[(int)Players.ROOT]._sprite.CreateAnimationStandRight(Content.Load<Texture2D>("Player/Root/IdleR"), 10, 48,48,75, 1,10,0,0);
            _players[(int)Players.ROOT]._sprite.CreateAnimationStandLeft(Content.Load<Texture2D>("Player/Root/IdleL"), 10, 48,48,75, 1,10,0,0);
            _players[(int)Players.ROOT]._sprite.CreateAnimationJumpRight(Content.Load<Texture2D>("Player/Root/JumpR"), 3, 48,48,75, 1,3,0,0);
            _players[(int)Players.ROOT]._sprite.CreateAnimationJumpLeft(Content.Load<Texture2D>("Player/Root/JumpL"), 3, 48,48,75, 1,3,0,0);
            _players[(int)Players.ROOT]._sprite.CreateAnimationRunRight(Content.Load<Texture2D>("Player/Root/RunR"), 8, 48,48,75, 1,8,0,0);
            _players[(int)Players.ROOT]._sprite.CreateAnimationRunLeft(Content.Load<Texture2D>("Player/Root/RunL"), 8, 48,48,75, 1,8,0,0);
            _players[(int)Players.ROOT]._sprite.CreateAnimationAttack1Right(Content.Load<Texture2D>("Player/Root/Attack1R"), 8, 48,48,75, 1,8,0,0);
            _players[(int)Players.ROOT]._sprite.CreateAnimationAttack1Left(Content.Load<Texture2D>("Player/Root/Attack1L"), 8, 64,64,75, 1,8,0,0);
            _players[(int)Players.ROOT]._sprite.CreateAnimationAttack2Right(Content.Load<Texture2D>("Player/Root/Attack2R"), 7, 64,64,75, 1,7,0,0); 
            _players[(int)Players.ROOT]._sprite.CreateAnimationAttack2Left(Content.Load<Texture2D>("Player/Root/Attack2L"), 7, 64,64,75, 1,7,0,0); 
            _players[(int)Players.ROOT]._sprite.CreateAnimationTakeHitRight(Content.Load<Texture2D>("Player/Root/TakeHitR"), 4, 64,64, 100, 1,4,0,0); 
            _players[(int)Players.ROOT]._sprite.CreateAnimationTakeHitLeft(Content.Load<Texture2D>("Player/Root/TakeHitL"), 4, 48,48, 100, 1,4,0,0);
            _players[(int)Players.ROOT]._sprite.CreateAnimationDeathRight(Content.Load<Texture2D>("Player/Root/DeathR"), 10, 64,64, 150, 1,10,0,0); 
            _players[(int)Players.ROOT]._sprite.CreateAnimationDeathLeft(Content.Load<Texture2D>("Player/Root/DeathL"), 10, 64,64, 150, 1,10,0,0);
        
            _players[(int)Players.ROOT].Icon = Content.Load<Texture2D>("Player/Root/icon");
        }
        */
        public void LoadEvilWizardSprite()
        {
            _players[(int)Players.EVIL_WIZARD]._sprite.CreateAnimationStandRight(Content.Load<Texture2D>("Player/EVil_Wizard/IdleR"), 8, 250,250,75, 1,8,0,0);
            _players[(int)Players.EVIL_WIZARD]._sprite.CreateAnimationStandLeft(Content.Load<Texture2D>("Player/EVil_Wizard/IdleL"), 8, 250,250,75, 1,8,0,0);
            _players[(int)Players.EVIL_WIZARD]._sprite.CreateAnimationJumpRight(Content.Load<Texture2D>("Player/EVil_Wizard/JumpR"), 2, 250,250,75, 1,2, 0,0);
            _players[(int)Players.EVIL_WIZARD]._sprite.CreateAnimationJumpLeft(Content.Load<Texture2D>("Player/EVil_Wizard/JumpL"), 2, 250,250,75, 1,2, 0,0);
            _players[(int)Players.EVIL_WIZARD]._sprite.CreateAnimationRunRight(Content.Load<Texture2D>("Player/EVil_Wizard/RunR"), 8, 250,250,75, 1,8,0,0);
            _players[(int)Players.EVIL_WIZARD]._sprite.CreateAnimationRunLeft(Content.Load<Texture2D>("Player/EVil_Wizard/RunL"), 8, 250,250,75, 1,8,0,0);
            _players[(int)Players.EVIL_WIZARD]._sprite.CreateAnimationAttack1Right(Content.Load<Texture2D>("Player/EVil_Wizard/Attack1R"), 8, 250,250,75, 1,8,0,0);
            _players[(int)Players.EVIL_WIZARD]._sprite.CreateAnimationAttack1Left(Content.Load<Texture2D>("Player/EVil_Wizard/Attack1L"), 8, 250,250,75, 1,8,0,0);
            _players[(int)Players.EVIL_WIZARD]._sprite.CreateAnimationAttack2Right(Content.Load<Texture2D>("Player/EVil_Wizard/Attack2R"), 8, 250,250,75, 1,8,0,0); 
            _players[(int)Players.EVIL_WIZARD]._sprite.CreateAnimationAttack2Left(Content.Load<Texture2D>("Player/EVil_Wizard/Attack2L"), 8, 250,250,75, 1,8,0,0); 
            _players[(int)Players.EVIL_WIZARD]._sprite.CreateAnimationTakeHitRight(Content.Load<Texture2D>("Player/EVil_Wizard/TakeHitR"), 3, 250,250, 100, 1,8,0,0); 
            _players[(int)Players.EVIL_WIZARD]._sprite.CreateAnimationTakeHitLeft(Content.Load<Texture2D>("Player/EVil_Wizard/TakeHitL"), 3, 250,250, 100, 1,8,0,0);
            _players[(int)Players.EVIL_WIZARD]._sprite.CreateAnimationDeathRight(Content.Load<Texture2D>("Player/EVil_Wizard/DeathR"), 7, 250,250, 150, 1,8,0,0); 
            _players[(int)Players.EVIL_WIZARD]._sprite.CreateAnimationDeathLeft(Content.Load<Texture2D>("Player/EVil_Wizard/DeathL"), 7, 250,250, 150, 1,8,0,0);
        
            _players[(int)Players.EVIL_WIZARD].Icon = Content.Load<Texture2D>("Player/EVil_Wizard/icon");
        }
    
        public void LoadWizardSprite()
        {
            _players[(int)Players.WIZARD]._sprite.CreateAnimationStandRight(Content.Load<Texture2D>("Player/Wizard/IdleR"), 6, 231,190, 100, 1,8,0,0);
            _players[(int)Players.WIZARD]._sprite.CreateAnimationStandLeft(Content.Load<Texture2D>("Player/Wizard/IdleL"), 6, 231,190, 100, 1,8,0,0);
            _players[(int)Players.WIZARD]._sprite.CreateAnimationJumpRight(Content.Load<Texture2D>("Player/Wizard/JumpR"), 2, 231,190, 100, 1,2, 0,0);
            _players[(int)Players.WIZARD]._sprite.CreateAnimationJumpLeft(Content.Load<Texture2D>("Player/Wizard/JumpL"), 2, 231,190, 100, 1,2, 0,0);
            _players[(int)Players.WIZARD]._sprite.CreateAnimationRunRight(Content.Load<Texture2D>("Player/Wizard/RunR"), 8, 231,190, 100, 1,8,0,0);
            _players[(int)Players.WIZARD]._sprite.CreateAnimationRunLeft(Content.Load<Texture2D>("Player/Wizard/RunL"), 8, 231,190, 100, 1,8,0,0);
            _players[(int)Players.WIZARD]._sprite.CreateAnimationAttack1Right(Content.Load<Texture2D>("Player/Wizard/Attack1R"), 8, 231,190, 100, 1,8,0,0);
            _players[(int)Players.WIZARD]._sprite.CreateAnimationAttack1Left(Content.Load<Texture2D>("Player/Wizard/Attack1L"), 8, 231,190, 100, 1,8,0,0);
            _players[(int)Players.WIZARD]._sprite.CreateAnimationAttack2Right(Content.Load<Texture2D>("Player/Wizard/Attack2R"), 8, 231,190, 100, 1,8,0,0); 
            _players[(int)Players.WIZARD]._sprite.CreateAnimationAttack2Left(Content.Load<Texture2D>("Player/Wizard/Attack2L"), 8, 231,190, 100, 1,8,0,0); 
            _players[(int)Players.WIZARD]._sprite.CreateAnimationTakeHitRight(Content.Load<Texture2D>("Player/Wizard/TakeHitR"), 4, 231,190, 200, 1,8,0,0); 
            _players[(int)Players.WIZARD]._sprite.CreateAnimationTakeHitLeft(Content.Load<Texture2D>("Player/Wizard/TakeHitL"), 4, 231,190, 200, 1,8,0,0);
            _players[(int)Players.WIZARD]._sprite.CreateAnimationDeathRight(Content.Load<Texture2D>("Player/Wizard/DeathR"), 7, 231,190, 150, 1,8,0,0); 
            _players[(int)Players.WIZARD]._sprite.CreateAnimationDeathLeft(Content.Load<Texture2D>("Player/Wizard/DeathL"), 7, 231,190, 150, 1,8,0,0);
       
            _players[(int)Players.WIZARD].Icon = Content.Load<Texture2D>("Player/Wizard/icon");
        }
    
        public void LoadFireWizardSprite()
        {       
            _players[(int)Players.FIRE_WIZARD]._sprite.CreateAnimationStandRight(Content.Load<Texture2D>("Player/Fire_Wizard/IdleR"), 8, 150,150, 100, 1,8,0,0);
            _players[(int)Players.FIRE_WIZARD]._sprite.CreateAnimationStandLeft(Content.Load<Texture2D>("Player/Fire_Wizard/IdleL"), 8, 150,150, 100, 1,8,0,0);
            _players[(int)Players.FIRE_WIZARD]._sprite.CreateAnimationJumpRight(Content.Load<Texture2D>("Player/Fire_Wizard/IdleR"), 2, 150,150, 100, 1,2, 0,0);
            _players[(int)Players.FIRE_WIZARD]._sprite.CreateAnimationJumpLeft(Content.Load<Texture2D>("Player/Fire_Wizard/IdleL"), 2, 150,150, 100, 1,2, 0,0);
            _players[(int)Players.FIRE_WIZARD]._sprite.CreateAnimationRunRight(Content.Load<Texture2D>("Player/Fire_Wizard/RunR"), 8, 150,150, 100, 1,8,0,0);
            _players[(int)Players.FIRE_WIZARD]._sprite.CreateAnimationRunLeft(Content.Load<Texture2D>("Player/Fire_Wizard/RunL"), 8, 150,150, 100, 1,8,0,0);
            _players[(int)Players.FIRE_WIZARD]._sprite.CreateAnimationAttack1Right(Content.Load<Texture2D>("Player/Fire_Wizard/Attack1R"), 8, 150,150, 100, 1,8,0,0);
            _players[(int)Players.FIRE_WIZARD]._sprite.CreateAnimationAttack1Left(Content.Load<Texture2D>("Player/Fire_Wizard/Attack1L"), 8, 150,150, 100, 1,8,0,0);
            _players[(int)Players.FIRE_WIZARD]._sprite.CreateAnimationAttack2Right(Content.Load<Texture2D>("Player/Fire_Wizard/Attack1R"), 8, 150,150, 100, 1,8,0,0); 
            _players[(int)Players.FIRE_WIZARD]._sprite.CreateAnimationAttack2Left(Content.Load<Texture2D>("Player/Fire_Wizard/Attack1L"), 8, 150,150, 100, 1,8,0,0); 
            _players[(int)Players.FIRE_WIZARD]._sprite.CreateAnimationTakeHitRight(Content.Load<Texture2D>("Player/Fire_Wizard/TakeHitR"), 4, 150,150, 200, 1,8,0,0); 
            _players[(int)Players.FIRE_WIZARD]._sprite.CreateAnimationTakeHitLeft(Content.Load<Texture2D>("Player/Fire_Wizard/TakeHitL"), 4, 150,150, 200, 1,8,0,0);
            _players[(int)Players.FIRE_WIZARD]._sprite.CreateAnimationDeathRight(Content.Load<Texture2D>("Player/Fire_Wizard/DeathR"), 5, 150,150, 150, 1,8,0,0); 
            _players[(int)Players.FIRE_WIZARD]._sprite.CreateAnimationDeathLeft(Content.Load<Texture2D>("Player/Fire_Wizard/DeathL"), 5, 150,150, 150, 1,8,0,0);
        
            _players[(int)Players.FIRE_WIZARD].Icon = Content.Load<Texture2D>("Player/Fire_Wizard/icon");
        }
    
        public void LoadWarriorSprite()
        {
            _players[(int)Players.WARRIOR]._sprite.CreateAnimationStandRight(Content.Load<Texture2D>("Player/Warrior/IdleR"), 10, 135,135, 100, 1,10,0,0);
            _players[(int)Players.WARRIOR]._sprite.CreateAnimationStandLeft(Content.Load<Texture2D>("Player/Warrior/IdleL"), 10, 135,135, 100, 1,10,0,0);
            _players[(int)Players.WARRIOR]._sprite.CreateAnimationJumpRight(Content.Load<Texture2D>("Player/Warrior/JumpR"), 2, 135,135, 100, 1,2, 0,0);
            _players[(int)Players.WARRIOR]._sprite.CreateAnimationJumpLeft(Content.Load<Texture2D>("Player/Warrior/JumpL"), 2, 135,135, 100, 1,2, 0,0);
            _players[(int)Players.WARRIOR]._sprite.CreateAnimationRunRight(Content.Load<Texture2D>("Player/Warrior/RunR"), 6, 135,135, 100, 1,6,0,0);
            _players[(int)Players.WARRIOR]._sprite.CreateAnimationRunLeft(Content.Load<Texture2D>("Player/Warrior/RunL"), 6, 135,135, 100, 1,6,0,0);
            _players[(int)Players.WARRIOR]._sprite.CreateAnimationAttack1Right(Content.Load<Texture2D>("Player/Warrior/Attack1R"), 5, 135,135, 100, 1,5,0,0);
            _players[(int)Players.WARRIOR]._sprite.CreateAnimationAttack1Left(Content.Load<Texture2D>("Player/Warrior/Attack1L"), 5, 135,135, 100, 1,5,0,0);
            _players[(int)Players.WARRIOR]._sprite.CreateAnimationAttack2Right(Content.Load<Texture2D>("Player/Warrior/Attack2R"), 4, 135,135, 100, 1,4,0,0); 
            _players[(int)Players.WARRIOR]._sprite.CreateAnimationAttack2Left(Content.Load<Texture2D>("Player/Warrior/Attack2L"), 4, 135,135, 100, 1,4,0,0); 
            _players[(int)Players.WARRIOR]._sprite.CreateAnimationTakeHitRight(Content.Load<Texture2D>("Player/Warrior/TakeHitR"), 3, 135,135, 200, 1,3,0,0); 
            _players[(int)Players.WARRIOR]._sprite.CreateAnimationTakeHitLeft(Content.Load<Texture2D>("Player/Warrior/TakeHitL"), 3, 135,135, 200, 1,3,0,0);
            _players[(int)Players.WARRIOR]._sprite.CreateAnimationDeathRight(Content.Load<Texture2D>("Player/Warrior/DeathR"), 9, 135,135, 150, 1,9,0,0); 
            _players[(int)Players.WARRIOR]._sprite.CreateAnimationDeathLeft(Content.Load<Texture2D>("Player/Warrior/DeathL"), 9, 135,135, 150, 1,9,0,0);
        
            _players[(int)Players.WARRIOR].Icon = Content.Load<Texture2D>("Player/Warrior/icon");
        }

        public void LoadWMartialHeroSprite()
        {
            _players[(int)Players.MARTIAL_HERO]._sprite.CreateAnimationStandRight(Content.Load<Texture2D>("Player/Martial_Hero/IdleR"), 10, 126,126, 75, 1,10,0,0);
            _players[(int)Players.MARTIAL_HERO]._sprite.CreateAnimationStandLeft(Content.Load<Texture2D>("Player/Martial_Hero/IdleL"), 10, 126,126, 75, 1,10,0,0);
            _players[(int)Players.MARTIAL_HERO]._sprite.CreateAnimationJumpRight(Content.Load<Texture2D>("Player/Martial_Hero/JumpR"), 3, 126,126, 75, 1,3, 0,0);
            _players[(int)Players.MARTIAL_HERO]._sprite.CreateAnimationJumpLeft(Content.Load<Texture2D>("Player/Martial_Hero/JumpL"), 3, 126,126, 75, 1,3, 0,0);
            _players[(int)Players.MARTIAL_HERO]._sprite.CreateAnimationRunRight(Content.Load<Texture2D>("Player/Martial_Hero/RunR"), 8, 126,126, 75, 1,8,0,0);
            _players[(int)Players.MARTIAL_HERO]._sprite.CreateAnimationRunLeft(Content.Load<Texture2D>("Player/Martial_Hero/RunL"), 8, 126,126, 75, 1,8,0,0);
            _players[(int)Players.MARTIAL_HERO]._sprite.CreateAnimationAttack1Right(Content.Load<Texture2D>("Player/Martial_Hero/Attack1R"), 7, 126,126, 75, 1,7,0,0);
            _players[(int)Players.MARTIAL_HERO]._sprite.CreateAnimationAttack1Left(Content.Load<Texture2D>("Player/Martial_Hero/Attack1L"), 7, 126,126, 75, 1,7,0,0);
            _players[(int)Players.MARTIAL_HERO]._sprite.CreateAnimationAttack2Right(Content.Load<Texture2D>("Player/Martial_Hero/Attack2R"), 6, 126,126, 75, 1,6,0,0); 
            _players[(int)Players.MARTIAL_HERO]._sprite.CreateAnimationAttack2Left(Content.Load<Texture2D>("Player/Martial_Hero/Attack2L"), 6, 126,126, 75, 1,6,0,0); 
            _players[(int)Players.MARTIAL_HERO]._sprite.CreateAnimationTakeHitRight(Content.Load<Texture2D>("Player/Martial_Hero/TakeHitR"), 3, 126,126, 100, 1,3,0,0); 
            _players[(int)Players.MARTIAL_HERO]._sprite.CreateAnimationTakeHitLeft(Content.Load<Texture2D>("Player/Martial_Hero/TakeHitL"), 3, 126,126, 100, 1,3,0,0);
            _players[(int)Players.MARTIAL_HERO]._sprite.CreateAnimationDeathRight(Content.Load<Texture2D>("Player/Martial_Hero/DeathR"), 11, 126,126, 100, 1,11,0,0); 
            _players[(int)Players.MARTIAL_HERO]._sprite.CreateAnimationDeathLeft(Content.Load<Texture2D>("Player/Martial_Hero/DeathL"), 11, 126,126, 100, 1,11,0,0);
        
            _players[(int)Players.MARTIAL_HERO].Icon = Content.Load<Texture2D>("Player/Martial_Hero/icon");
        }

        public void LoadWaterMaster()
        {
            _players[(int)Players.WATER_MASTER]._sprite.CreateAnimationStandRight(Content.Load<Texture2D>("Player/Water_Master/IdleR"), 8, 288,126, 75, 1,8,0,0);
            _players[(int)Players.WATER_MASTER]._sprite.CreateAnimationStandLeft(Content.Load<Texture2D>("Player/Water_Master/IdleL"), 8, 288,126, 75, 1,8,0,0);
            _players[(int)Players.WATER_MASTER]._sprite.CreateAnimationJumpRight(Content.Load<Texture2D>("Player/Water_Master/JumpR"), 3, 288,126, 75, 1,3, 0,0);
            _players[(int)Players.WATER_MASTER]._sprite.CreateAnimationJumpLeft(Content.Load<Texture2D>("Player/Water_Master/JumpL"), 3, 288,126, 75, 1,3, 0,0);
            _players[(int)Players.WATER_MASTER]._sprite.CreateAnimationRunRight(Content.Load<Texture2D>("Player/Water_Master/RunR"), 8, 288,126, 75, 1,8,0,0);
            _players[(int)Players.WATER_MASTER]._sprite.CreateAnimationRunLeft(Content.Load<Texture2D>("Player/Water_Master/RunL"), 8, 288,126, 75, 1,8,0,0);
            _players[(int)Players.WATER_MASTER]._sprite.CreateAnimationAttack1Right(Content.Load<Texture2D>("Player/Water_Master/Attack1R"), 21, 288,126, 75, 1,21,0,0);
            _players[(int)Players.WATER_MASTER]._sprite.CreateAnimationAttack1Left(Content.Load<Texture2D>("Player/Water_Master/Attack1L"), 21, 288,126, 75, 1,21,0,0);
            _players[(int)Players.WATER_MASTER]._sprite.CreateAnimationAttack2Right(Content.Load<Texture2D>("Player/Water_Master/Attack2R"), 32, 288,126, 75, 1,32,0,0); 
            _players[(int)Players.WATER_MASTER]._sprite.CreateAnimationAttack2Left(Content.Load<Texture2D>("Player/Water_Master/Attack2L"), 32, 288,126, 75, 1,32,0,0); 
            _players[(int)Players.WATER_MASTER]._sprite.CreateAnimationTakeHitRight(Content.Load<Texture2D>("Player/Water_Master/TakeHitR"), 7, 288,126, 100, 1,7,0,0); 
            _players[(int)Players.WATER_MASTER]._sprite.CreateAnimationTakeHitLeft(Content.Load<Texture2D>("Player/Water_Master/TakeHitL"), 7, 288,126, 100, 1,7,0,0);
            _players[(int)Players.WATER_MASTER]._sprite.CreateAnimationDeathRight(Content.Load<Texture2D>("Player/Water_Master/DeathR"), 16, 288,126, 100, 1,16,0,0); 
            _players[(int)Players.WATER_MASTER]._sprite.CreateAnimationDeathLeft(Content.Load<Texture2D>("Player/Water_Master/DeathL"), 16, 288,126, 100, 1,16,0,0);
        
            _players[(int)Players.WATER_MASTER].Icon = Content.Load<Texture2D>("Player/Water_Master/icon");
        }

        public void LoadAllSprite()
        {
            //LoadRootSprite();
            LoadWizardSprite();
            LoadEvilWizardSprite();
            LoadFireWizardSprite();
            LoadWarriorSprite();
            LoadWMartialHeroSprite();
            LoadWaterMaster();
        }
    
        public Texture2D[] GetIcons()
        {
            Texture2D[] icons = new Texture2D[_players.Length];
            for (int i = 0; i < _players.Length; i++)
            {
                icons[i] = _players[i].Icon;
            }

            return icons;
        }
    }

    public enum Backgrounds
    {
        SNK,
        Night,
        Beach
    }

    public class BackgroundImporter
    {
        private GraphicsDevice GraphicsDevice;
        private ContentManager Content;
        private Background[] backgrounds;

        public BackgroundImporter()
        {
            backgrounds = new Background[Enum.GetNames(typeof(Backgrounds)).Length];
        }

        public void Init(ContentManager content, GraphicsDevice gd)
        {
            Content = content;
            GraphicsDevice = gd;

            for (int i = 0; i < backgrounds.Length; i++)
            {
                backgrounds[i] = new Background(1080, 720);
            }
        }

        public void Load(int index,Texture2D texture, int SpriteFrame, int width, 
        int height, int FrameTime, int LineNumber, int ColumnNumber, int LineIndex, int ColumnIndex)
        {
            backgrounds[index].Load(texture, SpriteFrame, width, height, FrameTime,
            LineNumber, ColumnNumber, LineIndex, ColumnIndex);
        }

        public void LoadWater()
        {
            this.Load((int)Backgrounds.SNK, Content.Load<Texture2D>("Backgrounds/Water/water"), 8, 1024, 384, 75,
            2, 5, 0, 0);

            backgrounds[(int)Backgrounds.SNK].Icon = Content.Load<Texture2D>("Backgrounds/Water/icon");
        }

        public void LoadNight()
        {
            this.Load((int)Backgrounds.Night, Content.Load<Texture2D>("Backgrounds/Night/night"), 48, 752, 248, 50,
            10, 5, 0, 0);

            backgrounds[(int)Backgrounds.Night].Icon = Content.Load<Texture2D>("Backgrounds/Night/icon");
        }

        public void LoadBeach()
        {
            this.Load((int)Backgrounds.Beach, Content.Load<Texture2D>("Backgrounds/Beach/beach"), 36, 640, 384, 50,
            6, 7, 0, 0);

            backgrounds[(int)Backgrounds.Beach].Icon = Content.Load<Texture2D>("Backgrounds/Beach/icon");
        }

        public void LoadAll()
        {
            this.LoadWater();
            this.LoadNight();
            this.LoadBeach();
        }

        public Background Get(Backgrounds background)
        {
            return backgrounds[(int)background];
        }

        public Texture2D[] GetIcons()
        {
            Texture2D[] icons = new Texture2D[backgrounds.Length];
            for (int i = 0; i < icons.Length; i++)
            {
                icons[i] = backgrounds[i].Icon;
            }

            return icons;
        }
    }

    public enum Fonts
    {
        MainFont
    }

    public class FontImporter
    {
        private GraphicsDevice GraphicsDevice;
        private ContentManager Content;
        private SpriteFont[] _fonts;

        public FontImporter()
        {
            _fonts = new SpriteFont[Enum.GetNames(typeof(Fonts)).Length];
        }

        public void Init(GraphicsDevice grahpics, ContentManager content)
        {
            GraphicsDevice = grahpics;
            Content = content;
        }

        public void Load(Fonts font, string assets)
        {
            _fonts[(int)font] = Content.Load<SpriteFont>(assets);
        }

        public void LoadMainFont()
        {
            this.Load(Fonts.MainFont, "Fonts/MainFont");
        }   

        public void LoadAll()
        {
            this.LoadMainFont();
        }
    
        public SpriteFont Get(Fonts font)
        {
            return _fonts[(int)font];
        }
    }

}
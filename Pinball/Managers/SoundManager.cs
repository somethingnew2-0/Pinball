using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace Pinball
{
    /// <summary>
    /// Doesn't draw, but loads content
    /// </summary>
    public class SoundManager : DrawableGameComponent
    {
        public ContentManager Content { get; private set; }

        public static Dictionary<String, SoundEffect> SoundEffects { get; set; }

        public static Song MainSong { get; set; }

        public static Song SpeedUpSong { get; set; }

        public static float Volume
        {
            get
            {
                return SoundEffect.MasterVolume;
            }
            set
            {
                SoundEffect.MasterVolume = value;
            }
        }

        public SoundManager(Game game) : base(game)
        {
            Content = new ContentManager(game.Services, "Content");

            game.Services.AddService(typeof(SoundManager), this);

            SoundEffects = new Dictionary<string, SoundEffect>();

            SoundEffect.MasterVolume = 0.5f;
        } 

        protected override void LoadContent()
        {
            //SoundEffects.Add("BallRoll", Content.Load<SoundEffect>(@"SoundEffects\BallRoll"));
            //SoundEffects.Add("BellRing", Content.Load<SoundEffect>(@"SoundEffects\BellRing"));
            //SoundEffects.Add("Boing", Content.Load<SoundEffect>(@"SoundEffects\Boing"));
            SoundEffects.Add("CircleBumper", Content.Load<SoundEffect>(@"SoundEffects\CircleBumper"));
            SoundEffects.Add("Click1", Content.Load<SoundEffect>(@"SoundEffects\Click1"));
            SoundEffects.Add("Click2", Content.Load<SoundEffect>(@"SoundEffects\Click2"));
            SoundEffects.Add("ClickClack1", Content.Load<SoundEffect>(@"SoundEffects\ClickClack1"));
            SoundEffects.Add("ClickClack2", Content.Load<SoundEffect>(@"SoundEffects\ClickClack2"));
            SoundEffects.Add("Flipper", Content.Load<SoundEffect>(@"SoundEffects\Flipper"));
            SoundEffects.Add("Jackpot", Content.Load<SoundEffect>(@"SoundEffects\Jackpot"));
            SoundEffects.Add("LargePlungerHit", Content.Load<SoundEffect>(@"SoundEffects\LargePlungerHit"));
            SoundEffects.Add("LostBall", Content.Load<SoundEffect>(@"SoundEffects\LostBall"));
            SoundEffects.Add("MetalCrash1", Content.Load<SoundEffect>(@"SoundEffects\MetalCrash1"));
            SoundEffects.Add("MetalCrash2", Content.Load<SoundEffect>(@"SoundEffects\MetalCrash2"));
            SoundEffects.Add("FlipperBumper", Content.Load<SoundEffect>(@"SoundEffects\FlipperBumper"));
            SoundEffects.Add("PowerDown", Content.Load<SoundEffect>(@"SoundEffects\PowerDown"));
            SoundEffects.Add("PowerUp1", Content.Load<SoundEffect>(@"SoundEffects\PowerUp1"));
            SoundEffects.Add("PowerUp2", Content.Load<SoundEffect>(@"SoundEffects\PowerUp2"));
            SoundEffects.Add("PowerUp3", Content.Load<SoundEffect>(@"SoundEffects\PowerUp3"));
            //SoundEffects.Add("PowerUp4", Content.Load<SoundEffect>(@"SoundEffects\PowerUp4"));
            SoundEffects.Add("SmallPlungerHit", Content.Load<SoundEffect>(@"SoundEffects\SmallPlungerHit"));
            SoundEffects.Add("SpaceBeep1", Content.Load<SoundEffect>(@"SoundEffects\SpaceBeep1"));
            SoundEffects.Add("SpaceBeep2", Content.Load<SoundEffect>(@"SoundEffects\SpaceBeep2"));
            SoundEffects.Add("SpaceBeep3", Content.Load<SoundEffect>(@"SoundEffects\SpaceBeep3"));
            //SoundEffects.Add("SpaceBeep4", Content.Load<SoundEffect>(@"SoundEffects\SpaceBeep4"));
            SoundEffects.Add("SpaceBeep5", Content.Load<SoundEffect>(@"SoundEffects\SpaceBeep5"));
            //SoundEffects.Add("SpaceBuzzer1", Content.Load<SoundEffect>(@"SoundEffects\SpaceBuzzer1"));
            //SoundEffects.Add("SpaceBuzzer2", Content.Load<SoundEffect>(@"SoundEffects\SpaceBuzzer2"));
            SoundEffects.Add("Start", Content.Load<SoundEffect>(@"SoundEffects\Start"));
            SoundEffects.Add("Whoosh", Content.Load<SoundEffect>(@"SoundEffects\Whoosh"));

            MainSong = Content.Load<Song>(@"Songs\MainLoop");

            SpeedUpSong = Content.Load<Song>(@"Songs\BrickBeat");

        }
    }
}
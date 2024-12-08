

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Audio
{
    public interface Audio
    {
        public void Load(ContentManager content, string asset);

        public void Play();
        public void Pause();
        public void Stop();

        public bool IsPlaying();
        public bool IsPaused();
        public bool IsStopped();
    }

    public class Sound : Audio
    {
        private SoundEffect sound;
        private SoundEffectInstance SoundInstance;

        public void Dispose()
        {
            SoundInstance.Dispose();
        }

        public void Load(ContentManager content, string asset)
        {
            sound = content.Load<SoundEffect>(asset);
            SoundInstance = sound.CreateInstance();
        }

        public bool IsPlaying()
        {
            return SoundInstance.State == SoundState.Playing;
        }

        public bool IsPaused()
        {
            return SoundInstance.State == SoundState.Paused;
        }

        public bool IsStopped()
        {
            return SoundInstance.State == SoundState.Stopped;
        }

        public void Play()
        {
            if (!IsPlaying())
                SoundInstance.Play();
            else if (IsPaused())
                SoundInstance.Resume();
        }

        public void Pause()
        {
            if (!IsPaused())
                SoundInstance.Pause();
        }

        public void Stop()
        {
            if (!IsStopped())
                SoundInstance.Stop();
        }
        public void Stop(bool now)
        {
            if (!IsStopped())
                SoundInstance.Stop(now);
        }
    }


    public class Music : Audio
    {
        private Song song;

        public void Load(ContentManager content, string asset)
        {
            song = content.Load<Song>(asset);
        }

        public bool IsPlaying()
        {
            return MediaPlayer.State == MediaState.Playing;
        }

        public bool IsPaused()
        {
            return MediaPlayer.State == MediaState.Paused;
        }

        public bool IsStopped()
        {
            return MediaPlayer.State == MediaState.Stopped;
        }

        public void Play()
        {
            if (!IsPlaying())
                MediaPlayer.Play(song);
            else if (IsPaused())
                MediaPlayer.Resume();
        }

        public void Pause()
        {
            if (!IsPaused())
                MediaPlayer.Pause();
        }

        public void Stop()
        {
            if (!IsStopped())
                MediaPlayer.Stop();
        }
    }
}
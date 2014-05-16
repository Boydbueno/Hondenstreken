using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace HondenStreken
{
    class AudioEffect
    {

        #region Fields
        private SoundEffect _soundEffect;
        private SoundEffectInstance _soundEffectInstance;
        #endregion

        #region Properties
        public double Duration { get; private set; }

        /// <summary>
        /// The totalseconds when the track is started
        /// </summary>
        public double TimeStarted { get; private set; }

        /// <summary>
        /// The totalseconds when the track should to be finished.
        /// </summary>
        public double TimeFinished { get; private set; }
        public bool IsPlaying
        {
            get
            {
                return (_soundEffectInstance.State.Equals(SoundState.Playing));
            }
        }

        public bool HasStarted { get; private set; }
        #endregion

        #region Constructors
        public AudioEffect(SoundEffect soundEffect)
        {
            _soundEffect = soundEffect;
            Duration = _soundEffect.Duration.TotalSeconds;

            _soundEffectInstance = soundEffect.CreateInstance();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Play the audio and stores the time the audio started and when it will be finished
        /// </summary>
        /// <param name="gameTime"></param>
        public void Play(GameTime gameTime)
        {
            TimeStarted = gameTime.TotalGameTime.TotalSeconds;
            TimeFinished = TimeStarted + Duration;

            _soundEffectInstance.Play();
            HasStarted = true;
        }

        /// <summary>
        /// Play the audio if it hasn't started yet
        /// </summary>
        public void PlayOnce(GameTime gameTime)
        {
            if (!HasStarted)
            {
                Play(gameTime);
            }
        }

        public void Restart(GameTime gameTime)
        {
            HasStarted = false;
            PlayOnce(gameTime);
        }

        public void Stop()
        {
            _soundEffectInstance.Stop();
        }

        public void Pause()
        {
            _soundEffectInstance.Pause();
        }

        public void Resume()
        {
            _soundEffectInstance.Resume();
        }
        #endregion
    }
}

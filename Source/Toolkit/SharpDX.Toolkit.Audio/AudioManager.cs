﻿// Copyright (c) 2010-2013 SharpDX - SharpDX Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using SharpDX.Toolkit.Content;

namespace SharpDX.Toolkit.Audio
{
    using SharpDX.XAudio2;
    using SharpDX.Multimedia;
    using SharpDX.X3DAudio;
using SharpDX.XAPO.Fx;

    /// <summary>
    /// This manages the XAudio2 audio graph, device, and mastering voice.  This manager also allows loading of <see cref="SoundEffect"/> using
    /// the <see cref="IContentManager"/>
    /// </summary>
    public class AudioManager : GameSystem, IContentReader, IContentReaderFactory 
    {
        private ContentManager contentManager;
        private float masterVolume;
        private X3DAudio x3DAudio;
        private bool isMasteringLimiterEnabled;
        private MasteringLimiterParameters masteringLimiterParameters;
        private MasteringLimiter masteringLimiter;
       
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioManager" /> class.
        /// </summary>
        /// <param name="game">The game.</param>
        public AudioManager(Game game)
            : base(game)
        {
            Services.AddService(this);
            masterVolume = 1.0f;
            Speakers = Multimedia.Speakers.None;
            masteringLimiterParameters = new MasteringLimiterParameters { Loudness = MasteringLimiter.DefaultLoudness, Release = MasteringLimiter.DefaultRelease };
            InstancePool = new SoundEffectInstancePool(this);

            // register the audio manager as game system
            game.GameSystems.Add(this);
        }


        /// <summary>
        /// Initializes XAudio2 and MasteringVoice.  And registers itself as an <see cref="IContentReaderFactory"/>
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            contentManager = Content as ContentManager;
            if (contentManager == null)
            {
                throw new InvalidOperationException("Unable to initialize AudioManager. Expecting IContentManager to be an instance of ContentManager");
            }

#if !WIN8METRO && !WP8 && DEBUG
            try
            {
                Device = new XAudio2(XAudio2Flags.DebugEngine, ProcessorSpecifier.DefaultProcessor);
                Device.StartEngine();
            }
            catch (Exception)
#endif
            {
                Device = new XAudio2(XAudio2Flags.None, ProcessorSpecifier.DefaultProcessor);
                Device.StartEngine();                
            }  

           

#if WIN8METRO || WP8
            string deviceId = null;
#else
            const int deviceId = 0;
#endif
            MasteringVoice = new MasteringVoice(Device, XAudio2.DefaultChannels, XAudio2.DefaultSampleRate, deviceId);
            MasteringVoice.SetVolume(masterVolume);



#if WIN8METRO || WP8
            Speakers = (Speakers)MasteringVoice.ChannelMask;
#else
            var deviceDetails = Device.GetDeviceDetails(deviceId);
            Speakers = deviceDetails.OutputFormat.ChannelMask;
#endif
            x3DAudio = new X3DAudio(Speakers, SoundEffect.SpeedOfSound);
            

            if(isMasteringLimiterEnabled)
            {
                masteringLimiter = new MasteringLimiter();
                masteringLimiter.Parameter = masteringLimiterParameters;
                MasteringVoice.SetEffectChain(new EffectDescriptor(masteringLimiter));
            }

            contentManager.ReaderFactories.Add(this);
        }


        /// <summary>
        /// Sets and gets the volume of the Masteing voice.
        /// </summary>
        public float MasterVolume
        {
            get
            {
                return masterVolume;
            }
            set
            {

                if (value == masterVolume)
                    return;

                masterVolume = MathUtil.Clamp(value, 0.0f, 1.0f);

                if (MasteringVoice != null)
                    MasteringVoice.SetVolume(masterVolume);
            }
        }


        internal Speakers Speakers { get; private set; }
        internal XAudio2 Device { get; private set; }
        internal MasteringVoice MasteringVoice  { get; private set; }
        internal SoundEffectInstancePool InstancePool { get; private set; }


        public void EnableMasterVolumeLimiter()
        {
            if (isMasteringLimiterEnabled)
                return;

            if (MasteringVoice != null)
            {
                if (masteringLimiter == null)
                {                    
                    masteringLimiter = new MasteringLimiter();
                    masteringLimiter.Parameter = masteringLimiterParameters;
                    MasteringVoice.SetEffectChain(new EffectDescriptor(masteringLimiter));
                }
                else
                {
                    MasteringVoice.EnableEffect(0);
                }
            }            

            isMasteringLimiterEnabled = true;
        }

        public void DisableMasterVolumeLimiter()
        {
            if (!isMasteringLimiterEnabled)
                return;            

            if (MasteringVoice != null && masteringLimiter != null)
            {
                MasteringVoice.DisableEffect(0);
            }

            isMasteringLimiterEnabled = false;
        }

        public void SetMasteringLimit(int release, int loudness)
        {
            if (release < MasteringLimiter.MinimumRelease || release > MasteringLimiter.MaximumRelease)
                throw new ArgumentOutOfRangeException("release");

            if (loudness < MasteringLimiter.MinimumLoudness || loudness > MasteringLimiter.MaximumLoudness)
                throw new ArgumentOutOfRangeException("loudness");
            
            masteringLimiterParameters = new MasteringLimiterParameters { Loudness = loudness, Release = release };

            if(MasteringVoice != null && masteringLimiter != null)
            {
                MasteringVoice.SetEffectParameters(0, masteringLimiterParameters);
            }
        }

        internal void Calculate3D(Listener listener, Emitter emitter, CalculateFlags flags, DspSettings dspSettings)
        {
            x3DAudio.Calculate(listener, emitter, flags, dspSettings);            
        }


        IContentReader IContentReaderFactory.TryCreate(Type type)
        {
            if (type == typeof(SoundEffect) || type == typeof(WaveBank))
                return this;

            return null;
        }


        object IContentReader.ReadContent(IContentManager contentManager, ref ContentReaderParameters parameters)
        {
            if (parameters.AssetType == typeof(SoundEffect))
                return SoundEffect.FromStream(this, parameters.Stream, parameters.AssetName);

            if (parameters.AssetType == typeof(WaveBank))
                return WaveBank.FromStream(this, parameters.Stream);

            return null;
        }

        internal SoundEffect ToDisposeSoundEffect(SoundEffect soundEffect)
        {
            return ToDispose(soundEffect);
        }


        protected override void Dispose(bool disposeManagedResources)
        {            
            if (disposeManagedResources)
            {
                InstancePool.Dispose();
                base.Dispose(disposeManagedResources);
                if (x3DAudio != null)
                {
                    x3DAudio = null;
                }

                if (MasteringVoice != null)
                {
                    MasteringVoice.DestroyVoice();
                    MasteringVoice.Dispose();
                    MasteringVoice = null;
                }

                if(masteringLimiter != null)
                {
                    masteringLimiter.Dispose();
                    masteringLimiter = null;
                }
                isMasteringLimiterEnabled = false;

                if (Device != null)
                {
                    Device.StopEngine();
                    Device.Dispose();
                    Device = null;
                }
            }

        }  
    }
}

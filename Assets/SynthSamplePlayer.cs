using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets
{
    [RequireComponent(typeof(AudioSource))]
    class SynthSamplePlayer : MonoBehaviour
    {
        public SynthSample Sample;
        public float CurrentTime;

        public const double sampling_freq = 40000;

        private double increment;
        private double phase;
        private float gain = 0.15f;
        private AudioSource _audioSource;

        private bool audioSourceEnabled;

        private void OnEnable()
        {
            if (this.Sample != null)
                this.Sample.Reset();
        }

        private void Update()
        {
            this.Sample.SetCurrentFrequency(this.CurrentTime);
            if (this._audioSource == null)
                this._audioSource = this.GetComponent<AudioSource>();            
        }

        private void OnAudioFilterRead(float[] data, int channels)
        {
            if (this.CurrentTime < this.Sample.startTime || this.Sample.IsDone(this.CurrentTime))
            {
                //this.audioSourceEnabled = false;
                return;
            }
            //if (this.audioSourceEnabled == false)
            //    this.audioSourceEnabled = true;
            this.increment = this.Sample.currentFreq * 2.0 * Mathf.PI / sampling_freq;
            for (int i = 0; i < data.Length; i += channels)
            {
                this.phase += this.increment;
                data[i] = (float)(this.gain * Mathf.Sin((float)this.phase));
                // if we have multiple speakers, play it on both
                if (channels == 2)
                    data[i + 1] = data[i];
                if (this.phase > Mathf.PI * 2)
                    this.phase = 0;
            }
        }

    }
}

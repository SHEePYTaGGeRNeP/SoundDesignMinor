// Needed for Math

namespace Assets.Scripts
{
    using System;

    using UnityEngine;

    public class Sinus : MonoBehaviour
    {
        // un-optimized version
        public double frequency = 440;
        public double gain = 0.05;

        private double increment;
        private double phase;
        private double sampling_frequency = 48000;

        void OnAudioFilterRead(float[] data, int channels)
        {
            // update increment in case frequency has changed
            this.increment = this.frequency * 2 * Math.PI / this.sampling_frequency;
            for (var i = 0; i < data.Length; i = i + channels)
            {
                this.phase = this.phase + this.increment;
                // this is where we copy audio data to make them “available” to Unity
                data[i] = (float)(this.gain * Math.Sin(this.phase));
                // if we have stereo, we copy the mono data to each channel
                if (channels == 2) data[i + 1] = data[i];
                if (this.phase > 2 * Math.PI) this.phase = 0;
            }
        }
    }
}
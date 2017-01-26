namespace Assets.Scripts
{
    using UnityEngine;

    public class Oscillator1Freq : MonoBehaviour
    {
        [SerializeField]
        private int minFreq = 440;
        [SerializeField]
        private int maxFreq = 2000;
        [SerializeField]
        private int freqIncrement = 10;
        [SerializeField]
        private int currentFreq;
        private bool forwards = true;

        private double increment;
        private double phase;
        private readonly double sampling_freq = 40000;

        [SerializeField]
        private float gain = 0.15f;

        private void Awake()
        {
            this.currentFreq = this.minFreq;
        }
        private void Update()
        {
            if (this.forwards)
                this.currentFreq += this.freqIncrement;
            else
                this.currentFreq -= this.freqIncrement;

            if (this.currentFreq >= this.maxFreq)
                this.forwards = false;
            if (this.currentFreq <= this.minFreq)
                this.forwards = true;
        }

        private void OnAudioFilterRead(float[] data, int channels)
        {
            this.increment = this.currentFreq * 2.0 * Mathf.PI / this.sampling_freq;
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

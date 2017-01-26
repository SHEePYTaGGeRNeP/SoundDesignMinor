using UnityEngine;

namespace Assets
{
    public class Oscillator : MonoBehaviour
    {

        [SerializeField]
        private double frequency = 440;

        private double increment;
        private double phase;
        private readonly double sampling_freq = 40000;

        [SerializeField]
        private float gain = 0.15f;
        [SerializeField]
        private float volume = 0.1f;

        private float[] frequencies;
        private int thisFreq;

        private void Start()
        {
            this.frequencies = new float[8] { 440, 494, 554, 587, 659, 740, 831, 880 };
            this.gain = 0;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.gain = this.volume;
                this.frequency = this.frequencies[this.thisFreq];
                this.thisFreq ++;
                this.thisFreq = this.thisFreq % this.frequencies.Length;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                this.gain = 0;
            }
        }

        private void OnAudioFilterRead(float[] data, int channels)
        {
            // update increment in case frequency has changed
            this.increment = this.frequency * 2.0 * Mathf.PI / this.sampling_freq;
            for (int i = 0; i < data.Length; i += channels)
            {
                this.phase += this.increment;
                // this is where we copy audio data to make them “available” to Unity
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

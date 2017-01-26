namespace Assets.Scripts
{
    using UnityEngine;
    using UnityEngine.Audio;

    [RequireComponent(typeof(AudioSource))]
    public class SynthSamplePlayer : MonoBehaviour
    {

        public enum DataMode { Sinus, Sawtooth, Block, Noise, Silent }
        public DataMode dataMode;
        public SynthSample Sample;
        public float CurrentTime;
        private readonly System.Random random = new System.Random();

        public const double sampling_freq = 40000;

        private double increment;
        private double phase;
        public float gain = 0.05f;
        private float _randomData;

        private void OnEnable()
        {
            if (this.Sample != null)
                this.Sample.Reset();
            this.phase = 0;
        }

        private void Update()
        {
            if (this.Sample.ShouldPlay(this.CurrentTime))
                this.Sample.SetCurrentFrequency(this.CurrentTime);
        }

        private void OnAudioFilterRead(float[] data, int channels)
        {
            if (this.Sample == null) return;
            if (!this.Sample.ShouldPlay(this.CurrentTime) || this.Sample.IsDone(this.CurrentTime))
            {
                return;
            }
            // update increment in case frequency has changed
            this.increment = this.Sample.currentFreq * 2.0 * Mathf.PI / sampling_freq;
            if (this.dataMode == DataMode.Noise)
                this._randomData = (gain * (this.random.Next(-1000, 1000) / 1000f));
            for (int i = 0; i < data.Length; i += channels)
            {
                this.phase += this.increment;
                // this is where we copy audio data to make them “available” to Unity
                data[i] = this.GetData(this._randomData);
                // if we have multiple speakers, play it on both
                if (channels == 2)
                    data[i + 1] = data[i];
                if (this.phase > Mathf.PI * 2)
                    this.phase = 0;
            }
        }
        public float GetData(float randomData)
        {
            switch (this.dataMode)
            {
                default:
                case DataMode.Sinus: return (float)(gain * Mathf.Sin((float)this.phase));
                case DataMode.Sawtooth:
                    float perc = (float)this.phase / (Mathf.PI * 2);
                    return (float)(gain * Mathf.Lerp(-1, 1, perc));
                case DataMode.Block: return (float)(gain * (this.phase > Mathf.PI ? 1 : 0));
                case DataMode.Noise: return randomData;
                case DataMode.Silent: return 0;
            }
        }

    }
}

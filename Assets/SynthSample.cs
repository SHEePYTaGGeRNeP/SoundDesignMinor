using System;

namespace Assets
{
    using UnityEngine;

    [Serializable]
    public class SynthSample
    {
        public float startTime;
        public float duration;
        public enum SampleMode { Normal, FromTo }
        public SampleMode mode;
        public int currentFreq;
        public int startFreq;
        public int endFreq;
        public int freqStep;
        public bool done;

        private bool? forwards = null;
        private bool _firstPlayed;

        public void SetCurrentFrequency(float currentTime)
        {
            if (this.forwards == null)
            {
                this.forwards = this.startFreq < this.endFreq;
                this.currentFreq = this.startFreq;
                return;
            }
            switch (this.mode)
            {
                case SampleMode.Normal:
                    this.currentFreq = this.startFreq;
                    break;
                case SampleMode.FromTo:
                    if (currentTime < this.startTime) return;
                    if (!this._firstPlayed)
                    {
                        this._firstPlayed = true;
                        return;
                    }
                    if (this.forwards == true)
                        this.currentFreq = Mathf.Min(this.currentFreq + this.freqStep, this.endFreq);
                    else
                        this.currentFreq = Mathf.Max(this.currentFreq - this.freqStep, this.endFreq);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Reset()
        {
            this._firstPlayed = false;
            this.done = false;
            this.currentFreq = this.startFreq;
        }
        public bool IsDone(float currentTime)
        {
            switch (this.mode)
            {
                case SampleMode.Normal:
                    this.done = currentTime > this.startTime + this.duration;
                    break;
                case SampleMode.FromTo:
                    this.done = this.currentFreq == this.endFreq;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return this.done;
        }

        public bool ShouldPlay(float currentTime)
        {
            switch (this.mode)
            {
                case SampleMode.Normal:
                    return currentTime > this.startTime;
                case SampleMode.FromTo:
                    return currentTime > this.startTime;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}

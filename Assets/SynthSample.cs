using System;

namespace Assets
{
    using System.Collections.Generic;

    using UnityEngine;

    [Serializable]
    public class SynthSample
    {
        public SynthSamplePlayer.DataMode dataMode;
        public enum StartMode { Time, AfterPrevious, WithPrevious }
        public StartMode startMode;
        public float startTime;
        public int startFreq;
        public SampleMode sampleMode;
        public enum SampleMode { Normal, FromTo }
        public float duration;
        public int endFreq;
        public int freqStep;

        [Header("Debug")]
        public int currentFreq;
        public bool waiting = true;
        public bool done;

        private bool _sentStart;
        private bool _sentDone;
        private bool? forwards = null;
        private bool _firstPlayed;

        public SynthSamplePlayer SamplePlayer;
        [HideInInspector]
        public List<SynthSample> waitingForThisToFinish = new List<SynthSample>();
        [HideInInspector]
        public List<SynthSample> waitingForThisToStart = new List<SynthSample>();

        public void SetCurrentFrequency(float currentTime)
        {
            if (this.forwards == null)
            {
                this.forwards = this.startFreq < this.endFreq;
                this.currentFreq = this.startFreq;
                return;
            }
            if (!this._sentStart)
            {
                this._sentStart = true;
                for (int i = 0; i < this.waitingForThisToStart.Count; i++)
                    this.waitingForThisToStart[i].StopWaiting(currentTime);
            }
            switch (this.sampleMode)
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
            this._sentDone = false;
            this._sentStart = false;
            this.waiting = true;
            this.currentFreq = this.startFreq;
        }
        public bool IsDone(float currentTime)
        {
            if (this.waiting && this.startMode != StartMode.Time) return false;
            switch (this.sampleMode)
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
            if (!this.done) return this.done;
            if (!this._sentDone)
            {
                this._sentDone = true;
                for (int i = 0; i < this.waitingForThisToFinish.Count; i++)
                    this.waitingForThisToFinish[i].StopWaiting(currentTime);
            }
            return this.done;
        }

        public void StopWaiting(float time)
        {
            this.startTime = time;
            this.waiting = false;
        }

        public bool ShouldPlay(float currentTime)
        {
            if (this.startMode != StartMode.Time && this.waiting) return false;
            switch (this.sampleMode)
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

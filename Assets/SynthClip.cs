using System;
using UnityEngine;
namespace Assets
{
    using System.Collections.Generic;
    using System.Linq;
    
    public class SynthClip : MonoBehaviour
    {
        [SerializeField]
        private bool _useUnityTime = false;
        public SynthSample[] Samples;

        // we use DateTime so it still works when paused.
        private float startTimeUnity;
        private DateTime startTime;
        private bool _initialised;

        [Header("debug")]
        [SerializeField]
        private float _currentTime;
        public float CurrentTime
        {
            get
            {
                if (this._useUnityTime)
                    return Time.time - this.startTimeUnity;
                else
                    return (float)(DateTime.Now - this.startTime).TotalSeconds;
            }
        }

        private readonly List<SynthSamplePlayer> _synthSamplePlayers = new List<SynthSamplePlayer>();
        private readonly List<SynthSamplePlayer> _synthPlayersToPlay = new List<SynthSamplePlayer>();
        private readonly List<SynthSamplePlayer> _playersToRemove = new List<SynthSamplePlayer>();

        private bool _isPlaying;

        private void Awake()
        {
            if (this._initialised) return;
            this.CreateSamplePlayers();
            this.gameObject.SetActive(false);
            this._initialised = true;
        }

        private void CreateSamplePlayers()
        {
            IEnumerable<SynthSample> samples = this.Samples.OrderBy(x => x.startTime);
            int count = 1;
            foreach (SynthSample ss in samples)
            {
                SynthSamplePlayer player = new GameObject("sample" + count++).AddComponent<SynthSamplePlayer>();
                player.transform.SetParent(this.transform);
                player.Sample = ss;
                this._synthSamplePlayers.Add(player);
            }
        }

        public void Play()
        {
            this.startTimeUnity = Time.time;
            this.startTime = DateTime.Now;
            this.gameObject.SetActive(true);
            this._synthPlayersToPlay.Clear();
            this._synthPlayersToPlay.AddRange(this._synthSamplePlayers);
            this._isPlaying = true;
        }

        private void Update()
        {
            if (!this._isPlaying) return;
            float currentTime = this.CurrentTime;
            this._currentTime = currentTime;
            if (this.Samples.All(x => x.IsDone(this.CurrentTime)))
                this.gameObject.SetActive(false);
            this._playersToRemove.Clear();
            for (int i = 0; i < this._synthPlayersToPlay.Count; i++)
            {
                this._synthPlayersToPlay[i].CurrentTime = currentTime;
                if (this._synthPlayersToPlay[i].Sample.IsDone(currentTime))
                {
                    this._playersToRemove.Add(this._synthPlayersToPlay[i]);
                }
            }
            for (int i = 0; i < this._playersToRemove.Count; i++)
                this._synthPlayersToPlay.Remove(this._playersToRemove[i]);
            if (this._synthPlayersToPlay.Count == 0)
            {
                Debug.Log("Done playing " + this.gameObject.name);
                this._isPlaying = false;
                this.gameObject.SetActive(false);
            }

        }
    }
}

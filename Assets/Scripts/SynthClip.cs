namespace Assets.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using UnityEngine;
    using UnityEngine.Audio;

    public class SynthClip : MonoBehaviour
    {
        [HideInInspector]
        public GameObject prefabToSpawn;        

        public List<SynthSample> Samples;

        // we use DateTime so it still works when paused.
        private float startTimeUnity;
        private DateTime startTime;
        private bool _initialised;

        [Header("debug")]
        [SerializeField]
        private float _currentTime;
        public float CurrentTime { get { return (float)(DateTime.Now - this.startTime).TotalSeconds; } }

        private readonly List<SynthSamplePlayer> _synthSamplePlayers = new List<SynthSamplePlayer>();
        private readonly List<SynthSamplePlayer> _synthPlayersToPlay = new List<SynthSamplePlayer>();
        private readonly List<SynthSamplePlayer> _playersToRemove = new List<SynthSamplePlayer>();

        private bool _isPlaying;

        private void Start()
        {
            if (this._initialised) return;
            this.CreateSamplePlayers();
            this.gameObject.SetActive(false);
            this.Setup();
            this._initialised = true;
        }

        public void Setup()
        {
            for (int i = 0; i < this._synthSamplePlayers.Count; i++)
            {
                switch (this._synthSamplePlayers[i].Sample.startMode)
                {
                    case SynthSample.StartMode.Time:
                        break;
                    case SynthSample.StartMode.AfterPrevious:
                        this._synthSamplePlayers[i - 1].Sample.waitingForThisToFinish.Add(this._synthSamplePlayers[i].Sample);
                        break;
                    case SynthSample.StartMode.WithPrevious:
                        this._synthSamplePlayers[i - 1].Sample.waitingForThisToStart.Add(this._synthSamplePlayers[i].Sample);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void CreateSamplePlayers()
        {
            int count = 1;
            foreach (SynthSample ss in this.Samples)
            {
                SynthSamplePlayer player = GameObject.Instantiate(this.prefabToSpawn, this.transform).AddComponent<SynthSamplePlayer>();
                player.gameObject.name = "sample" + count++;
                player.Sample = ss;
                player.Sample.SamplePlayer = player;
                player.dataMode = ss.dataMode;
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

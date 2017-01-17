using UnityEngine;
namespace Assets
{
    class SynthClipPlayer : MonoBehaviour
    {
        [Header("Debug info")]
        [SerializeField]
        private SynthClip[]_clips;

        [SerializeField]
        private int _indexToPlay = 0;
        [SerializeField]
        private bool _play;

        private void Awake()
        {
            this._clips = this.GetComponentsInChildren<SynthClip>(true);
        }

        private void Update()
        {
            if (this._play)
            {
                this._play = false;
                this.Play(this._indexToPlay);
            }
        }

        public void Play(int index)
        {
            this._clips[index].Play();
        }
    }
}

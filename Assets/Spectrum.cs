using UnityEngine;

namespace Assets
{
    using System.Collections.Generic;

    public class Spectrum : MonoBehaviour
    {
        public GameObject prefab;
        public int numberOfObjects = 50;
        public float radius = 10f;
        public List<GameObject> cubes = new List<GameObject>();
        public int multiplyValue = 5000;

        float[] spectrum = new float[1024];

        private void Start()
        {
            // Instantiates a prefab in a circle
            for (int i = 0; i < this.numberOfObjects; i++)
            {
                float angle = i * Mathf.PI * 2 / this.numberOfObjects;
                Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * this.radius;
                this.cubes.Add(Instantiate(this.prefab, pos, Quaternion.identity, this.transform));
            }
        }

        private void Update()
        {
            AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);
            for (int i = 0; i < this.numberOfObjects; i++)
            {
                Vector3 previousScale = this.cubes[i].transform.localScale;
                previousScale.y = Mathf.Lerp(previousScale.y, spectrum[i] * this.multiplyValue, Time.deltaTime * 30);
                this.cubes[i].transform.localScale = previousScale;
            }

        }
    }
}

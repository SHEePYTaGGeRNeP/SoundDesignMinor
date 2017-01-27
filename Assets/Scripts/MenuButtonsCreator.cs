using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts
{
    class MenuButtonsCreator : MonoBehaviour
    {
        [SerializeField]
        private Button _buttonPrefab;
        [SerializeField]
        private SynthClipPlayer _clipPlayer;

        private void Start()
        {
            for (int i = 0; i < _clipPlayer.transform.childCount; i++)
            {
                Button button = GameObject.Instantiate(_buttonPrefab, this.transform);
                button.name = "btnPlay" + _clipPlayer.transform.GetChild(i).name;
                button.GetComponentInChildren<Text>().text = "Play " + _clipPlayer.transform.GetChild(i).name;
                int playIndex = i;
                button.onClick.AddListener(() => Click(playIndex));
            }
        }

        private void Click(int i)
        {
            _clipPlayer.Play(i);
        }
    }
}

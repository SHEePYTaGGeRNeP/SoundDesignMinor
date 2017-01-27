using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class ScriptReader : MonoBehaviour
    {
        #region "Fields"

        public TextAsset script;
        private SynthClip clip;

        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        private void ParseScript()
        {
            // Get all lines
            string[] actions = script.text.Split(new char[] { '\n', '\r', ';' }, StringSplitOptions.RemoveEmptyEntries);

            bool hasDelays = actions.Any(a => a.StartsWith("delay"));
            float totalDelay = 0;
            SynthSamplePlayer.DataMode mode = SynthSamplePlayer.DataMode.Sinus;
            foreach (string action in actions)
            {
                if (action.StartsWith("beep"))
                {
                    string[] values = action.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length < 3)
                        continue;

                    SynthSample sample = new SynthSample();
                    sample.dataMode = mode;
                    sample.startTime = totalDelay;
                    sample.startFreq = Convert.ToInt32(values[1]);
                    sample.duration = Convert.ToInt32(values[2]) / 1000f;
                    if (!hasDelays)
                        totalDelay += Convert.ToInt32(values[2]) / 1000f;
                    clip.Samples.Add(sample);
                }
                else if (action.StartsWith("delay"))
                {
                    string[] values = action.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length == 2)
                    {
                        totalDelay += Convert.ToInt32(values[1]) / 1000f;
                    }
                }
                else if (action.StartsWith("mode"))
                {
                    string[] values = action.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length == 2)
                    {
                        string[] names = Enum.GetNames(mode.GetType());
                        foreach (string name in names)
                        {
                            if (name.ToLower() == values[1].ToLower())
                            {
                                mode = (SynthSamplePlayer.DataMode)Enum.Parse(mode.GetType(), name);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public void Start()
        {
            clip = gameObject.GetComponent<SynthClip>();

            ParseScript();
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}

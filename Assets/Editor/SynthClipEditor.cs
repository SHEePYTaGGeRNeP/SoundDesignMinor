using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CustomEditor(typeof(SynthClip))]
    class SynthClipEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SynthClip clip = (SynthClip)target;
            serializedObject.Update();

            //EditorGUILayout.PropertyField(serializedObject.FindProperty("Samples"), true);
            Show(serializedObject.FindProperty("Samples"), clip);

            EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);
            EditorGUILayout.FloatField("Current Time", clip.CurrentTime);

            serializedObject.ApplyModifiedProperties();
        }

        public static void Show(SerializedProperty list, SynthClip clip)
        {
            EditorGUILayout.PropertyField(list);
            EditorGUI.indentLevel += 1;
            if (list.isExpanded)
            {
                EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
                List<SynthSample> samples = clip.Samples;
                for (int i = 0; i < list.arraySize; i++)
                {
                    EditorGUILayout.BeginVertical();
                    if (samples[i] == null)
                        samples[i] = new SynthSample();
                    samples[i].dataMode = (SynthSamplePlayer.DataMode)EditorGUILayout.EnumPopup("Data Mode", samples[i].dataMode);
                    if (samples[i].SamplePlayer != null)
                        samples[i].SamplePlayer.dataMode = samples[i].dataMode;
                    samples[i].startMode = (SynthSample.StartMode)EditorGUILayout.EnumPopup("Start Mode", samples[i].startMode);
                    if (samples[i].startMode == SynthSample.StartMode.Time)
                    {
                        samples[i].startTime = EditorGUILayout.FloatField("Start Time", samples[i].startTime);
                    }
                    samples[i].startFreq = EditorGUILayout.IntField("Start Frequency", samples[i].startFreq);
                    samples[i].sampleMode = (SynthSample.SampleMode)EditorGUILayout.EnumPopup("Sample Mode", samples[i].sampleMode);
                    if (samples[i].sampleMode == SynthSample.SampleMode.FromTo)
                    {
                        samples[i].endFreq = EditorGUILayout.IntField("End Frequency", samples[i].endFreq);
                        samples[i].freqStep = EditorGUILayout.IntField("Frequency Step", samples[i].freqStep);
                    }
                    else
                    {
                        samples[i].duration = EditorGUILayout.FloatField("Duration", samples[i].duration);
                    }

                    EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);
                    EditorGUILayout.IntField("Current Frequency", samples[i].currentFreq);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.Toggle("Waiting", samples[i].waiting);
                    EditorGUILayout.Toggle("Done", samples[i].done);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                    EditorGUILayout.EndVertical();
                }
                EditorGUI.indentLevel += -1;
            }
            if (GUILayout.Button("Add Sample"))
                clip.Samples.Add(new SynthSample());

        }
    }
}

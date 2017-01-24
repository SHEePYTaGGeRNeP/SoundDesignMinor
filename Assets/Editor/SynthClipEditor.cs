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
            //EditorGUILayout.PropertyField(serializedObject.FindProperty("MonoSamples"), true);
            Show(serializedObject.FindProperty("Samples"), clip);
            //Show(serializedObject.FindProperty("MonoSamples"), clip.MonoSamples);

            serializedObject.ApplyModifiedProperties();
        }

        public static void Show(SerializedProperty list, IList<SynthSampleMono> samples)
        {
            EditorGUILayout.PropertyField(list);
            EditorGUI.indentLevel += 1;
            if (list.isExpanded)
            {
                EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
                for (int i = 0; i < list.arraySize; i++)
                {
                    EditorGUILayout.BeginVertical(GUILayout.MinHeight(100));
                    if (samples[i] == null)
                        samples[i] = new SynthSampleMono();
                    samples[i].Sample.startMode = (SynthSample.StartMode)EditorGUILayout.EnumPopup("Start Mode", samples[i].Sample.startMode);
                    if (samples[i].Sample.startMode == SynthSample.StartMode.Time)
                    {
                        EditorGUILayout.LabelField("Start Time");
                        samples[i].Sample.startTime = EditorGUILayout.FloatField(samples[i].Sample.startTime);
                    }
                    EditorGUILayout.LabelField("Start Frequency");
                    samples[i].Sample.startFreq = EditorGUILayout.IntField(samples[i].Sample.startFreq);

                    samples[i].Sample.sampleMode = (SynthSample.SampleMode)EditorGUILayout.EnumPopup("Sample Mode", samples[i].Sample.sampleMode);
                    if (samples[i].Sample.sampleMode == SynthSample.SampleMode.FromTo)
                    {
                        EditorGUILayout.LabelField("End Frequency");
                        samples[i].Sample.endFreq = EditorGUILayout.IntField(samples[i].Sample.endFreq);
                        EditorGUILayout.LabelField("Frequency Step");
                        samples[i].Sample.freqStep = EditorGUILayout.IntField(samples[i].Sample.freqStep);
                    }
                    else
                    {
                        EditorGUILayout.LabelField("Duration");
                        samples[i].Sample.duration = EditorGUILayout.FloatField(samples[i].Sample.duration);
                    }

                    EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);
                    EditorGUILayout.LabelField("Current Frequency");
                    EditorGUILayout.IntField(samples[i].Sample.currentFreq);
                    EditorGUILayout.LabelField("Waiting");
                    EditorGUILayout.Toggle(samples[i].Sample.waiting);
                    EditorGUILayout.LabelField("Done");
                    EditorGUILayout.Toggle(samples[i].Sample.done);
                    EditorGUILayout.EndVertical();
                }
                EditorGUI.indentLevel += -1;
            }
        }
        public static void Show(SerializedProperty list,SynthClip clip)
        {
            EditorGUILayout.PropertyField(list);
            EditorGUI.indentLevel += 1;
            if (list.isExpanded)
            {
                EditorGUILayout.IntField("Size", clip.Samples.Length);
                SynthSample[] samples = clip.Samples;
                for (int i = 0; i < clip.Samples.Length; i++)
                {
                    EditorGUILayout.BeginVertical();
                    if (samples[i] == null)
                        samples[i] = new SynthSample();
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
        }
    }
}

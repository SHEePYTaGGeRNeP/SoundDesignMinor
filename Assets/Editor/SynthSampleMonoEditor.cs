using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Assets.Editor
{
    [CustomEditor(typeof(SynthSampleMono))]
    public class SynthSampleMonoEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            SynthSampleMono sample = (SynthSampleMono)target;
            if (sample == null)
                sample = new SynthSampleMono();
            sample.Sample.startMode = (SynthSample.StartMode)EditorGUILayout.EnumPopup("Start Mode", sample.Sample.startMode);
            if (sample.Sample.startMode == SynthSample.StartMode.Time)
            {
                EditorGUILayout.LabelField("Start Time");
                sample.Sample.startTime = EditorGUILayout.FloatField(sample.Sample.startTime);
            }
            EditorGUILayout.LabelField("Start Frequency");
            sample.Sample.startFreq = EditorGUILayout.IntField(sample.Sample.startFreq);
            
            sample.Sample.sampleMode = (SynthSample.SampleMode)EditorGUILayout.EnumPopup("Sample Mode", sample.Sample.sampleMode);
            if (sample.Sample.sampleMode == SynthSample.SampleMode.FromTo)
            {
                EditorGUILayout.LabelField("End Frequency");
                sample.Sample.endFreq = EditorGUILayout.IntField(sample.Sample.endFreq);
                EditorGUILayout.LabelField("Frequency Step");
                sample.Sample.freqStep = EditorGUILayout.IntField(sample.Sample.freqStep);
            }
            else
            {
                EditorGUILayout.LabelField("Duration");
                sample.Sample.duration = EditorGUILayout.FloatField(sample.Sample.duration);
            }

            EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Current Frequency");
            EditorGUILayout.IntField(sample.Sample.currentFreq);
            EditorGUILayout.LabelField("Waiting");
            EditorGUILayout.Toggle(sample.Sample.waiting);
            EditorGUILayout.LabelField("Done");
            EditorGUILayout.Toggle(sample.Sample.done);
            //        EditorGUI.PropertyField(new Rect(position.x, position.y + y, 50, y_increase), property.FindPropertyRelative("startFreq"));
            //        y += y_increase;
            //        SerializedProperty sampleMode = property.FindPropertyRelative("sampleMode");
            //        EditorGUI.PropertyField(new Rect(position.x, position.y + y, 50, y_increase), sampleMode);
            //        y += y_increase;
            //        if (sampleMode.enumValueIndex == 0)
            //        {
            //            Rect durationRect = new Rect(position.x, position.y + y, 50, y_increase);
            //            y += y_increase;
            //            EditorGUI.PropertyField(durationRect, property.FindPropertyRelative("duration"));
            //        }
            //        else
            //        {
            //            Rect rect = new Rect(position.x, position.y + y, 50, y_increase);
            //            y += y_increase;
            //            EditorGUI.PropertyField(rect, property.FindPropertyRelative("endFreq"));
            //            rect = new Rect(position.x, position.y + y, 50, y_increase);
            //            //y+= y_increase;
            //            EditorGUI.PropertyField(rect, property.FindPropertyRelative("freqStep"));
            //        }
        }
    }
}

﻿using UnityEditor;

namespace Assets.Editor
{
    using Assets.Scripts;
    using UnityEngine;

    [CustomEditor(typeof(SynthSamplePlayer))]
    public class SynthSamplePlayerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SynthSamplePlayer player = (SynthSamplePlayer)this.target;
            EditorGUILayout.BeginVertical();
            if (player.Sample == null)
                player.Sample = new SynthSample();
            player.Sample.dataMode = (SynthSamplePlayer.DataMode)EditorGUILayout.EnumPopup("Data Mode", player.Sample.dataMode);
            player.dataMode = player.Sample.dataMode;
            player.Sample.startMode = (SynthSample.StartMode)EditorGUILayout.EnumPopup("Start Mode", player.Sample.startMode);
            if (player.Sample.startMode == SynthSample.StartMode.Time)
            {
                player.Sample.startTime = EditorGUILayout.FloatField("Start Time", player.Sample.startTime);
            }
            player.Sample.startFreq = EditorGUILayout.IntField("Start Frequency", player.Sample.startFreq);
            player.Sample.sampleMode = (SynthSample.SampleMode)EditorGUILayout.EnumPopup("Sample Mode", player.Sample.sampleMode);
            if (player.Sample.sampleMode == SynthSample.SampleMode.FromTo)
            {
                player.Sample.endFreq = EditorGUILayout.IntField("End Frequency", player.Sample.endFreq);
                player.Sample.freqStep = EditorGUILayout.IntField("Frequency Step", player.Sample.freqStep);
            }
            else
            {
                player.Sample.duration = EditorGUILayout.FloatField("Duration", player.Sample.duration);
            }
            player.Sample.startGain = EditorGUILayout.FloatField("Start Gain", player.Sample.startGain);
            player.Sample.gainStep = EditorGUILayout.FloatField("Gain Step", player.Sample.gainStep);
            player.Sample.pitch = EditorGUILayout.FloatField("Pitch", player.Sample.pitch);
            player.GetComponent<AudioSource>().pitch = player.Sample.pitch;
            //player.Sample.nrOfRepeats = EditorGUILayout.IntField("Repeat Time", player.Sample.nrOfRepeats);
            //if (player.Sample.nrOfRepeats > 1)
            //    player.Sample.reverseRepeat = EditorGUILayout.Toggle("Reverse Repeat", player.Sample.reverseRepeat);

            EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);
            EditorGUILayout.IntField("Current Frequency", player.Sample.currentFreq);
            EditorGUILayout.FloatField("Current Gain", player.gain);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Toggle("Waiting", player.Sample.waiting);
            EditorGUILayout.Toggle("Done", player.Sample.done);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

    }
}

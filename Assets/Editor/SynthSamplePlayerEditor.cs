using UnityEditor;

namespace Assets.Editor
{
    [CustomEditor(typeof(SynthSamplePlayer))]
    public class SynthSamplePlayerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SynthSamplePlayer player = (SynthSamplePlayer)target;
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

            EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);
            EditorGUILayout.IntField("Current Frequency", player.Sample.currentFreq);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Toggle("Waiting", player.Sample.waiting);
            EditorGUILayout.Toggle("Done", player.Sample.done);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

    }
}

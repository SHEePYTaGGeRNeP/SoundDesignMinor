using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
namespace Assets.Editor
{
    using UnityEngine;

    using Editor = UnityEditor.Editor;

    //[CustomPropertyDrawer(typeof(SynthSample))]
    //public class SynthSampleDrawer : PropertyDrawer
    //{
    //    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //    {
    //        // Using BeginProperty / EndProperty on the parent property means that
    //        // prefab override logic works on the entire property.
    //        EditorGUI.BeginProperty(position, label, property);

    //        // Draw label
    //        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

    //        // Don't make child fields be indented
    //        //int indent = EditorGUI.indentLevel;
    //        //EditorGUI.indentLevel = 0;
    //        float y = 0;
    //        float y_increase = 50;
    //        Rect startModeRect = new Rect(position.x, position.y, 100, y_increase);
    //        SerializedProperty startMode = property.FindPropertyRelative("startMode");
    //        EditorGUI.PropertyField(startModeRect, startMode);
    //        y += y_increase;
    //        if (startMode.enumValueIndex == 0)
    //        {
    //            Rect startTimeRect = new Rect(position.x, position.y, 100, y_increase);
    //            y += y_increase;
    //            EditorGUI.PropertyField(startTimeRect, property.FindPropertyRelative("startTime"));
    //        }
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


    //    // Calculate rects
    //        //Rect amountRect = new Rect(position.x, position.y, 30, position.height);
    //        //Rect unitRect = new Rect(position.x + 35, position.y, 50, position.height);
    //        //Rect nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

    //        //// Draw fields - passs GUIContent.none to each so they are drawn without labels
    //        //EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
    //        //EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("unit"), GUIContent.none);
    //        //EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

    //        // Set indent back to what it was
    //        //EditorGUI.indentLevel = indent;

    //        EditorGUI.EndProperty();
    //    }
    //}
}

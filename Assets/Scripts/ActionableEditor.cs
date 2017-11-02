using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// [CustomPropertyDrawer(typeof(Actionable))]
public class ActionableEditor : PropertyDrawer {

    SerializedProperty animationName;
    SerializedProperty setTo;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        // Calculate rects
        Rect amountRect = new Rect(position.x, position.y, 30, position.height);
        Rect unitRect = new Rect(position.x + 35, position.y, 50, position.height);
        Rect nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("AnimationName"), GUIContent.none);
        EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("SetTo"), GUIContent.none);

        EditorGUI.EndProperty();
    }

    //void OnEnable()
    //{
    //    Debug.Log("Actionable Editor");
    //    animationName = serializedObject.FindProperty("AnimationName");
    //    setTo = serializedObject.FindProperty("SetTo");
    //}

    //public override void OnInspectorGUI()
    //{
    //    return;
    //    Debug.Log("Actionable Editor");

    //    serializedObject.Update();
    //    EditorGUILayout.PropertyField(setTo);

    //    serializedObject.ApplyModifiedProperties();
    //    EditorGUILayout.LabelField("(Above this object)");

    //    //if (lookAtPoint.vector3Value.y > (target as LookAtPoint).transform.position.y)
    //    //{
    //    //}
    //    //if (lookAtPoint.vector3Value.y < (target as LookAtPoint).transform.position.y)
    //    //{
    //    //    EditorGUILayout.LabelField("(Below this object)");
    //    //}
    //}
}

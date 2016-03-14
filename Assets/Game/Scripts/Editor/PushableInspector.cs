using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Pushable))]
public class PushableInspector : Editor {

  string[] directions = {"Left", "Right", "Down", "Up"};

  public override void OnInspectorGUI() {
    Pushable pushable = target as Pushable;

    DrawDefaultInspector();

    pushable.directions = EditorGUILayout.MaskField("Directions", pushable.directions, directions);
  }
}

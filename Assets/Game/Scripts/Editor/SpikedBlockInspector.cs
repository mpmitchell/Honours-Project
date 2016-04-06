using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpikedBlock))]
public class SpikedBlockInspector : Editor {

  string[] directions = {"Left", "Right", "Down", "Up"};

  public override void OnInspectorGUI() {
    SpikedBlock pushable = target as SpikedBlock;

    DrawDefaultInspector();

    pushable.directions = EditorGUILayout.MaskField("Directions", pushable.directions, directions);
  }
}

using UnityEngine;
using UnityEditor;

public class PrefabUtilities : EditorWindow {

  GameObject prefab = null;
  string prefabPath = "";

  [MenuItem("Tools/Prefab Utilities")]
  public static void ShowWindow() {
    EditorWindow.GetWindow<PrefabUtilities>("Prefab Utilities");
  }

  void OnGUI() {
    EditorGUILayout.BeginHorizontal();

      if (GUILayout.Button("Disconnect")) {
        foreach (GameObject gameObject in Selection.gameObjects) {
          PrefabUtility.DisconnectPrefabInstance(gameObject);
        }
      }

      if (GUILayout.Button("Reconnect")) {
        foreach (GameObject gameObject in Selection.gameObjects) {
          prefab = PrefabUtility.GetPrefabParent(gameObject) as GameObject;
          PrefabUtility.ConnectGameObjectToPrefab(gameObject, prefab);
        }
      }

    EditorGUILayout.EndHorizontal();
    EditorGUILayout.BeginHorizontal();

      if (GUILayout.Button("Connect to")) {
        foreach (GameObject gameObject in Selection.gameObjects) {
          PrefabUtility.ConnectGameObjectToPrefab(gameObject, prefab);
        }
      }

      prefab = EditorGUILayout.ObjectField(prefab, typeof(GameObject), false) as GameObject;

    EditorGUILayout.EndHorizontal();

    prefabPath = GUILayout.TextField(prefabPath);

    EditorGUILayout.BeginHorizontal();

      if (GUILayout.Button("Make Prefab")) {
        foreach (GameObject gameObject in Selection.gameObjects) {
          prefab = PrefabUtility.CreatePrefab("Assets/Game/Prefabs/" + prefabPath + "/" + gameObject.name + ".prefab", gameObject);
          PrefabUtility.ConnectGameObjectToPrefab(gameObject, prefab);
        }
      }

      if (GUILayout.Button("Get Prefab")) {
        prefab = PrefabUtility.GetPrefabParent(Selection.activeGameObject) as GameObject;
      }

    EditorGUILayout.EndHorizontal();
  }
}

using UnityEngine;
using UnityEditor;

public class TileGrid : EditorWindow {

  int x = 1;
  int y = 1;

  [MenuItem("Tools/Tiler %#t")]
  public static void ShowWindow() {
    EditorWindow.GetWindow<TileGrid>("Tiler");
  }

  void OnGUI() {
    x = EditorGUILayout.IntSlider("x", x, 1, 15);
    y = EditorGUILayout.IntSlider("y", y, 1, 11);

    EditorGUILayout.BeginHorizontal();

      if (GUILayout.Button("Clone")) {
        GameObject target = Selection.activeGameObject;
        GameObject prefab = PrefabUtility.GetPrefabParent(target) as GameObject;
        Vector3 origin = target.transform.position;

        for (int i = 0; i < x; i++) {
          for (int j = 0; j < y; j++) {
            if (i == 0 && j == 0) {
              continue;
            }

            Vector3 position = new Vector3(origin.x + i, origin.y + j, origin.z);
            GameObject clone = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            clone.transform.position = position;
            clone.transform.parent = target.transform.parent;
          }
        }
      }

      if (GUILayout.Button("Reset")) {
        x = 1;
        y = 1;
      }

    EditorGUILayout.EndHorizontal();

    if (GUILayout.Button("Snap to Grid")) {
      foreach (GameObject gameObject in Selection.gameObjects) {
        gameObject.transform.position = new Vector3(
          Mathf.Round(gameObject.transform.position.x),
          Mathf.Round(gameObject.transform.position.y),
          gameObject.transform.position.z
          );
      }
    }
  }
}

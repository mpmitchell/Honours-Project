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
    EditorGUILayout.BeginHorizontal();

      GUILayout.Label("X: " + x);
      x = (int)GUILayout.HorizontalSlider(x, -15, 15);

    EditorGUILayout.EndHorizontal();
    EditorGUILayout.BeginHorizontal();

      GUILayout.Label("Y: " + y);
      y = (int)GUILayout.HorizontalSlider(y, -11, 11);

    EditorGUILayout.EndHorizontal();
    EditorGUILayout.BeginHorizontal();

      if (GUILayout.Button("Clone")) {
        GameObject target = Selection.activeGameObject;
        GameObject prefab = PrefabUtility.GetPrefabParent(target) as GameObject;
        Vector3 origin = target.transform.position;

        float dx = Mathf.Sign(x);
        float dy = Mathf.Sign(y);

        float x_ = Mathf.Abs(x);
        float y_ = Mathf.Abs(y);

        for (int i = 0; i < x_; i++) {
          for (int j = 0; j < y_; j++) {
            if (i == 0 && j == 0) {
              continue;
            }

            Vector3 position = new Vector3(origin.x + i * dx, origin.y + j * dy, origin.z);
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
  }
}

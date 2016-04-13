using UnityEngine;
using UnityEditor;

public class ResetPlayerPrefs : MonoBehaviour {

  [MenuItem("Edit/Reset Playerprefs")] public static void DeletePlayerPrefs() { PlayerPrefs.DeleteAll(); }
}

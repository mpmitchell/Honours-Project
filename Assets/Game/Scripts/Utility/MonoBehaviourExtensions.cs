using UnityEngine;

public static class MonoBehaviourExtensions {

  public static float GetX(this GameObject gameObject) {
    return gameObject.transform.position.x;
  }

  public static float GetY(this GameObject gameObject) {
    return gameObject.transform.position.y;
  }

  public static float GetZ(this GameObject gameObject) {
    return gameObject.transform.position.z;
  }

  public static void SetX(this GameObject gameObject, float x) {
    gameObject.transform.position = new Vector3(x, gameObject.GetY(), gameObject.GetZ());
  }

  public static void SetY(this GameObject gameObject, float y) {
    gameObject.transform.position = new Vector3(gameObject.GetX(), y, gameObject.GetZ());
  }

  public static void SetZ(this GameObject gameObject, float z) {
    gameObject.transform.position = new Vector3(gameObject.GetX(), gameObject.GetY(), z);
  }
}

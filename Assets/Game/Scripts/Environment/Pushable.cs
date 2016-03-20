using UnityEngine;

public class Pushable : MonoBehaviour {

  [HideInInspector] public int directions;

  [SerializeField] bool onlyOnce = true;
  [SerializeField] [Range(1, 13)] int maxDistance;
  [SerializeField] [Range(1, 10)] int drag;

  float distancePushed = 0.0f;
  bool pushed = false;

  void PushLeft() {
    if (!pushed && (directions & (int)Direction.Left) != 0) {
      float translation = 1.0f / drag;
      distancePushed += translation;

      if (onlyOnce && distancePushed >= maxDistance) {
        pushed = true;
      }

      transform.Translate(Vector3.left * translation);
    }
  }

  void PushRight() {
    if (!pushed && (directions & (int)Direction.Right) != 0) {
      float translation = 1.0f / drag;
      distancePushed += translation;

      if (onlyOnce && distancePushed >= maxDistance) {
        pushed = true;
      }

      transform.Translate(Vector3.right * translation);
    }
  }

  void PushDown() {
    if (!pushed && (directions & (int)Direction.Down) != 0) {
      float translation = 1.0f / drag;
      distancePushed += translation;

      if (onlyOnce && distancePushed >= maxDistance) {
        pushed = true;
      }

      transform.Translate(Vector3.down * translation);
    }
  }

  void PushUp() {
    if (!pushed && (directions & (int)Direction.Up) != 0) {
      float translation = 1.0f / drag;
      distancePushed += translation;

      if (onlyOnce && distancePushed >= maxDistance) {
        pushed = true;
      }

      transform.Translate(Vector3.up * translation);
    }
  }
}

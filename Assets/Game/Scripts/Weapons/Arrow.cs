using UnityEngine;

public class Arrow : MonoBehaviour {

  Direction direction;

  void Update() {
    //
  }

  public void Activate(Vector3 origin, Direction direction) {
    gameObject.SetActive(true);
    transform.position = origin;

    this.direction = direction;

    switch (direction) {
      case Direction.Left: {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 315.0f);
        break;
      }

      case Direction.Right: {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 135.0f);
        break;
      }

      case Direction.Down: {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 45.0f);
        break;
      }

      case Direction.Up: {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 225.0f);
        break;
      }
    }
  }
}

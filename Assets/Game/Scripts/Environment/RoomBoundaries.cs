using UnityEngine;

public class RoomBoundaries : MonoBehaviour {

  [SerializeField] Direction direction;
  [SerializeField] Room room;

  Vector3 cameraTranslation;

  void Awake() {
    switch (direction) {
      case Direction.Left: {
        cameraTranslation = Vector3.left * 15.0f;
        break;
      }

      case Direction.Right: {
        cameraTranslation = Vector3.right * 15.0f;
        break;
      }

      case Direction.Down: {
        cameraTranslation = Vector3.down * 11.0f;
        break;
      }

      case Direction.Up: {
        cameraTranslation = Vector3.up * 11.0f;
        break;
      }
    }
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (room != Room.currentRoom) {
      Room.entering = room;
    }
  }

  void OnTriggerExit2D(Collider2D collider) {
    if (room == Room.currentRoom && Room.entering != null) {
      Room.currentRoom = Room.entering;
      Room.entering = null;

      Camera.main.transform.Translate(cameraTranslation);
    } else if (room == Room.entering) {
      Room.entering = null;
    }
  }
}

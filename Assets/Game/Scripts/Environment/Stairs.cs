using UnityEngine;

public class Stairs : MonoBehaviour {

  [SerializeField] Stairs otherStairs;

  public Room room;

  static GameObject mapCamera = null;

  bool exited = true;

  void Start() {
    if (mapCamera == null) {
      mapCamera = GameObject.FindWithTag("MapCamera");
    }
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (exited) {
      Camera.main.transform.position = new Vector3(otherStairs.room.transform.position.x, otherStairs.room.transform.position.y, Camera.main.transform.position.z);
      mapCamera.transform.localPosition = new Vector3(otherStairs.room.node.position.mapNumber * 105.0f, 40.0f, 0.0f);
      PlayerController.player.transform.position = otherStairs.transform.position;

      otherStairs.exited = false;

      Room.currentRoom.Exit();
      otherStairs.room.Enter();
      otherStairs.room.LockGates();

      Room.currentRoom = otherStairs.room;
    }
  }

  void OnTriggerExit2D(Collider2D collider) {
    exited = true;
  }
}

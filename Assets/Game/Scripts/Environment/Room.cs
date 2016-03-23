using UnityEngine;

public class Room : MonoBehaviour {

  [SerializeField] bool startingRoom;

  public static Room currentRoom = null;
  public static Room entering = null;

  void Awake() {
    if (startingRoom) {
      currentRoom = this;
    }
  }
}

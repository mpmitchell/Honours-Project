using UnityEngine;

public class Gate : MonoBehaviour {

  [SerializeField] bool openOnRoomClear;

  void RoomClear() {
    if (openOnRoomClear) {
      Destroy(gameObject);
    }
  }

  void Open() {
    Destroy(gameObject);
  }
}

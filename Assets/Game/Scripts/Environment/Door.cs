using UnityEngine;

public class Door : MonoBehaviour {

  [SerializeField] GameObject matchingDoor;

  void Open() {
    Destroy(matchingDoor);
    Destroy(gameObject);
  }
}

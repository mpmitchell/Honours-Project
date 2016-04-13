using UnityEngine;

public class Lock : MonoBehaviour {

  [SerializeField] GameObject matchingLock;

  void Open() {
    Destroy(matchingLock);
    Destroy(gameObject);
  }
}

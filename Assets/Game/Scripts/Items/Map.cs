using UnityEngine;

public class Map : MonoBehaviour {

  void OnTriggerEnter2D(Collider2D collider) {
    Room.GotMap();
    Destroy(gameObject);
  }
}

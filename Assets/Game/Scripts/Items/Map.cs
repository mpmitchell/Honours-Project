using UnityEngine;

public class Map : MonoBehaviour {

  void OnTriggerEnter2D(Collider2D collider) {
    Logger.Log("pickup map");
    Room.GotMap();
    Destroy(gameObject);
  }
}

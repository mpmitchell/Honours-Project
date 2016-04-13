using UnityEngine;

public class Compass : MonoBehaviour {

  void OnTriggerEnter2D(Collider2D collider) {
    Logger.Log("pickup compass");
    Room.GotCompass();
    Destroy(gameObject);
  }
}

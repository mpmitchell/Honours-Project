using UnityEngine;

public class Compass : MonoBehaviour {

  void OnTriggerEnter2D(Collider2D collider) {
    Room.GotCompass();
    Destroy(gameObject);
  }
}
